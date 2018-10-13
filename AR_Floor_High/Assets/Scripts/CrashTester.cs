using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTester : MonoBehaviour {
    public GameObject message;
    public AudioSource audio;
    public AudioClip otherClip;
    public AudioClip firstClip;
    public GameObject blood;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            message.SetActive(false);
            blood.SetActive(false);
            audio.clip = firstClip;
            audio.Play();
        }

    }

    private void OnTriggerEnter(Collider other) {

            message.SetActive(true);
            blood.SetActive(true);
            audio.clip = otherClip;
            audio.Play();

    }
}
