using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private int score;
    public Text ScoreUI;
    public Text LifeUI;
    private int liftPoint = 3;
    private bool isgameOver = false;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        this.score += 5;
        changeScore();
    }
    
    public void RemoveLifePoint()
    {
        this.liftPoint -= 1;
        changeLife();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void changeScore()
    {
        ScoreUI.text = "Score : " + this.score;
    }

    public bool getIsGameOver()
    {
        return this.isgameOver;
    }
    
    void changeLife()
    {
        LifeUI.text = "HP x " + this.liftPoint;
        if(this.liftPoint <= 0 && !isgameOver)
        {
            isgameOver = true;
            gameOverPanel.SetActive(true);
        }
    }
}
