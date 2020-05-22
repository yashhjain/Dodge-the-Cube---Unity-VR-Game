using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string HIGHSCORE = "High Score";
    public Text HighScore;
    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = "High Score: " + PlayerPrefs.GetInt(HIGHSCORE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(){
        SceneManager.LoadScene("SampleScene");
    }
}
