using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private AudioController audioController;
    public AudioClip somBotao;

    void Awake()
    {
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    void NovoJogo()
    {
        audioController.playAudio(somBotao);
#if UNITY_WEBGL || UNITY_IOS
        SceneManager.LoadScene("Game");
#else
        SceneManager.LoadScene("Video");
#endif
    }

    void Opcoes()
    {
        audioController.playAudio(somBotao);
        SceneManager.LoadScene("Opcoes");
    }

    void Salvar()
    {
        audioController.playAudio(somBotao);
        SceneManager.LoadScene("Salvar");
    }

    void Sair()
    {
        audioController.playAudio(somBotao);
        SceneManager.LoadScene("Sair");
    }
}
