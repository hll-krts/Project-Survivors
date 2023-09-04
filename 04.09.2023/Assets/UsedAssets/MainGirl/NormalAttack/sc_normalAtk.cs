using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sc_normalAtk : MonoBehaviour
{
    public Transform target;
    public float damage, speed, lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
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
}
