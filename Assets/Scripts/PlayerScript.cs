using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Transform startMarker;
    
    private Rigidbody2D rd2d;

    public float speed;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverTextObject;
    
    private int scoreValue;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        scoreValue = 0;
        lives = 3;

        SetCountText();
        gameOverTextObject.SetActive(false);
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
                
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            scoreText.text = "Score: " + scoreValue.ToString();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            Destroy(collision.collider.gameObject);
            
            if (scoreValue == 4)
            {
                transform.position = new Vector2(48.0f, -0.1f);
                lives = 3;
                livesText.text = "Lives: " + lives.ToString();
            }
            if (scoreValue >= 8)
            {
                gameOverTextObject.SetActive(true);
            }
        }
       if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);
            
            if (lives <= 0)
            {
                gameOverTextObject.SetActive(true);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

        if (lives == 0)
        {
            Destroy(gameObject);
        }

        if (scoreValue == 8)
        {
            Destroy(this);
        }
    }
}