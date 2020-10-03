//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Camera theCamera;
    GameManager gamemanager;

    void Start() {
        
    }

    void Update() {
        if (gamemanager == null) {
            gamemanager = GameObject.FindObjectOfType<GameManager>();
        }
        handleKeyboard();
        
    }


    private void handleKeyboard() {
        Vector3 vectMoveCamera;
        float fCameraMoveSpeed = 2f;

        vectMoveCamera = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Time.deltaTime * fCameraMoveSpeed;
        theCamera.transform.Translate(vectMoveCamera, Space.World);


        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) {
            gamemanager.pauseGame();
        }


    }
}