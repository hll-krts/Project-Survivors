using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_normalAtk : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0,-1,0), 10*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("A");
            Destroy(this.gameObject);
        }
    }
}
