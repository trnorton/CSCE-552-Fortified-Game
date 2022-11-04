using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bat; //add more weapons later
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                BatAttack();
            }
        }
    }

    public void BatAttack()
    {
        isAttacking = true;
        CanAttack = false;
        Animator anim = Bat.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(resetAttackCD());
    }

    IEnumerator resetAttackCD()
    {
        StartCoroutine(resetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator resetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
