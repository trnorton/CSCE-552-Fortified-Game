using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject prefab;

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
            Instantiate(prefab, this.transform.position, this.transform.rotation);
            
            Destroy(this.gameObject);
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
