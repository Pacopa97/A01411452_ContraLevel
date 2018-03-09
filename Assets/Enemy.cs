using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 2;
    public int health = 2;
    public GameObject moveEnemy;

    private bool damaged = false;
    private Vector3 run;
    private bool death = false;
    private int direction;
    private bool moveRight, moveLeft;

    protected Animator animator;
    protected Animator moveEnemyAnimator;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        moveEnemyAnimator = moveEnemy.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (death == true)
        {
           // GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().ghostCounter--;
            Destroy(gameObject);
        }
        if (moveLeft) MoveLeft(); else MoveRight();
    }
    void MoveRight()
    {
        if (damaged == false)
        {
            moveLeft = false;
            run.x = 1;
            Flip();
            transform.position += run * speed * Time.deltaTime;
        }
    }

    void MoveLeft()
    {
        if (damaged == false)
        {
            moveLeft = true;
            run.x = -1;
            Flip();
            transform.position += run * speed * Time.deltaTime;
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = -run.x;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            if (moveLeft == false)
                moveLeft = true;
            else moveLeft = false;
        }

        if (coll.gameObject.tag == "Bullet")
        {
            health--;
            if (health <= 0)
            {
                StartCoroutine(Killed());
           
            }
        }

    }

    IEnumerator Killed()
    {
        animator.SetBool("isDead", true);
        moveEnemyAnimator.SetBool("DeadJump", true);

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    }
