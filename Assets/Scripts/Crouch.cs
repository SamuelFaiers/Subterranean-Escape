using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {

    bool isCrouched = false;
    bool checkCeiling;
    public GameObject keyPrompt;

    CharacterController characterController;    

    void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        keyPrompt.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            if (!isCrouched) {
                crouch();
            }
        }
        else { 
            if (isCrouched  && !checkCeiling) {
                stand();
            }
        }

        if (isCrouched)
        {
            if (Physics.Raycast(transform.position, Vector3.up, 2f))
            {
                checkCeiling = true;
            }
            else
            {
                checkCeiling = false;
            }
        }
    }

    void crouch()
    {
        characterController.height /= 2;
        
        isCrouched = true;
    }
    void stand()
    {
        characterController.height *= 2;
        isCrouched = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MustCrouch")
        {
            keyPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        keyPrompt.SetActive(false);
    }
}
