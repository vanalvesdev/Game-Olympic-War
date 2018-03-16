using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]
public class PlayVideo : MonoBehaviour {
#if !UNITY_WEBGL && !UNITY_IOS
    public MovieTexture movie;
    private AudioSource audio;
	// Use this for initialization
	void Awake () {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 28;
	}
	
	// Update is called once per frame
	void Start () {
        
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
	}

    void Update()
    {
        if(!movie.isPlaying  || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
#endif
}
