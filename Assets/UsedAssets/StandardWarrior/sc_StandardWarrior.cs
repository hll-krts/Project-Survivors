using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_StandardWarrior : MonoBehaviour
{
    GameObject player;
    public float speed = 5f;

    bool didItLeave=true, isItTouching=false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("runnin", true);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawLine(this.transform.position, player.transform.position + new Vector3(0f, 3f, 0f));

        if (player.transform.position.x < transform.position.x) 
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f,3f,0f), speed*Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            animator.SetBool("runnin", false);
            isItTouching = true;
            didItLeave = false;
            speed = 0f;

            StopCoroutine(StartMoving());
            StartCoroutine(AttackAnim());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isItTouching = true;
        didItLeave = false;
        speed = 0f;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isItTouching = false;
            didItLeave = true;

            StartCoroutine(StartMoving());
            StopCoroutine(AttackAnim());
        }
    }
    IEnumerator StartMoving()
    {
        while (didItLeave)
        {
            yield return new WaitForSecondsRealtime(1f);
            speed = 5f;
            animator.SetBool("runnin", true);
            didItLeave = false;
        }
    }
    IEnumerator AttackAnim()
    {
        while(isItTouching)
        {
            animator.SetTrigger("attack");
            yield return new WaitForSecondsRealtime(2f);
            animator.ResetTrigger("attack");
        }
    }
}
