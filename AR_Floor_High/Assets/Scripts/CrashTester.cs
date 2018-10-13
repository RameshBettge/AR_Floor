using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTester : MonoBehaviour {
    public GameObject message;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {

            message.SetActive(true);
        
    }
}
