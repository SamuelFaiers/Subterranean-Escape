using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour {
    public Transform canvas;
    public Transform Player;
    public bool isPause;
    public AudioSource openMenu;

    void Start()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;      
        Player.GetComponent<FirstPersonController>().enabled = true;
        isPause = false;
    }


    // Update is called once per frame
    void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
	}
    public void Pause()
    {
        openMenu.Play();

        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            Player.GetComponent<FirstPersonController>().enabled = false;
            Player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
            isPause = true;
        }
        else if (canvas.gameObject.activeInHierarchy == true)
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            Player.GetComponent<FirstPersonController>().enabled = true;
            Player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(true);
            isPause = false;
        }
    }
}
