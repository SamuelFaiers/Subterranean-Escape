using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Canvas CRCanvas;                                                                 //Credits Canvas

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel()                                                                 //Function that loads the main game when the "Play" button is pressed
    {
        SceneManager.LoadScene("Level01");
    }

    public void ShowCredits()                                                               //Function that shows the credits when the "Credits" button is pressed
    {
        CRCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideCredits()                                                               //Function that hides the credits when the "Back" button is pressed
    {
        CRCanvas.GetComponent<Canvas>().enabled = false;
    }
}
