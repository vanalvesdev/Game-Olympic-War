using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private AudioSource audioSource;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void playAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
