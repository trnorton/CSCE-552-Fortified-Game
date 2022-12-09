using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject prefab;
    public GameObject damagedprefab;
    public int destructionLevel;
    
    //adding this to fix my warriorcount
    private GameObject buyFriendly;


    // Start is called before the first frame update
    void Start()
    {
        buyFriendly = GameObject.Find("WeaponShopHUD");
        currentHealth = maxHealth;
    }
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= maxHealth/2 && this.gameObject.tag == "Wall" && destructionLevel == 0)
        {
            Instantiate(damagedprefab, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        if(currentHealth == 0 && this.gameObject.tag == "Wall" && destructionLevel == 1)
        {
            Instantiate(prefab, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);
        }
        else if(currentHealth == 0 && this.gameObject.tag == "Player")
        {
            Debug.Log("You lose!!");
            SceneManager.LoadScene("GameOverScene");
        }
        else if(currentHealth == 0 && this.gameObject.tag == "Treasure")
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("GameOverScene");
        }
        else if(currentHealth == 0 && this.gameObject.tag == "Friendly")
        {
            buyFriendly.GetComponent<buywarrior>().warriorcount --;
            Destroy(this.gameObject);
        }
    }
    public float HealthtoInt()
    {
        return currentHealth;
    }
    public float MaxHealthtoInt()
    {
      return maxHealth;
    }
    public void HealToMax()
    {
      currentHealth = maxHealth;
    }

}
