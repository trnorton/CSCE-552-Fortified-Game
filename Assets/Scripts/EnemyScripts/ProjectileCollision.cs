using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public GameObject player;
    public GameObject treasure;
    public GameObject northWall;
    public GameObject southWall;
    public GameObject eastWall;
    public GameObject westWall;

    void OnCollisionEnter(Collision col)
    {
        var playerHealthComponent = player.GetComponent<Health>();
        var nWallHealthComponent = northWall.GetComponent<Health>();
        var sWallHealthComponent = southWall.GetComponent<Health>();
        var wWallHealthComponent = westWall.GetComponent<Health>();
        var eWallHealthComponent = eastWall.GetComponent<Health>();
        var treasureHealthComponent = treasure.GetComponent<Health>();
        
        if(col.gameObject.name == "WestWall")
        {
            if(wWallHealthComponent != null)
            {
                wWallHealthComponent.TakeDamage(1);
            }
        }

        if(col.gameObject.name == "EastWall")
        {
            if(eWallHealthComponent != null)
            {
                eWallHealthComponent.TakeDamage(1);
            }
        }

        if(col.gameObject.name == "NorthWall")
        {
            if(nWallHealthComponent != null)
            {
                nWallHealthComponent.TakeDamage(1);
            }
        }

        if(col.gameObject.name == "SouthWall")
        {
            if(sWallHealthComponent != null)
            {
                sWallHealthComponent.TakeDamage(1);
            }
        }

        if(col.gameObject.name == "Player")
        {
            if(playerHealthComponent != null)
            {
                playerHealthComponent.TakeDamage(1);
            }
        }

        if(col.gameObject.name == "Treasure")
        {
            if(treasureHealthComponent != null)
            {
                treasureHealthComponent.TakeDamage(1);
            }
        }
        Destroy(this.gameObject);
    }
}
