using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamge : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        var healComponent = GetComponent<Health>();
        GameObject collsionGameObject = col.gameObject;
        if(collsionGameObject.tag == "Projectile")
            healComponent.TakeDamage(1);
    }
}
