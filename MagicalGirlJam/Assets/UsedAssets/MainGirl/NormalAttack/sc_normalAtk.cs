using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sc_normalAtk : MonoBehaviour
{
    public Transform target;
    [SerializeField] int WeaponLevel = 0;
    public float damage = 10f, speed = 10f, projectileCount = 1;
    public string[] levelDescriptions;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0,-1,0), speed*Time.deltaTime);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<sc_StandardWarrior>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void LevelWeaponUP()
    {
        WeaponLevel++;

        switch (WeaponLevel)
        {
            case 1:
                damage = damage;
                speed = speed;
                levelDescriptions[0] = "Fire a blue magic ball towards nearest enemy.";
                break;
            case 2:
                damage += damage * 20 / 100;
                levelDescriptions[1] = "Increase damage by 20%.";
                break;
            case 3:
                speed += speed * 20 / 100;
                levelDescriptions[2] = "Increase projectile speed by 20%.";
                break;
            case 4:
                damage += damage * 20 / 100;
                levelDescriptions[1] = "Increase damage by another 20%.";
                break;
            case 5:
                speed += speed * 20 / 100;
                levelDescriptions[2] = "Increase projectile speed by another 20%.";
                break;
            case 6:
                damage += damage * 2;
                levelDescriptions[2] = "Double the damage.";
                break;
        }
    }


    /*public void StartBallzRoutine() => StartCoroutine(Ballz());
    public void StopBallzRoutine() => StopCoroutine(Ballz());
    IEnumerator Ballz()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(normalAttack, weaponOrigin.transform.position, weaponOrigin.transform.rotation);
        }
    }*/
}
