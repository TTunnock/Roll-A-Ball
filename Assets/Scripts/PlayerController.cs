using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class PlayerController : MonoBehaviour
{
    //variables
    //float values require f after the value
    public Rigidbody rb;
    public float speed = 1.0f;
    
    
    public int TopScore;
    private int Score;
    int totalPickups;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    public bool wonGame = false;
    public GameObject inGamePanel;
    public GameObject winPanel;
    public Image pickupFill;
    float pickupChunk;

    [Header("UI")]
    public GameObject gameOverScreen;
    public TMP_Text scoreText;
    public Image winText;

    

   

    //Controllers
    GameController gameController;
    Timer timer;


    void Start()
    {

        //Turn off our win text object
        winPanel.SetActive(false);
        //Turn on our in game panel
        inGamePanel.SetActive(true);
        //Gets the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        //work out how many pickups are in the scene and store in variable (pickupCount)
        TopScore = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Asign the amount of pickups to the total pickups
        totalPickups = TopScore;
        //Work out the amount of fill for our pickup fill
        pickupChunk = 1.0f / TopScore;
        pickupFill.fillAmount = 0;
        //Start Score at zero
        Score = 0;
        gameOverScreen.SetActive(false);
        //Display the pickups to the user
        CheckPickups();
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;

        gameController = FindObjectOfType<GameController>();
        timer = FindObjectOfType<Timer>();
        if (gameController.gameType == GameType.SpeedRun)
            StartCoroutine(timer.StartCountdown());

        
    }


    void FixedUpdate()
    {
        if (resetting)
            return;

        if (wonGame == true)
            return;


        if (gameController.gameType == GameType.SpeedRun && !timer.IsTiming())
            return;

        //movement controls
        //Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the vertical axis value in a float
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Add force to our rigidbody from our movement vector times our speed
        rb.AddForce(movement * speed);


    }






    private void OnTriggerEnter(Collider other)
    {




        //if we collide with a pickup, destroy the pickup
        if (other.gameObject.CompareTag("Pickup"))
        {
            //Increase the Score when we collide with a pickup
            Score += 2;
            //Increase the fill amount of our pickup fill image
            pickupFill.fillAmount = pickupFill.fillAmount + pickupChunk + pickupChunk;
            //Display the pickups to the user
            CheckPickups();

            Destroy(other.gameObject);
        }
        //if we collide with a pickup, destroy the pickup
        if (other.gameObject.CompareTag("NegativePickup"))
        {
            //Decrement the Score when we collide with a pickup
            Score -= 1;
            //Decrease the fill amount of our pickup fill image
            pickupFill.fillAmount = pickupFill.fillAmount - pickupChunk;
            //Display the pickups to the user
            CheckPickups();

            Destroy(other.gameObject);
        }
    }

    void CheckPickups()
    {
        //Display the new pickupCount to the player
        scoreText.text = "Score:" + Score.ToString();
        //Check if the pickupCount == 0
        if (Score == TopScore)
        {
            WinGame();
        }
    }
    
    void WinGame()
    {
        gameOverScreen.SetActive(true);

        if (gameController.gameType == GameType.SpeedRun)
            timer.StopTimer();
    }
    

    //Temporary reset functionality
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;

    }
    
    
}





        
    
    
    

    





    //Create a win condition that happens when pickupCount == 0
