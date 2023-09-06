using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_MainWeapon : MonoBehaviour
{
    [SerializeField] int WeaponLevel, startingLevel = 0;
    [SerializeField] float baseProjectileCount = 1, baseFireRate = 2f;
    public float baseDamage = 40f, baseSpeed = 10f, baseLifeTime = 3f, projectileCount, pireRate;

    public sc_normalAtk normalAttackScript;
    // Start is called before the first frame update
    void Start()
    {
        StartBallzRoutine();
        projectileCount = baseProjectileCount;
        pireRate = baseFireRate;
        WeaponLevel = startingLevel;


        if (WeaponLevel == 0)
        {
            LevelWeaponUP(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) LevelWeaponUP();
    }

    public void LevelWeaponUP()
    {
        if(WeaponLevel < 6) WeaponLevel++;

        switch (WeaponLevel)
        {
            case 1:
                normalAttackScript.damage = baseDamage;
                normalAttackScript.speed = baseSpeed;
                normalAttackScript.lifeTime = baseLifeTime;
                break;
            case 2:
                normalAttackScript.damage += normalAttackScript.damage * 10 / 100;
                break;
            case 3:
                normalAttackScript.speed += normalAttackScript.speed * 20 / 100;
                projectileCount++;
                break;
            case 4:
                normalAttackScript.damage += normalAttackScript.damage * 10 / 100;
                break;
            case 5:
                normalAttackScript.speed += normalAttackScript.speed * 20 / 100;
                break;
            case 6:
                normalAttackScript.damage += normalAttackScript.damage * 20 / 100;
                break;
        }
    }

    public void StartBallzRoutine() => StartCoroutine(Ballz());
    public void StopBallzRoutine() => StopCoroutine(Ballz());
    IEnumerator Ballz()
    {
        while (true)
        {
            yield return new WaitForSeconds(pireRate);
            
            for (int i = 0; i < projectileCount; i++)
            {
                SummonBallz();
            }
            
        }
    }
    void SummonBallz()
    {
        Instantiate(normalAttackScript.gameObject, this.transform);
    }
}
