using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	 private Rigidbody2D myRigidbody;
	private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

	private bool attack;

    private bool facingRight;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	private LayerMask whatIsGround;

	private bool isGrounded;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		
	}

	void Update()
	{
		HandleInput ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
    { 

        float horizontal = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded ();
       

        HandleMovement(horizontal);

        Flip(horizontal);

		HandleAttacks ();

		ResetValues ();
		
	}
    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);

		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));
    }

	private void HandleAttacks()
	{
		if (attack)
		{
			myAnimator.SetTrigger ("SwipeUp");

	   }
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown (KeyCode.E))
		{
			attack = true;
		}
	
	}

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
           

        }

    }

	private void ResetValues()

	{
		attack = false;
	}
		
	private bool IsGrounded()
	{
		if (myRigidbody.velocity.y <= 0) 
		{
			foreach (Transform point in groundPoints) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].gameObject != gameObject)
					{
						return true;
						
					}
				}
			}
			return false;
		}
	}
}
