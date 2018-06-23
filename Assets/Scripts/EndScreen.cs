using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    public Button playAgain, quit;

	// Use this for initialization
	void Start () {
        playAgain.onClick.AddListener(PlayAgin);
        quit.onClick.AddListener(Quit);
	}

    void PlayAgin()
    {
        GameSettings.Reset();
        SceneManager.LoadScene("StartScreen");
    }

    void Quit()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}
}
