using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MusicControl : MonoBehaviour {

    public static MusicControl instance = null;

    public AudioSource audioSource;

    //public AudioClip mainMusic;
    //public AudioClip timerMusic;

	void Start () {

	}

    void Awake()
    {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
    }
	
	void Update () {
	
	}
}
