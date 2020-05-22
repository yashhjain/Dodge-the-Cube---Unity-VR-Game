using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score;
    public Text scoreReference;
    private int levelUp = 1;
    private int maxLevel = 10;
    private int nextLevelScore = 10;
    private bool isDead = false;
    public GameOver gameOver;

    private string HIGHSCORE = "High Score";

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        scoreReference.text = ((int)score).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
            return;

        if(score >= nextLevelScore)
            IncreaseLevel();

        score = score + Time.deltaTime * levelUp;
        scoreReference.text = ((int)score).ToString();
    }

    void IncreaseLevel(){
        if(levelUp == maxLevel)
            return;

        nextLevelScore = nextLevelScore * 2;
        levelUp++;

        GetComponent<playerController>().ChangeSpeed(levelUp);
        Debug.Log("Level: " + levelUp);
    }
    
    public void setGameOver(){
        scoreReference.text = "DEAD";
    }

    public void persistScore(int score){
        if(score > PlayerPrefs.GetInt(HIGHSCORE)){
            PlayerPrefs.SetInt(HIGHSCORE, score);
        }
    }

    public void OnDeath(){
        isDead = true;
        this.persistScore((int)score);
        gameOver.setGameStatus(true, score);
    }
}
