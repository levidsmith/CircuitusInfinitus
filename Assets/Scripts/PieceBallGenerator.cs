//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBallGenerator : Piece {

    public Ball BallPrefab;
    float fBallGenerateCountdown;
    const float GENERATE_COUNTDOWN_MAX = 2f;
    GameManager gamemanager;
    int iBallsLeft;
    const int MAX_BALLS = 3;

    void Start() {
        fBallGenerateCountdown = GENERATE_COUNTDOWN_MAX;
        //iBallsLeft = MAX_BALLS;
        iBallsLeft = 0;
        
    }

    void Update() {
        if (gamemanager == null) {
            gamemanager = GameObject.FindObjectOfType<GameManager>();
        }
        fBallGenerateCountdown -= Time.deltaTime;
        if ((fBallGenerateCountdown <= 0f) && (iBallsLeft > 0)) {
            Ball ball = Instantiate(BallPrefab, new Vector3(transform.position.x, 1.4f, transform.position.z), Quaternion.identity);

            //ball.setBallTexture(gamemanager.matBalls[Random.Range(0, 15)]);
            ball.setBallTexture(gamemanager.matBalls[gamemanager.getBallNumber()]);

            //ball.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, 10f));
            ball.transform.SetParent(gamemanager.Balls.transform);
//            ball.GetComponent<Rigidbody>().AddForce(transform.forward * 50f);

            iBallsLeft--;
            fBallGenerateCountdown += GENERATE_COUNTDOWN_MAX;
            gamemanager.soundeffects.soundBallGenerate.Play();
        }

        
    }

    public void spawnBalls() {
        iBallsLeft = MAX_BALLS;
        fBallGenerateCountdown = 0;
    }
}