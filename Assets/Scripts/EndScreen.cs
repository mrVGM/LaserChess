using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    public Button playAgain, quit;
    public Text text;

	// Use this for initialization
	void Start () {
        playAgain.onClick.AddListener(PlayAgin);
        quit.onClick.AddListener(Quit);
        text.text = (GameSettings.winner == Game.Winner.Human) ? "You Win" : "You Lose";
	}

    void PlayAgin()
    {
        GameSettings.Reset();
        SceneManager.LoadScene("StartScreen");
    }

    void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
