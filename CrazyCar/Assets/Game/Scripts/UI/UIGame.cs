using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    public Text score;
    public Text time;
    public Text scoreGameOver;
    public Text highscore;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + System.Math.Round(GameManager.Get().time,2);
        score.text = "Score: " + GameManager.Get().score;
        scoreGameOver.text = "Score: " + GameManager.Get().score;
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();

        if (!GameManager.Get().car.gameStarted)
        {
            anim.SetTrigger("GameOver");
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void PlayAgain()
    {
        GameManager.Get().time = 30;
        GameManager.Get().score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void GoToMenu()
    {
        GameManager.Get().car.gameStarted = false;
        GameManager.Get().time = 30;
        GameManager.Get().score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroScene");
    }
}
