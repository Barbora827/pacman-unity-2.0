using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpSpeed = 12f;
    public float moveSpeed = 5f;
    public float timeRemaining = 7;
    public bool timerIsRunning = false;
    public Text counter;

    private Rigidbody2D myRigidbody;
    public GameObject[] cookies;
    

    SpriteRenderer sprite;
    Color newColor;
    

    [Header("-- Private references --")]
    [SerializeField] private int cookiesEaten = 0;
    [SerializeField] private GameObject winningFlag;

    // Start is called before the first frame update
    void Start()
    {
            Time.timeScale = 1;
            myRigidbody = GetComponent<Rigidbody2D>();
            cookies = GameObject.FindGameObjectsWithTag("Cookie");
            timerIsRunning = false;
            timeRemaining = 7;
            FindObjectOfType<AudioManager>().Play("BgMusic");

            counter = GetComponent <Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
         {
             print("Game restarted");
             SceneManager.LoadScene(0);
             Time.timeScale = 1;
         }

         if (Input.GetKeyDown(KeyCode.Escape))
         {
             Application.Quit();
         }

        if (timerIsRunning == true){
            if (timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
                sprite = GetComponent<SpriteRenderer>();
                sprite.color = Color.blue;
            }
            else{
                print("Time has run out!");
                timerIsRunning = false;
                sprite = GetComponent<SpriteRenderer>();
                sprite.color = Color.yellow;
            }
        }
        Vector3 newPosition = transform.position;
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                newPosition.x -= moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                newPosition.x += moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                newPosition.y += moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                newPosition.y -= moveSpeed * Time.deltaTime;
            }

            if(transform.position.x > 14.3){
                newPosition.x = -14;
            }

            if(transform.position.x < -14.6){
                newPosition.x = 14;
            }

            

        transform.position = newPosition;

        counter.text = "Cookies eaten: " + cookiesEaten.ToString();

        if (Time.timeScale == 0){
            FindObjectOfType<AudioManager>().Stop("BgMusic");
        }

        

    }

    // OnTriggerEnter2D is called once, when two GameObjects with Collider2Ds hit each other
        // - One GameObject must have a Rigidbody2D as well
        // - One of the Collider2Ds must be a Trigger
    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.tag.Equals("Cookie")) {
            FindObjectOfType<AudioManager>().Play("Cookie");
            cookiesEaten += 1;
            Destroy(coll.gameObject);
        }

        if(coll.gameObject.tag.Equals("Cherry")) {
            FindObjectOfType<AudioManager>().Play("Cherry");
            timerIsRunning = true;
            timeRemaining = 7;
            Destroy(coll.gameObject);
            
        }
            

        if(coll.gameObject.tag.Equals("Ghost")) {
            if(timerIsRunning == true){
                FindObjectOfType<AudioManager>().Play("EatEnemy");
                Destroy(coll.gameObject);
            }
            else{
            SceneManager.LoadScene("gameover");
            }
        }

        int cookiesAmount = cookies.Length;

        if(cookiesEaten == cookiesAmount){
            SceneManager.LoadScene("newLvl");
        }


        
    }

    // OnCollisionEnter2D is called once, when two GameObjects with Collider2Ds hit each other
        // - One GameObject must have a Rigidbody2D as well
    void OnCollisionEnter2D(Collision2D coll) {

    }

}
