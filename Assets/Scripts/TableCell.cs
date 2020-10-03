//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCell : MonoBehaviour {
    bool isHighlighted;
    public bool isFilled;
    public Material matHighlight;
    public Material matRegular;
    GameManager gamemanager;
    public int iRow, iCol;

    void Start() {
        isHighlighted = false;
        isFilled = false;
        
    }

    void Update() {
        if (gamemanager == null) {
            gamemanager = GameObject.FindObjectOfType<GameManager>();
        }
        Renderer renderer = transform.Find("table_cell").GetComponent<Renderer>();
        /*
        if (isHighlighted) {
            renderer.material = matHighlight;

        } else {
            renderer.material = matRegular;
        }
        */
        
    }

    private void OnMouseOver() {
        if (gamemanager != null) {
            isHighlighted = true;
            gamemanager.showPreviewPiece(true, iRow, iCol);
        }
    }

    private void OnMouseExit() {
        isHighlighted = false;
        gamemanager.showPreviewPiece(false, iRow, iCol);
    }

    private void OnMouseDown() {
        if (!isFilled) {
            isFilled = true;
            gamemanager.createPiece(iRow, iCol);
        }
        
    }
}