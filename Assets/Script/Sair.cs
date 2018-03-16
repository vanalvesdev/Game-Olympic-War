using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sair : MonoBehaviour {

    public AudioClip somEscape;
    private AudioController audioController;
    // Use this for initialization
    void Awake () {
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void sair()
    {
        Application.Quit();
    }

    void voltarMenu()
    {
        audioController.playAudio(somEscape);
        SceneManager.LoadScene("Menu");
    }
}
