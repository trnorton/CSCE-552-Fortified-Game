using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{

    public GameObject shopInterface;
    public GameObject shopIndicator;
    public KeyCode openMenuKey = KeyCode.F;
    public KeyCode closeMenuKey = KeyCode.Escape;

    // Start is called before the first frame update
    void Start()
    {
        shopInterface.SetActive(false);
        shopIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(closeMenuKey)){
            shopInterface.SetActive(false);
        }
    }

void OnTriggerEnter(Collider other){
    shopIndicator.SetActive(true);
}
void OnTriggerExit(Collider other){
    shopIndicator.SetActive(false);
}
    void OnTriggerStay(Collider other){
        //shopIndicator.SetActive(false);
        if(Input.GetKey(openMenuKey)){
            shopInterface.SetActive(true);
            shopIndicator.SetActive(false);    
        }
    }
}
