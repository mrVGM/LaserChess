using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    public Button startButton;
    public Dropdown level;

    // Use this for initialization
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        GameSettings.level = level.value + 1;
        SceneManager.LoadScene("Board");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
