using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bat; //add more weapons later
    public GameObject Secondary;
    private bool isPrimActive = true;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;

    //For Slingshot
    public GameObject projectile;
    public Transform firePoint;
    public float fireSpeed;


    void Start()
    {
        Secondary.SetActive(false);
    }

    void Update()
    {
        swapWeapons();
        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack && isPrimActive)
            {
                BatAttack();
            }
            else if(CanAttack && !isPrimActive)
            {
                SlingAttack();
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

    public void SlingAttack()
    {
        isAttacking = true;
        CanAttack = false;
        StartCoroutine(fireSling());
        StartCoroutine(resetAttackCD());
    }

    IEnumerator resetAttackCD()
    {
        StartCoroutine(resetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false; //!
        CanAttack = true;
    }

    IEnumerator resetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    IEnumerator fireSling()
    {
        GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * fireSpeed);
        yield return new WaitForSeconds(AttackCooldown);
    }

    void swapWeapons()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(isPrimActive)
            {
                isPrimActive = !isPrimActive;
                Bat.SetActive(isPrimActive);
                Secondary.SetActive(!isPrimActive);
            }
            else if(!isPrimActive)
            {
                isPrimActive = !isPrimActive;
                Bat.SetActive(isPrimActive);
                Secondary.SetActive(!isPrimActive);
            }
        }
    }
}