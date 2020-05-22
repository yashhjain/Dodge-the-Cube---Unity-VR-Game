using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    // Start is called before the first frame update
    public Text scoreSoFar;
    public Image gameOverTrasition;
    private float trasition = 0.0f;
    private bool isGameOver;
    void Start()
    {
        //Should not be displayed when our game starts.
        gameObject.SetActive(false);
        this.setIsGameOverShown(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(isGameOver){
            trasition = trasition + Time.deltaTime;
            gameOverTrasition.color = Color.Lerp(new Color(0,0,0,0), new Color(150,1,1,255), trasition);
        }
        else{
            return;
        }
    }  

    public void setGameStatus(bool status, float score){
        gameObject.SetActive(status);
        this.setScore(score);
        this.setIsGameOverShown(true);
    }

    public void setScore(float score){
        scoreSoFar.text = ((int)score).ToString();
    }

    public void setIsGameOverShown(bool status){
        isGameOver = status;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
