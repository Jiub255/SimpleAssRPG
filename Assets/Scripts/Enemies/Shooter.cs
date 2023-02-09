using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
   // public GameObject projectile;
    public float shootRadius = 4f;
    private Transform target;
    public float shootTimerLength = .5f;
    private float shootTimer;
    public bool canShoot = true;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

   /* protected override*/ void Update()
    {
        //base.Update();
        if (canShoot == false) //timer not working either? update overridden by enemy script?
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                canShoot = true;
            }
        }

        else
        {
            //Debug.Log(Vector2.Distance(target.position, transform.position));
            if (Vector2.Distance(target.position, transform.position) <= shootRadius)
            {
                canShoot = false;
                shootTimer = shootTimerLength;
                //Debug.Log("In range and canShoot true"); 

                Vector2 towardTarget = target.transform.position - transform.position;
                towardTarget.Normalize();
                //Vector3 spawnPosition = transform.position + Vector3.up;

                //GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                GameObject currentProjectile = ObjectPool.SharedInstance.GetPooledObject("Red Projectile");
                if(currentProjectile != null)
                {
                    currentProjectile.transform.position = transform.position;
                    currentProjectile.transform.rotation = transform.rotation;
                    currentProjectile.SetActive(true);
                }

                currentProjectile.GetComponent<Projectile>().Launch(towardTarget);
            }
        }
    }
}
