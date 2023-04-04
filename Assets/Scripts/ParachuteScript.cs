using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ParachuteScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField] private float horizontalSpeed = 2f; // The speed at which the parachute moves horizontally
    [SerializeField] private float xMin = -6f; // The minimum x position of the parachute
    [SerializeField] private float xMax = 6f; // The maximum x position of the parachute
    [SerializeField] private float xChangeInterval = 4f; // The interval at which the parachute changes its horizontal direction

    private float xDirection; // The current horizontal direction of the parachute
    private float timeSinceXChange; // The time since the parachute last changed its horizontal direction

    private float bombExplodeTime = 2f;

    [SerializeField] private float doubleTapThreshold = 0.2f;
    private float lastTapTime, bombTimer=0; // The time of the last tap

    [SerializeField]
    private TextMeshPro sentence;

    [SerializeField]
    private Sprite bombedSprite;

    [SerializeField]
    private GameObject confetti, pointsText;

    bool correctSentence;

    bool bombAttached;

    bool finalised;

    private void Start()
    {
        pointsText = UIReferences.Instance.pointsText;
    }

    public void startParachute()
    {
        // Set the initial horizontal direction of the parachute randomly
        if (Random.Range(0, 2) == 0)
        {
            xDirection = 1;
        }else
        {
            xDirection = -1;
        }
        speed = ApplicationController.Instance.parachuteSpeed;
        xChangeInterval = Random.Range(4, 8);
        setText();
    }

    private void setText()
    {
        if(Random.Range(0,2)==0)
        {
            sentence.text = SentencesManager.Instance.generateCorrectSentence();
            correctSentence = true;
        }else
        {
            sentence.text = SentencesManager.Instance.generateIncorrectSentence();
            correctSentence = false;
        }
    }

    private void Update()
    {
        if (transform.position.y > -4)
        {
            // Move the parachute down the screen
            transform.Translate((Vector2.down + xDirection * Vector2.right) * speed * Time.deltaTime);

            // Check if it's time to change the horizontal direction of the parachute
            timeSinceXChange += Time.deltaTime;
            if (timeSinceXChange >= xChangeInterval)
            {
                // Set a new random horizontal direction for the parachute
                xDirection = -xDirection;
                timeSinceXChange = 0f;
                xChangeInterval = Random.Range(4, 8);
            }

            if (transform.position.x < xMin)
            {
                transform.position = new Vector3(xMin + 0.1f, transform.position.y, transform.position.z);
                xDirection = 1;
            }

            if (transform.position.x > xMax)
            {
                transform.position = new Vector3(xMax - 0.1f, transform.position.y, transform.position.z);
                xDirection = -1;
            }

        }
        else
        {
            if (!correctSentence)
            {
                if (!bombAttached)
                {
                    ApplicationController.Instance.endMenu();
                }else
                {
                    finalised = true;
                }
            }
            if (!finalised)
            {
                finalised = true;
                ApplicationController.Instance.parachuteFinalised();
            }
        }

        if(bombAttached)
        {
            bombTimer += Time.deltaTime;
            if(bombTimer>bombExplodeTime)
            {
                explodeBomb();
            }
        }

    }

    private void attachBomb()
    {
        bombAttached = true;
        GetComponent<SpriteRenderer>().sprite = bombedSprite;
    }

    private void explodeBomb()
    {
        
        bombTimer = 0;
        if(!correctSentence)
        {
            confetti.SetActive(true);
            pointsText.transform.position = transform.position;
            pointsText.SetActive(true);
            ApplicationController.Instance.increasePoints();
            ApplicationController.Instance.parachuteFinalised();
            Destroy(gameObject,1);
        }
        else
        {
            bombAttached = false;
            ApplicationController.Instance.decreaseLife();
        }
       
    }

    void OnMouseDown()
    {
        // Check if this tap is a double tap
        float timeSinceLastTap = Time.time - lastTapTime;
        if (timeSinceLastTap <= doubleTapThreshold)
        {
            // This is a double tap
            if (transform.position.y > -4)
            {
                attachBomb();
            }
            lastTapTime = 0f;
        }
        else
        {
            // This is a single tap
            lastTapTime = Time.time;
        }
    }

   
}
