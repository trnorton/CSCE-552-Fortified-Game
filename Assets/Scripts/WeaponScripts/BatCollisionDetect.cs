using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollisionDetect : MonoBehaviour
{
    public WeaponController wc;

    private void OnCollisionEnter(Collision col)
    {
        GameObject collsionGameObject = col.gameObject;
        var healComponent = collsionGameObject.GetComponent<Health>();
        Debug.Log(healComponent.currentHealth);
        if(collsionGameObject.tag == "Enemy" && wc.isAttacking)
        {
            healComponent.TakeDamage(1);
        }
    }
}
