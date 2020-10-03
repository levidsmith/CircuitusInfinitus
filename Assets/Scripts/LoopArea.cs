//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopArea : MonoBehaviour {
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
            gamemanager.incrementLoops();
            
        }
    }

}