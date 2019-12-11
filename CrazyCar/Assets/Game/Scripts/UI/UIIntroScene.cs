using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIIntroScene : MonoBehaviour
{
    private Animator anim;
    public Text highscore;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Credits()
    {
        anim.SetTrigger("OpenCredits");
    }

    public void CloseCredits()
    {
        anim.SetTrigger("CloseCredits");
    }


    public void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
