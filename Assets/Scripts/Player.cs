using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int lives = 3;
    public bool grounded;
    public bool crouch;
    public float fireR;
    public float speed=1;
    
    private int direction = 1;
    private bool death = false;
    private bool isJumping;

    public GameObject bullet;

    protected Rigidbody2D rigidBody;
    protected Animator animator;



	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (grounded == true && death == false)
            {
                StartCoroutine(Jumping());
            }
        }
        if (IsGrounded()&&death==false)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow))
            {

                direction = -1;
                Flip();
                animator.SetBool("isWalking", true);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                direction = 1;
                Flip();
                animator.SetBool("isWalking", true);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("isCrouching",true);
                crouch = true;
            }else if (!Input.GetKey(KeyCode.RightArrow) || !Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("isWalking", false);
            }
            else if(!Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("isCrouching", false);
                crouch = false;
            }
             
        }
       


    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            grounded = true;
        }

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            grounded = false;
        }

    }

    private bool IsGrounded()
    {
        if (grounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;
    }

    IEnumerator Jumping()
    {
        isJumping = true;
        animator.SetBool("isJumping", true);
        //rigidBody.AddForce(new Vector2(0,3),ForceMode2D.Impulse);
        rigidBody.velocity = new Vector2(0,3);
        //yield return new WaitForSeconds(1.5f);
       
        yield return new WaitUntil(()=> grounded==false);
        animator.SetBool("isJumping", false);
    }

 

}
