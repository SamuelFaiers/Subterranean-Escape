using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour
{

    public Camera mainCamera;
    public GameObject keyPrompt; //This will prompt the player to press E when they are looking at an interactable object
    private bool lookActive = false;
    public int numOfKeys = 0;

    public float range = 12f;

    //Used in CollectKey Function
    public AudioClip collectSound;

    //Used in CollectTreasureFunction
    public AudioClip victorySound;

    //Used in Lever Function
    public Animator leveranim;

    //Used in Pillar Function
    public Animator pillaranim;
    public GameObject pillarsound;
    public AudioSource audiosource;

    //Used in Door Function
    public Animator dooranim;
    public ParticleSystem doorparticles;

    //Pillar Particles
    public ParticleSystem pillarparticles;

    //Door 2 Function
    public Animator doortwoanim;
    public ParticleSystem doortwoparticles;
   


    // Use this for initialization
    void Start()
    {
        keyPrompt.SetActive(false);
        dooranim.SetBool("DoorState", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CollectKey();
            LeverActivate();
            DoorActivate();
            CollectTreasure();
        }

        if (lookActive == true)
        {
            Ray lookRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit lookHit;

            if (Physics.Raycast(lookRay, out lookHit, range))
            {
                if (lookHit.collider.CompareTag("Key") || lookHit.collider.CompareTag("Lever") || lookHit.collider.CompareTag("KeyHole") || lookHit.collider.CompareTag("Treasure"))
                {
                    keyPrompt.SetActive(true);
                }
                else
                {
                    keyPrompt.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LookRadius")
        {
            lookActive = true;
        }
    }

    void CollectKey()
    {
        Ray interactRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit interactHit;

        if (Physics.Raycast(interactRay, out interactHit, range))
        {
            if (interactHit.collider.CompareTag("Key"))
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                Destroy(interactHit.collider.gameObject);
                numOfKeys++;
            }
        }
        else
        {
            return;
        }
    }

    void CollectTreasure()
    {
        Ray interactRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit interactHit;

        if (Physics.Raycast(interactRay, out interactHit, range))
        {
            if (interactHit.collider.CompareTag("Treasure"))
            {                
                Destroy(interactHit.collider.gameObject);
                if (doortwoanim.GetBool("DoorState") == false)
                {
                    doortwoparticles.Emit(40); //door2 particles
                    doortwoanim.SetBool("DoorState", true); //door2 anim
                    audiosource.PlayOneShot(audiosource.clip, 0.5F); //door2 audio
                    AudioSource.PlayClipAtPoint(victorySound, transform.position); //play victory audio //needs rework
                }
            }
        }
        else
        {
            return;
        }
    }

    void LeverActivate()
    {
        Ray interactRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit interactHit;

        if (Physics.Raycast(interactRay, out interactHit, range))
        {
            if (interactHit.collider.CompareTag("Lever"))
            {
                if(leveranim.GetBool("LeverState") == false)
                { 
                    leveranim.SetBool("LeverState", true);
                    pillarparticles.Emit(100);
                    pillaranim.SetBool("PillarState", true);
                    audiosource.PlayOneShot(audiosource.clip, 0.5F);


                } else if (leveranim.GetBool("LeverState") == true)
                {
                    leveranim.SetBool("LeverState", false);
                    pillaranim.SetBool("PillarState", false);
                    audiosource.PlayOneShot(audiosource.clip, 0.5F);
                }

                
            }
        }
        else
        {
            return;
        }
    }



    void DoorActivate()
    {
        Ray interactRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit interactHit;

        if (Physics.Raycast(interactRay, out interactHit, range))
        {
            if (interactHit.collider.CompareTag("KeyHole"))
            {
                if (numOfKeys >= 2)
                {
                    if (dooranim.GetBool("DoorState") == false)
                    {
                        doorparticles.Emit(40);
                        dooranim.SetBool("DoorState", true);
                        audiosource.PlayOneShot(audiosource.clip, 0.5F);
                        numOfKeys = 0;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            return;
        }
    }

    public int DisplayKeys()
    {
        return numOfKeys;
    }
}