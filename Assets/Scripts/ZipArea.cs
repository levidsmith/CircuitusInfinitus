//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipArea : MonoBehaviour {
    void Start() {
        
    }

    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        Ball b = other.GetComponent<Ball>();
        if (b != null) {
            Rigidbody rigidbody = b.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 20f);
        }
    }
}