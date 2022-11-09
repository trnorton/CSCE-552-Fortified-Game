using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
     
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth == 0 && this.gameObject.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
        else if(currentHealth == 0 && this.gameObject.tag == "Player")
        {
            Debug.Log("You lose!!");
        }
        else if(currentHealth == 0 && this.gameObject.tag == "Treasure")
        {
            this.gameObject.SetActive(false);
        }
    }
    public int HealthtoInt()
    {
        return currentHealth;
    }
}
