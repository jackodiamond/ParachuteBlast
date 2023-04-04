using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIReferences : MonoBehaviour
{
    public static UIReferences Instance;

    private void Awake()
    {
        // Ensure that there is only one instance of ApplicationController
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TMP_Text scoreText; // Reference to the text field in the UI

    public GameObject[] hearts;

    public GameObject endGameMenu, pointsText,levelUpUI;

    private void Start()
    {
        scoreText.text = "" + ApplicationController.Instance.score;
    }
    public void nextLevel()
    {
        ApplicationController.Instance.nextLevel();
    }
    public void restart()
    {
        ApplicationController.Instance.restart();
    }

    public void quit()
    {
        ApplicationController.Instance.closeApp();
    }
}
