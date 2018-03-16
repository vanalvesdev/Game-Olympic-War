using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salvar : MonoBehaviour {

    public AudioClip somEscape;
    private AudioController audioController;

    void Awake()
    {
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    void voltarMenu()
    {
        audioController.playAudio(somEscape);
        SceneManager.LoadScene("Menu");
    }
}
