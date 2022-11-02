using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float deathTimer;

    void Start()
    {
        StartCoroutine(Timer());
    }
    void OnCollisionEnter(Collision col)
    {
 
        if(col.gameObject.name == "WestWall")
        {
            projectileDie();
        }

        if(col.gameObject.name == "EastWall")
        {
            projectileDie();
        }

        if(col.gameObject.name == "NorthWall")
        {
            projectileDie();
        }

        if(col.gameObject.name == "SouthWall")
        {
            projectileDie();
        }

        if(col.gameObject.name == "Player")
        {
            projectileDie();
        }

        if(col.gameObject.name == "Treasure")
        {
            projectileDie();
        }

        if(col.gameObject.layer == 8)
        {
            projectileDie();
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(deathTimer);
        projectileDie();
    }
    void projectileDie()
    {
        Destroy(gameObject);
    }
}
