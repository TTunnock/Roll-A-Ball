using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //variables
    //float values require f after the value
    Rigidbody rb;
    public float speed = 1.0f;
    public int Score;
    int totalPickups;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    private bool wonGame = false;
    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text winText;
    
    public GameObject inGamePanel;
    public GameObject winPanel;
    public Image pickupFill;
    float pickupChunk;


    
    
    private void Start()
    {
        
        //Turn off our win text object
        winPanel.SetActive(false);
        //Turn on our in game panel
        inGamePanel.SetActive(true);
        //Gets the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        //work out how many pickups are in the scene and store in variable (pickupCount)
        Score = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Asign the amount of pickups to the total pickups
        totalPickups = Score;
        //Work out the amount of fill for our pickup fill
        pickupChunk = 1.0f / Score;
        pickupFill.fillAmount = 0;
        //Display the pickups to the user
        CheckPickups();
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;
    }
  

    void FixedUpdate()
    {
        if (resetting)
            return;

        if (wonGame == true)
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
            Score += 1;
            //Increase the fill amount of our pickup fill image
            pickupFill.fillAmount = pickupFill.fillAmount + pickupChunk;
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
        scoreText.text = "Fruits Left:" + Score.ToString() + "/" + totalPickups.ToString();
        //Check if the pickupCount == 0
        if (Score == 0)
        {
            //Turn on off in game panel
            inGamePanel.SetActive(false);
            winPanel.SetActive(true);
            //remove controls from player
            wonGame = true;
            //Set the velocity of the rigidbody to zero
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    } 
    
    //Temporary reset functionality
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
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
