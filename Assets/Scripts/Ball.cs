//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    void Start() {
        
    }

    public void setBallTexture(Material mat) {
        Renderer renderer = transform.GetComponentInChildren<Renderer>();
        renderer.material = mat;


    }

    void Update() {
        
    }
}