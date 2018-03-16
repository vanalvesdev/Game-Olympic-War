using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {
    public int damage;
    public Vector2 direction;
    public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(direction != null)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
	}


    void OnBecameInvisible()
    {
        DestroyObject(this.gameObject);
    }
}
