using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sc_player : MonoBehaviour
{
    public float maxHP = 100.0f, healthPoints, baseATK = 50, speed = 8.0f, armor = 10.0f, healthRegen = 0.0f, coinGainPerChance = 10.0f, coinGainMultiplier = 1.0f;
    public bool alive;
    Camera camera;
    Vector3 mousePosition, playerPosition;
    Animator animator;
    public GameObject normalAttack;
    [SerializeField] GameObject weaponOrigin;

    GameObject[] allEnemies;
    [SerializeField] Transform closestEnemy;
    private float enemyDistance, closestDistance;

    public sc_normalAtk normalAtkTarget;
    sc_Spawner Spawner;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Ballz());
        animator = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        healthPoints = maxHP;
        alive = true;
        //Spawner = FindObjectOfType<sc_Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        normalAtkTarget.target = closestEnemy;
        //Spawner.alive = alive;
    }

    void FixedUpdate()
    {
        closestEnemy = GetClosestEnemy();
        weaponOrigin.transform.LookAt(closestEnemy);

        //------------------------------------------------------------------------------------------------

        var xx = Input.GetAxis("Horizontal");
        var yy = Input.GetAxis("Vertical");

        Move(xx, yy);
        Camera();
    }

    void Camera()
    {
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        //normalAttack.transform.position = mousePosition;

        //------------------------------------------------------------------------------------------------

        if(mousePosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
    }

    void Move(float x, float y)
    {
        if(x != 0 || y != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        //transform.position = new Vector2(x * speed, y * speed);
        transform.position += Vector3.up * (y * speed) * Time.deltaTime;
        transform.position += Vector3.right * (x * speed) * Time.deltaTime;
    }

    void Death()
    {
        alive = false;
    }

    Transform GetClosestEnemy()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach(var enemy in allEnemies)
        {
            enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(enemyDistance < closestDistance)
            {
                closestDistance = enemyDistance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }

    public void LevelUp()
    {
        maxHP += 5 * (maxHP / 100);
        speed += 5 * (speed / 100);
    }

    public void GetHit(float damage)
    {
        healthPoints -= damage;
    }

    IEnumerator HealthRegen()
    {
        healthPoints += healthRegen;
        yield return new WaitForSeconds(1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy") ;//Debug.Log(other.collider.tag + "FUCK");
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy") ;//Debug.Log(other.collider.tag + "SIKE");
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag + "FUCK");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.tag + "SIKE");
    }*/
}
