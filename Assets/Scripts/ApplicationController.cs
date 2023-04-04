using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    public static ApplicationController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        score = 0;
        lives = 3;
        instantiteParachutes();
    }

    public float parachuteSpeed=1;
    public float timeBetweenParachute=5;
    public GameObject parachutePrefab;


    private GameObject parachuteObjects;

    public int score;
    private int lives;

    float timer=0;

    bool levelUp;

    public int parachuteCount;


    private void instantiteParachutes()
    {
        parachuteObjects = Instantiate(parachutePrefab);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>timeBetweenParachute)
        {
            timer = 0;
            selectParachute();
        }

        
    }

    private void selectParachute()
    {
        if (parachuteObjects == null)
            return;

        for(int i=0;i<parachuteObjects.transform.childCount;i++)
        {
            if(!parachuteObjects.transform.GetChild(i).gameObject.activeSelf)
            {
                startParachute(parachuteObjects.transform.GetChild(i).gameObject);
                break;
            }
        }
    }

    private void startParachute(GameObject parachute)
    {
        parachute.SetActive(true);
        parachute.transform.position = new Vector3(Random.Range(-5,5),7,0);
        parachute.GetComponent<ParachuteScript>().startParachute();
    }

    private void OnLevelWasLoaded(int level)
    {
        instantiteParachutes();
        parachuteCount = 0;
        if(!levelUp)
        {
            score = 0;   
        }
        lives = 3;
        for(int i=0;i<lives;i++)
        {
            UIReferences.Instance.hearts[i].SetActive(true);
        }
        UIReferences.Instance.scoreText.text = "" + score;
    }


    public void parachuteFinalised()
    {
        parachuteCount++;
        if(parachuteCount>=10)
        {
            showLevelUpUI();
        }
    }

    private void showLevelUpUI()
    {
        Destroy(parachuteObjects);
        UIReferences.Instance.levelUpUI.SetActive(true);
    }

    public void increasePoints()
    {
        score += 10;
        UIReferences.Instance.scoreText.text = "" + score;
    }

    public void decreaseLife()
    {
        lives--;
        UIReferences.Instance.hearts[lives].SetActive(false);
        if(lives<=0)
        {
            endMenu();
        }
    }

    public void nextLevel()
    {
        parachuteSpeed += 1.5f;
        levelUp = true;
        SceneManager.LoadScene(1);

    }

    public void endMenu()
    {
        levelUp = false;
        Destroy(parachuteObjects);
        UIReferences.Instance.endGameMenu.SetActive(true);
    }

    public void restart()
    {
        lives = 3;

        UIReferences.Instance.hearts[0].SetActive(true);
        UIReferences.Instance.hearts[1].SetActive(true);
        UIReferences.Instance.hearts[2].SetActive(true);

        score = 0;

        parachuteSpeed = 1;

        SceneManager.LoadScene(1);
    }

    public void closeApp()
    {
        Application.Quit();
    }
}
