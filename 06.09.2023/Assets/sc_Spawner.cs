using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Spawner : MonoBehaviour
{
    public GameObject StandardWarriorPrefab, kamera;
    public GameObject[] SpawnLocations;

    bool alive;
    float SpawnRate = 3f;

    sc_player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<sc_player>();
        alive = playerScript.alive;
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        alive = playerScript.alive;
    }

    IEnumerator SpawnEnemies()
    {
        while(alive)
        {
            yield return new WaitForSeconds(SpawnRate);
            Transform SpawnLoc = SpawnLocations[Random.Range(0, 2)].transform;
            Instantiate(StandardWarriorPrefab, SpawnLoc.transform.position + new Vector3(0, Random.Range(-10, 11), 0), SpawnLoc.transform.rotation);
        }
    }

}
