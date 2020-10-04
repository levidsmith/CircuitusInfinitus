//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipArea : MonoBehaviour {
    GameManager gamemanager;
    void Start() {
        
    }

    void Update() {
        if (gamemanager == null) {
            gamemanager = GameObject.FindObjectOfType<GameManager>();
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        Ball b = other.GetComponent<Ball>();
        if (b != null) {
            Rigidbody rigidbody = b.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 20f);
            gamemanager.soundeffects.soundZip.Play();
        }
    }
}