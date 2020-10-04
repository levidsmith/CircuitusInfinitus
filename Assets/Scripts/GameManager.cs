//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject PanelLevelCompleted;
    public GameObject PanelGameCompleted;

    public Text TextSelectedPiece;
    public Text TextSelectedRotation;
    public Text TextLoops;
    public Text TextLevel;

    public Text TextBallGeneratorCount;
    public Text TextBridgeCount;
    public Text TextBridgeTurnCount;
    public Text TextZipCount;

    public List<Material> matBalls;
    public List<int> listBallNumbers;

    int iLoops;
    int iLoopsRequired;
    bool isCompleted;
    public int iCurrentLevel;
    const int NUM_LEVELS = 3;

    Dictionary <string, int> dictPieceCount;
    public SoundEffects soundeffects;
    

    void Start() {
        iSelectedPiece = 0;
        iSelectedRotation = 0;
        iCurrentLevel = 0;
        dictPieceCount = new Dictionary<string, int>();
        listBallNumbers = new List<int>();
        makeTable();
        setSelectedPiece(0);

        resetLevel();


    }

    public void resetLevel() {
        setupLevel(iCurrentLevel);
        resetBalls();
        resetPieces();
        iLoops = 0;
        isCompleted = false;
        PanelLevelCompleted.SetActive(false);
        PanelGameCompleted.SetActive(false);
        updateUIDisplay();

    }

    private void setupLevel(int iLevel) {
        dictPieceCount.Clear();

        switch (iLevel) {
            case 0:
                iLoopsRequired = 20;
                dictPieceCount.Add("ballgenerator", 1);
                dictPieceCount.Add("bridge", 10);
                dictPieceCount.Add("bridgeturn", 8);
                dictPieceCount.Add("zip", 5);
                break;
            case 1:
                iLoopsRequired = 50;
                dictPieceCount.Add("ballgenerator", 1);
                dictPieceCount.Add("bridge", 20);
                dictPieceCount.Add("bridgeturn", 16);
                dictPieceCount.Add("zip", 10);
                break;
            case 2:
                iLoopsRequired = 100;
                dictPieceCount.Add("ballgenerator", 1);
                dictPieceCount.Add("bridge", 25);
                dictPieceCount.Add("bridgeturn", 18);
                dictPieceCount.Add("zip", 20);
                break;
        }

    }

    void Update() {
        checkCompleted();
        
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

    public bool createPiece(int iRow, int iCol) {
        bool isPieceCreated;
        float fRotation = iSelectedRotation * 90f;
        isPieceCreated = false;

        switch(iSelectedPiece) {
            case 0:
                if (dictPieceCount["ballgenerator"] > 0) {
                    PieceBallGenerator ballgenerator = Instantiate(PieceBallGeneratorPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceBallGenerator>();
                    ballgenerator.transform.SetParent(CreatedPieces.transform);
                    dictPieceCount["ballgenerator"]--;
                    isPieceCreated = true;
                }
                break;
            case 1:
                if (dictPieceCount["bridge"] > 0) {
                    PieceBridge bridge = Instantiate(PieceBridgePrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceBridge>();
                    bridge.transform.SetParent(CreatedPieces.transform);
                    dictPieceCount["bridge"]--;
                    isPieceCreated = true;
                }
                break;
            case 2:
                if (dictPieceCount["bridgeturn"] > 0) {
                    PieceBridgeTurn bridgeturn = Instantiate(PieceBridgeTurnPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceBridgeTurn>();
                    bridgeturn.transform.SetParent(CreatedPieces.transform);
                    dictPieceCount["bridgeturn"]--;
                    isPieceCreated = true;
                }
                break;
            case 3:
                if (dictPieceCount["zip"] > 0) {
                    PieceZip zip = Instantiate(PieceZipPrefab, new Vector3((float)iCol, 0f, (float)iRow), Quaternion.Euler(0f, fRotation, 0f)).GetComponent<PieceZip>();
                    zip.transform.SetParent(CreatedPieces.transform);
                    dictPieceCount["zip"]--;
                    isPieceCreated = true;
                }
                break;

        }

        if (isPieceCreated) {
            soundeffects.soundCreatePiece.Play();
        } else {
            soundeffects.soundNegative.Play();
        }

        updateUIDisplay();
        return isPieceCreated;
    }

    public void setSelectedPiece(int iPiece) {
        string strDisplay = "Selected Piece:\n";
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
        soundeffects.soundClick.Play();
    }

    public void incrementRotation() {
        iSelectedRotation += 1;
        if (iSelectedRotation >= 4) {
            iSelectedRotation = iSelectedRotation % 4;
        }
        TextSelectedRotation.text = "Rotation " + (iSelectedRotation * 90);
        soundeffects.soundRotate.Play();
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
            ballgenerator.spawnBalls();
        }

        iLoops = 0;
        updateUIDisplay();

    }

    public void resetPieces() {
        foreach (Piece piece in CreatedPieces.GetComponentsInChildren<Piece>()) {
            Destroy(piece.gameObject);
        }

        foreach (TableCell tablecell in TableCells.GetComponentsInChildren<TableCell>()) {
            tablecell.isFilled = false;
        }

        setupLevel(iCurrentLevel);
        updateUIDisplay();
    }

    public void incrementLoops() {
        iLoops++;
        soundeffects.soundLoop.Play();
        updateUIDisplay();
    }

    private void updateUIDisplay() {
        TextLoops.text = "Loops " + iLoops + " / " + iLoopsRequired;
        TextLevel.text = "Level " + (iCurrentLevel + 1);
        TextBallGeneratorCount.text = dictPieceCount["ballgenerator"].ToString();
        TextBridgeCount.text = dictPieceCount["bridge"].ToString();
        TextBridgeTurnCount.text = dictPieceCount["bridgeturn"].ToString();
        TextZipCount.text = dictPieceCount["zip"].ToString();

    }


    private void checkCompleted() {
        if (!isCompleted && iLoops >= iLoopsRequired) {
            isCompleted = true;
            if (iCurrentLevel < NUM_LEVELS - 1) { 
                PanelLevelCompleted.SetActive(true);
            } else {
                PanelGameCompleted.SetActive(true);
            }

        }

    }

    public void nextLevel() {
        iCurrentLevel++;
        resetLevel();

    }

    public void pauseGame() {
        SceneManager.LoadScene("title");
    }

    public int getBallNumber() {
        int iBallNumber = 0;

        if (listBallNumbers.Count <= 0) {
            int i;
            for (i = 0; i < 15; i++) {
                listBallNumbers.Add(i);
            }
        }

        int iIndex = Random.Range(0, listBallNumbers.Count);
        iBallNumber = listBallNumbers[iIndex];
        listBallNumbers.RemoveAt(iIndex);

        return iBallNumber;
    }


}