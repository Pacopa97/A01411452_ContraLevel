using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [HideInInspector]
    public bool reachDoor = false;
    public Transform waypoint;
    public GameObject player;
    protected Animator playerAnimator;
    public float speedWalk;

    void Start()
    {
        
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reachDoor == true)
        {
            StartCoroutine(reachEnd());
        }
    }

    IEnumerator reachEnd()
    {
        playerAnimator.SetInteger("Speed", 0);
        yield return new WaitForSeconds(1.5f);

        playerAnimator.SetInteger("Speed", 1);
        Vector3 dir = waypoint.position - player.transform.position;
        float distanceThisFrame = speedWalk * Time.deltaTime;
        player.transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        yield return new WaitForSeconds(1.5f);
        playerAnimator.SetInteger("Speed", 0);

        yield return new WaitForSeconds(1.5f);
        reachDoor = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerAnimator.SetInteger("Speed", 1);
            reachDoor = true;
        }
    }
}