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
        var HealthComponent = col.gameObject.GetComponent<Health>();
        if(col.gameObject.tag == "Wall")
        {
            
            projectileDie();
        }
        if(col.gameObject.tag == "Player")
        {
            HealthComponent.TakeDamage(1);
            projectileDie();
        }

        if(col.gameObject.tag == "Treasure")
        {
            HealthComponent.TakeDamage(1);
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
        if(col.gameObject.tag == "Friendly")
        {
            
            HealthComponent.TakeDamage(1);
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
