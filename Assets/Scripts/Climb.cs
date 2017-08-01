using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Climb : MonoBehaviour {

    private bool canClimb;
    private FirstPersonController controller;                                                       //This will be used to set the FPS Controller to false when the climb animation is played
    public Animator anim;                                                                           //Drag the "ClimbCam" from the hierarchy to this variable
    public Camera climbCam;                                                                         //Drag the "ClimbCam" from the hierarchy to this variable
    public Camera mainCam;                                                                          //Drag the "FirstPersonCharacter" from the hierarchy to this variable
    public GameObject keyPrompt;                                                                    //This will prompt the player to press E when they are near a climeable ledge

    // Use this for initialization
    void Start () {
        keyPrompt.SetActive(false);
        controller = GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (canClimb && Input.GetKeyDown(KeyCode.E))                                                //if the player can climb the object and the E key is pressed
        {
            mainCam.depth = 0;
            climbCam.depth = 1;
            controller.enabled = false;                                                             //Set FPS controller to false
            anim.SetTrigger("Climb");                                                               //play the climb animation
            StartCoroutine(afterClimb());
        }
	}

    IEnumerator afterClimb ()                                                                      //After the key is pressed, wait the length of the animation and reset the changes
    {
        yield return new WaitForSeconds(1);
        mainCam.depth = 1;
        climbCam.depth = 0;
        controller.enabled = true;
        transform.position = climbCam.transform.position;                                          //Transform position to that of the climbCam which was moved in the animation
    }

    void OnTriggerEnter (Collider other)                                                           //If the player enters the trigger ledge
    {
        if(other.tag == "Climb")                                                                   //And its tag is "Climb"
        {
            canClimb = true;                                                                       //Set canClimb to true
            keyPrompt.SetActive(true);                                                             //Prompt the player to press the "E" key
        }
    }

    void OnTriggerExit(Collider other)                                                            //If they exit the trigger ledge
    {
        canClimb = false;                                                                         //Set canClimb to false
        keyPrompt.SetActive(false);                                                               //Deactivate the "E" key prompt
    }

}
