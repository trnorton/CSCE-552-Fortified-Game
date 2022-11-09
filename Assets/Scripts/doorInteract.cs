using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorInteract : MonoBehaviour
{
    public GameObject door;
    public GameObject doorIndicator;
    public KeyCode openKey = KeyCode.F;

    private Vector3 startRotationAngle;
    public float defaultRotationAngle = 90;

    private bool isOpen;
    private float doorActionRate = 0.0f;
    private float doorActionTime = 0.001f;
    // Start is called before the first frame update
    void Start()
    {

        isOpen = false;
        startRotationAngle = new Vector3(door.transform.localRotation.x, door.transform.localRotation.y, door.transform.localRotation.z);
        doorIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        doorIndicator.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        doorIndicator.SetActive(false);
    }
    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(openKey))
        {
            
            if(isOpen == false && Time.time > doorActionRate)
            {
                door.transform.Rotate(0, door.transform.localRotation.y + defaultRotationAngle, 0);
                isOpen = true;
                doorActionRate = Time.time + doorActionTime;
            }
            else if(isOpen == true && Time.time > doorActionRate)
            {
                door.transform.Rotate(0, -defaultRotationAngle, 0);
                isOpen = false;
                doorActionRate = Time.time + doorActionTime;
                
            }
        }
    }
}
