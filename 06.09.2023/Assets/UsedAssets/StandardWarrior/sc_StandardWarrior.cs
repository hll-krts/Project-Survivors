using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_StandardWarrior : MonoBehaviour
{
    GameObject player;
    public float maxHP, healthPoints, speed, ATK, armor, Level;
    [SerializeField] float baseMaxHP = 50f, baseATK = 10f, baseSPD = 5f, baseArmor = 5f, StartLevel = 0f;

    bool didItLeave=true, isItTouching=false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //setting up things that doesn't change
        maxHP = baseMaxHP; ATK = baseATK; speed = baseSPD; armor = baseArmor;
        Level = StartLevel;


        //get the sprite renderer and animator
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("runnin", true);

        //set current hp to max hp
        healthPoints = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //find player object
        player = GameObject.FindGameObjectWithTag("Player");

        Debug.DrawLine(this.transform.position, player.transform.position + new Vector3(0f, 3f, 0f));

        //rotate towards player object
        if (player.transform.position.x < transform.position.x) 
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }

        //move towards player obj
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f,3f,0f), speed*Time.deltaTime);

    }

    private void FixedUpdate()
    {
        if(Time.deltaTime > 0 && Time.deltaTime % 60 == 0) { }
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
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isItTouching = true;
            didItLeave = false;
            speed = 0f; 
        }
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
            player.GetComponent<sc_player>().GetHit(ATK);
            animator.ResetTrigger("attack");
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------
    //Taking damage, leveling up, getting stunned/frozed etc.
    //------------------------------------------------------------------------------------------------------------------------------------------------


    public void LEvelUp()
    {
        Level++;

        maxHP += 5 * (maxHP / 100);
        speed += 5 * (speed / 100);
        ATK += 5 * (ATK / 100);
    }

    public void TakeDamage(float damage) 
    {
        healthPoints -= (damage * (100-armor)) / 100;

        if (healthPoints < 0) 
        {
            player.GetComponent<sc_player>().EXPoints++;
            Destroy(this.gameObject);
        }
    }
}
