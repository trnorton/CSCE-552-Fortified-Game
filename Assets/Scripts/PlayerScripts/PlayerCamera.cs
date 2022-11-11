using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float xSens;
    public float ySens;

    public Transform orientation;
    float xRotation;
    float yRotation;
    private bool shopOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySens;

        yRotation += mouseX;
        xRotation -= mouseY;
        //Make so we can't look up or down more than 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Cam rotate and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        

        /*
        if(!shopOpen){
            //mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSens;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySens;

            yRotation += mouseX;
            xRotation -= mouseY;
            //Make so we can't look up or down more than 90 degrees
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //Cam rotate and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        }*/
    }
    public void toggleShop(){
        shopOpen = !shopOpen;
    }
}
