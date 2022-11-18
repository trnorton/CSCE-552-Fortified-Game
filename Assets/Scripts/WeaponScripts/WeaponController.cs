using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Prim; //add more weapons later
    public GameObject Secondary;

    public GameObject[] Weapons;
    private bool isPrimActive = true;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;
    public bool noSecondary = true;

    //For Slingshot
    public GameObject projectile;
    public Transform firePoint;
    public float fireSpeed;

    public GameObject PauseUI;


    void Start()
    {
        foreach(GameObject g in Weapons){
            g.SetActive(false);
        }
        GameObject Bat = getWeaponByTag("Bat");
        GameObject Hands = getWeaponByTag("PH");
        Bat.SetActive(true);
        Hands.SetActive(true);
        Prim = Bat;
        Secondary = Hands;
    }

    void Update()
    {
        swapWeapons();
        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack && isPrimActive)
            {
                PrimAttack();
            }
            else if(CanAttack && !isPrimActive && !PauseUI.activeSelf)
            {
                SecAttack();
            }
        }
    }

    public void PrimAttack()
    {
        isAttacking = true;
        CanAttack = false;
        Debug.Log(Prim.tag);
        Animator anim = Prim.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(resetAttackCD());
    }

    public void SecAttack()
    {
        if(Secondary.tag == "Slingshot"){
            isAttacking = true;
            CanAttack = false;
            StartCoroutine(fireSling());
            StartCoroutine(resetAttackCD());
        }
    }

    IEnumerator resetAttackCD()
    {
        StartCoroutine(resetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
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
                Prim.SetActive(isPrimActive);
                Secondary.SetActive(!isPrimActive);
            }
            else if(!isPrimActive)
            {
                isPrimActive = !isPrimActive;
                Prim.SetActive(isPrimActive);
                Secondary.SetActive(!isPrimActive);
            }
        }
    }

    public GameObject getWeaponByTag(string str){
        foreach(GameObject g in Weapons){
            if(g.tag == str){
                return g;
            }
        }
        return null;
    }

    public GameObject getPrim(){
        return Prim;
    }
    public GameObject getSec(){
        return Secondary;
    }

    public void upgradePrim(string taggy){
        Prim = getWeaponByTag(taggy);
    }

    public void upgradeSec(string taggy){
        Secondary =  getWeaponByTag(taggy);
    }
}
