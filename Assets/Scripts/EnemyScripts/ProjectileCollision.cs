using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float deathTimer;

    void Start()
    {
        StartCoroutine(Timer());
        Physics.IgnoreLayerCollision(13,12);
    }
    void OnCollisionEnter(Collision col)
    {
 
        if(col.gameObject.tag == "Wall")
        {
            projectileDie();
        }
        if(col.gameObject.tag == "Player")
        {
            projectileDie();
        }

        if(col.gameObject.tag == "Treasure")
        {
            projectileDie();
        }
        if(col.gameObject.tag == "Enemy")
        {
            //projectileDie();
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
