using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{

    public GameObject shopInterface;
    public GameObject shopIndicator;
    public GameObject cam;
    public KeyCode openMenuKey = KeyCode.F;
    public KeyCode closeMenuKey = KeyCode.F;

    // Start is called before the first frame update
    void Start()
    {
        shopInterface.SetActive(false);
        shopIndicator.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(closeMenuKey)){
            Cursor.lockState = CursorLockMode.Locked;
            shopInterface.SetActive(false);
            Cursor.visible = false;
            //cam.GetComponent<PlayerCamera>().toggleShop();
        }
    }

void OnTriggerEnter(Collider other){
    shopIndicator.SetActive(true);
}
void OnTriggerExit(Collider other){
    shopIndicator.SetActive(false);
}
    void OnTriggerStay(Collider other){
        if(Input.GetKey(openMenuKey)){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //cam.GetComponent<PlayerCamera>().toggleShop();
            shopInterface.SetActive(true);
            shopIndicator.SetActive(false);
        }
    }
}
