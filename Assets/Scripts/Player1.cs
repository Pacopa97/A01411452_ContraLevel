using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    protected BoxCollider2D collider;
    public GameObject player;
    public Transform endPos;
    public GameObject bulletPrefab;
    public Transform bulletSpawner;


    public float force;
    public int lives;
    public bool dead = false;
    public bool grounded = true;
    public bool jumping = false;
    private bool hitted = false;
    private bool isCrouching = false;
    private bool end = false;

    public int direction = 1;
   

    protected LevelManager levelManger;
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected BoxCollider2D boxCol;

    public static Player instance;

    public float scrollSpeed = 1;
    public float speed = 1.5f;
 

    void Start()
    {
        Flip();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        levelManger = GetComponent<LevelManager>();
       
    }


    // Update is called once per frame
    void Update()
    {
        if (end==false)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (grounded == true && dead == false) { Jump(); }
            }

            if (isCrouching == false && dead == false && hitted == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(Attack());
                }
                if (Input.GetKey(KeyCode.LeftArrow) && IsGrounded() && !Input.GetKey(KeyCode.DownArrow))
                {
                    animator.SetBool("isWalking", true);

                    direction = -1;
                    Flip();
                    transform.position += Vector3.left * speed * Time.deltaTime;
                }
                else if (!Input.GetKey(KeyCode.RightArrow)) { animator.SetBool("isWalking", false); }

                if (Input.GetKey(KeyCode.RightArrow) && IsGrounded() && !Input.GetKey(KeyCode.DownArrow))
                {
                    animator.SetBool("isWalking", true);
                    direction = 1;
                    Flip();
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                else if (!Input.GetKey(KeyCode.LeftArrow))
                {
                    animator.SetBool("isWalking", false);
                }
            }



            if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("isCrouching", true);
                isCrouching = true;
                if (Input.GetKey(KeyCode.Space))
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                isCrouching = false;
                animator.SetBool("isCrouching", false);
            }
        }
        if (hitted == true)
        {
            if (direction == 1)
            {
                Flip();
                transform.position += Vector3.right * 1.5f * Time.deltaTime;

            }
            if (direction == -1)
            {
                Flip();
                transform.position += Vector3.left * 1.5f * Time.deltaTime;
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

    IEnumerator Attack()
    {
        animator.SetBool("isShooting",true);
        Instantiate(bulletPrefab,bulletSpawner.position,Quaternion.identity);
        yield return new WaitForSeconds(1);
        animator.SetBool("isShooting", false);
    }

    void Jump()
    {
        // jump animation 

        animator.SetBool("isJumping", true);
        jumping = true;
        rigidbody.velocity = new Vector2(0, 3f);

        
        
            if (direction == 1 && Input.GetKey(KeyCode.RightArrow))
            {
                Flip();
                transform.position += Vector3.right * 1.5f * Time.deltaTime;
                if (grounded == true) jumping = false;
            }
            if (direction == -1 && Input.GetKey(KeyCode.LeftArrow))
            {
                Flip();
                transform.position += Vector3.left * 1.5f * Time.deltaTime;
                if (grounded == true) jumping = false;
            }
        

        //rigidbody.AddForce(new Vector3(0f, 15f, 0f), ForceMode2D.Impulse);

        animator.SetBool("isJumping", false);

    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (lives <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(Hit(coll));
            }
        }
    }

    IEnumerator Die()
    {
        lives--;
        dead = true;
       // animator.SetBool("isDead", true);
        yield return new WaitForSeconds(3f);
    }

    IEnumerator Hit(Collider2D coll)
    {
        //animator.SetBool("Hurt", true);
        hitted = true;
        rigidbody.velocity = new Vector2(0, 3f);

        yield return new WaitUntil(() => grounded == true);
        hitted = false;
        animator.SetBool("Hurt", false);
    }


    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = direction;
        transform.localScale = theScale;
    }

    public bool IsGrounded()
    {
        if (grounded == true) return true;
        else return false;
    }



}