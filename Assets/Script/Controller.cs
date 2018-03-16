using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    public float contador;
    private float currentTime;
    public Text texto;
    // Use this for initialization
    void Awake () {
    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        currentTime += Time.deltaTime;

        if(currentTime >= 1f)
        {
            currentTime = 0;
            contador--;
            texto.text = contador.ToString();
        }
    }

    public void MaxVenceu()
    {
        SceneManager.LoadScene("VoceGanhou");
    }

    public void MaxPerdeu()
    {
        SceneManager.LoadScene("VocePerdeu");
    }
}
