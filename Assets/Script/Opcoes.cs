using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opcoes : MonoBehaviour {

    public AudioClip somEscape;
    private AudioController audioController;
    // Use this for initialization
    void Awake () {
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }


    void voltarMenu()
    {
        audioController.playAudio(somEscape);
        SceneManager.LoadScene("Menu");
    }
}
