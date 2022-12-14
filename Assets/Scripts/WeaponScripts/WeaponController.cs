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
    public GameObject player_arrow;
    public Transform firePoint;
    private float bowFireSpeed = 3000f;
    private float slingFireSpeed = 1750f;

    //Audio Sources
    public AudioSource A1;
    public AudioSource A2;
    public AudioSource A3;
    public AudioSource A4;
    public AudioSource A5;

    public GameObject PauseUI;


    void Start()
    {
        foreach(GameObject g in Weapons){
            g.SetActive(false);
        }
        GameObject Bat = getWeaponByTag("Bat");
        GameObject Hands = getWeaponByTag("PH");
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
        //play sound
        if(Prim.tag == "Bat"){
            A1.Play();
        } else if(Prim.tag == "Sword"){
            A2.Play();
        } else if(Prim.tag == "LaserSword"){
            A3.Play();
        }
        StartCoroutine(resetAttackCD());
    }

    public void SecAttack()
    {
        if(Secondary.tag == "Slingshot"){
            isAttacking = true;
            CanAttack = false;
            Animator anim = Secondary.GetComponent<Animator>();
            anim.SetTrigger("Attack");
            StartCoroutine(fireSling());
            A4.Play();
            StartCoroutine(resetAttackCD());
        } 
        else if(Secondary.tag == "Bow"){
            isAttacking = true;
            CanAttack = false;
            Animator anim = Secondary.GetComponent<Animator>();
            anim.SetTrigger("Attack");
            StartCoroutine(fireBow());
            A5.Play();
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
        yield return new WaitForSeconds(0.75F);
        GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * slingFireSpeed);
        yield return new WaitForSeconds(AttackCooldown);
    }
    IEnumerator fireBow()
    {
        yield return new WaitForSeconds(0.75F);
        GameObject newProjectile = Instantiate(player_arrow, firePoint.position, firePoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * bowFireSpeed);
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
        if(getWeaponByTag(taggy))
            Prim = getWeaponByTag(taggy);
    }

    public void upgradeSec(string taggy){
        if(getWeaponByTag(taggy))
            Secondary =  getWeaponByTag(taggy);
    }

    public void hideWeapons(bool b){
        Prim.SetActive(b);
        Secondary.SetActive(b);
    }
}
