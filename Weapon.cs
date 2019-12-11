using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    bool fightBtnDown = false;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
        */

        if (fightBtnDown) //mobile
        {
            Shoot();
            fightBtnDown = false;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void FightBtnDown() //mobile
    {
        fightBtnDown = true;
    }
}
