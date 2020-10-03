//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public TableCell TableCellPrefab;
    public int iSelectedPiece;
    public int iSelectedRotation;
    public GameObject PieceBallGeneratorPrefab;
    public GameObject PieceBridgePrefab;
    public GameObject PieceBridgeTurnPrefab;
    public GameObject PieceZipPrefab;
    public List<GameObject> PreviewPieces;
    public GameObject CreatedPieces;
    public GameObject TableCells;
    public GameObject Balls;

    public Text TextSelectedPiece;
    public Text TextSelectedRotation;
    public Text TextLoops;

    int iLoops;

    void Start() {
        iSelectedPiece = 0;
        iSelectedRotation = 0;
        makeTable();

        iLoops = 0;
        
    }

    void Update() {
        
    }

    private void makeTable() {
        int i, j;
        for (i = 0; i < 10; i++) {
            for (j = 0; j < 10; j++) {
                TableCell tablecell = Instantiate(TableCellPrefab, new Vector3((float)j, 0f, (float)i), Quaternion.identity).GetComponent<TableCell>();
                tablecell.transform.SetParent(TableCells.transform);
                tablecell.iRow = i;
                tablecell.iCol = j;
            }
        }


    }

    public void createPiece(int iRow, int iCol) {
        float fRotation = iSelectedRotation * 90f;

        switch(iSelectedPiece) {
            case 0:
                //                Instantiate(PieceBallGeneratorPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.identity);
                PieceBallGenerator ballgenerator = Instantiate(PieceBallGeneratorPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceBallGenerator>() ;
                ballgenerator.transform.SetParent(CreatedPieces.transform);
                break;
            case 1:
                PieceBridge bridge = Instantiate(PieceBridgePrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceBridge>();
                bridge.transform.SetParent(CreatedPieces.transform);
                break;
            case 2:
                PieceBridgeTurn bridgeturn = Instantiate(PieceBridgeTurnPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceBridgeTurn>();
                bridgeturn.transform.SetParent(CreatedPieces.transform);
                break;
            case 3:
                PieceZip zip = Instantiate(PieceZipPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceZip>();
                zip.transform.SetParent(CreatedPieces.transform);
                break;

        }
    }

    public void setSelectedPiece(int iPiece) {
        string strDisplay = "Selected Piece: ";
        switch (iPiece) {
            case 0:
                iSelectedPiece = iPiece;
                strDisplay += "Ball Generator";
                break;
            case 1:
                iSelectedPiece = iPiece;
                strDisplay += "Bridge";
                break;
            case 2:
                iSelectedPiece = iPiece;
                strDisplay += "Bridge Turn";
                break;
            case 3:
                iSelectedPiece = iPiece;
                strDisplay += "Zip";
                break;
        }

        TextSelectedPiece.text = strDisplay;
    }

    public void incrementRotation() {
        iSelectedRotation += 1;
        if (iSelectedRotation >= 4) {
            iSelectedRotation = iSelectedRotation % 4;
        }
        TextSelectedRotation.text = "Rotation " + (iSelectedRotation * 90);
    }

    public void showPreviewPiece(bool isShowing, int iRow, int iCol) {
        foreach (GameObject gobj in PreviewPieces) {
            gobj.SetActive(false);
        }

        if (isShowing) {
            float fRotation = iSelectedRotation * 90f;
            PreviewPieces[iSelectedPiece].transform.position = new Vector3(iCol, 0f, iRow);
            PreviewPieces[iSelectedPiece].transform.rotation = Quaternion.Euler(0f, fRotation, 0f);
            PreviewPieces[iSelectedPiece].SetActive(true);


        } 
    }

    public void resetBalls() {
        foreach (Ball ball in Balls.GetComponentsInChildren<Ball>()) {
            Destroy(ball.gameObject);
        }

        foreach (PieceBallGenerator ballgenerator in GameObject.FindObjectsOfType<PieceBallGenerator>()) {
            ballgenerator.reset();
        }

    }

    public void resetPieces() {
        foreach (Piece piece in CreatedPieces.GetComponentsInChildren<Piece>()) {
            Destroy(piece.gameObject);
        }

        foreach (TableCell tablecell in TableCells.GetComponentsInChildren<TableCell>()) {
            tablecell.isFilled = false;
        }
    }

    public void incrementLoops() {
        iLoops++;
        TextLoops.text = "Loops: " + iLoops;

    }


}