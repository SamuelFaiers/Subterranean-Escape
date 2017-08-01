using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Timer : MonoBehaviour {

    private FirstPersonController player;                                                       //variable for the player
    public GameObject TimerUI;                                                                  //to be used as a reference to the TimerUI game object in the HUD Canvas
    public float timeRemaining = 180F;                                                          //initial time limit
    public Text timerText;                                                                      //reference to the text where the timer is displayed

    Animator anim;                                                                              //Animation controller for the Game Over canvs
    public Canvas GOCanvas;                                                                     //to be used as a reference to  the Game Over Canvas


	void Start () {
        player = GetComponent<FirstPersonController>();                                         //Assign player as the FirstPersonController
        TimerUI.SetActive(false);                                                               //Timer inactive until displayTimer function activates it
        anim = GOCanvas.GetComponent<Animator>();                                               //Get the animator control of the GameOverCanvas
	}
	
	void Update () {

	}

    public void displayTimer()
    {
        TimerUI.SetActive(true);                                                                //activate timer
        CountDown();                                                                            //start the CountDown function
    }

   void CountDown()
    {
        if(timeRemaining >= 0)                                                                  //if the time is above zero
        {
            timeRemaining -= Time.deltaTime;                                                    //decrement the time
            timerText.text = "" + Mathf.Round(timeRemaining) + "s";                             //and display it on the TimerUI text
        }
        else                                                                                    //if it is below zero
        {
            player.enabled = false;                                                             //Disable first person control
            player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);      //Unlock mouse cursor
            GOCanvas.GetComponent<Canvas>().enabled = true;                                     //Enable Game Over Canvas
            anim.SetTrigger("GameOver");                                                        //Play transition to Game Over Screen
        }
    }

}
