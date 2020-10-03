//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
    GameManager gamemanager;
    void Start() {
        
    }

    void Update() {
        if (gamemanager == null) {
            gamemanager = GameObject.FindObjectOfType<GameManager>();
        }

        handleMouseInput();
        
    }

    private void handleMouseInput() {
        if (Input.GetMouseButtonDown(1)) {
            doRotate();
        }
    }

    public void doBallGeneratorSelected() {
        gamemanager.setSelectedPiece(0);

    }

    public void doBridgeSelected() {
        gamemanager.setSelectedPiece(1);
    }

    public void doBridgeTurnSelected() {
        gamemanager.setSelectedPiece(2);
    }

    public void doZipSelected() {
        gamemanager.setSelectedPiece(3);
    }


    public void doRotate() {
        gamemanager.incrementRotation();
    }

    public void doResetBalls() {
        gamemanager.resetBalls();
    }

    public void doResetPieces() {
        gamemanager.resetPieces();
    }

    public void doNextLevel() {
        gamemanager.nextLevel();
    }

    public void doReturnToTitle() {
        SceneManager.LoadScene("title");
    }
}