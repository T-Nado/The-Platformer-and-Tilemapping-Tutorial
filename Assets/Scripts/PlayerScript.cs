using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public Text winText;

    public Text livesText;

    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        livesText.text = lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        winText.text = "";
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
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            livesText.text = lives.ToString();
            Destroy(collision.collider.gameObject);

        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(70.0f, 2.82f, 0.0f);
        }
        if (scoreValue == 8)
        {
            winText.text = "You Win!";
            Destroy(this);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        if (lives == 0)
        {
            winText.text = "You Lose!";
            Destroy(this);
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
    }
}