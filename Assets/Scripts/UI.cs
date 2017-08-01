using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class UI : MonoBehaviour {

    private FirstPersonController player;                                                   //Player
    public Text keysText;                                                                   //Key number displayed on the HUD
    private bool timerActive = false;                                                       //Whether or not the timer is active
    public GameObject keyPrompt;                                                            //Keyboard prompt to turn on torch
    public GameObject jumpPrompt;                                                           //Keyboard prompt to Jump
    public GameObject sprintPrompt;                                                         //Keyboard prompt to Sprint

    public Text stage1Text;                                                                 //Pause menu text before the timer is activated
    public Text stage2Text;                                                                 //Pause menu text after the timer is activated

    Animator fadeAnim;                                                                      //Fade out animation for pop-up text
    public Canvas POCanvas;                                                                 //Pop-up UI Canvas

    Animator endAnim;                                                                       //Transition to Ending screen
    public Canvas EGCanvas;
    //End Screen Canvas


 


    void Start () {
        player = GetComponent<FirstPersonController>();                                     //Assign player as the FirstPersonController
        keyPrompt.SetActive(false);
        stage1Text.enabled = true;                                                          //Enable the Stage 1 text at the beginning of the game
        stage2Text.enabled = false;                                                         //Keep Stage 2 text disabled until the timer is activated
        fadeAnim = POCanvas.GetComponent<Animator>();                                       //Assign the Pop-up Animation controller to fadeAnim
        endAnim = EGCanvas.GetComponent<Animator>();                                        //Assign the Ending Screen Animation controller to endAnim
        POCanvas.enabled = false;                                                           //Keep the Pop-up canvas disabled until the timer is activated      
    }
	
	void Update () {
        keysText.text = GetComponent<Interact>().DisplayKeys() + "/2";                       //Have the text display the value of numOfKeys from the Interact.cs script

        if (timerActive == true)                                                            //if the timer is active
        {
            GetComponent<Timer>().displayTimer();                                           //run the function from Timer.cs that displays the timer
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FlashPrompt")                                                     //Show flashlight keyboard prompt
        {
            keyPrompt.SetActive(true);
        }
        else if (other.tag == "JumpPrompt")                                                 //Show flashlight keyboard prompt
        {
            jumpPrompt.SetActive(true);
        }
        else if (other.tag == "SprintPrompt")                                               //Show flashlight keyboard prompt
        {
            sprintPrompt.SetActive(true);
        }
        else if(other.tag == "ActivateTimer")
        {
            timerActive = true;                                                             //Activate the timer
            POCanvas.enabled = true;                                                        //Enable the pop-up message
            StartCoroutine(PopUp(5.0F));                                                    //Fade out the message after 5 seconds
            stage1Text.enabled = false;                                                     //Disable Stage1 text
            stage2Text.enabled = true;                                                      //Enable Stage2 text
            other.GetComponent<Collider>().enabled = false;                                 //Disable the collider
        }
        else if(other.tag == "DeActivateTimer")
        {
            timerActive = false;
        }
        else if(other.tag == "Finish")
        {
            player.enabled = false;                                                         //Disable FPS controller
            player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);  //Unlock the mouse cursor
            EGCanvas.GetComponent<Canvas>().enabled = true;                                 //Enable the ending screen
            endAnim.SetTrigger("EndGame");                                                  //Start the transition to the ending screen
        }
    }

    void OnTriggerExit(Collider other)
    {
        keyPrompt.SetActive(false);
        jumpPrompt.SetActive(false);
        sprintPrompt.SetActive(false);
    }

    IEnumerator PopUp(float delay)                                                          //Coroutine that will wait 5 seconds and then start to fade out the pop-up message
    {
        yield return new WaitForSeconds(delay);
        fadeAnim.SetTrigger("Fade");
    }

    public void LoadMenu()                                                                  //Load the Main Menu when the Main Menu Button(s) are pressed
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()                                                                  //Restart the level when the Restart Button(s) are pressed
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
