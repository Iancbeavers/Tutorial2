using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    private bool Stage2 = false;
    public Text winText;
    public Text loseText;
    private int lifecount = 3;
    public Text lives;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool facingRight = true;
    Animator anim;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = scoreValue.ToString();
        lives.text = "Lives: " + lifecount.ToString();
        winText.text = null;
        loseText.text = null;
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }

        if (Input.GetKey(KeyCode.W) || isOnGround==false)
            {
                anim.SetInteger("State", 2);
            }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (isOnGround==true)
                {
                    anim.SetInteger("State", 1);
                }
            }
        
        if (Input.GetKey(KeyCode.A)==false && Input.GetKey(KeyCode.D)==false && Input.GetKey(KeyCode.W)==false && isOnGround==true)
            {
                anim.SetInteger("State", 0);
            }

        if (Stage2 == true)
            {
                if (scoreValue == 4)
                {
                    winText.text = "Congratulations! You Win!\nGame made by: Ian Beavers";
                    musicSource.Pause();

                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                }
            }
        if (Stage2 == false)
            {
                if (scoreValue==4)
                {
                    transform.position = new Vector3(47.0f, 0.0f, 0.0f);
                    scoreValue = 0;
                    lifecount = 3;
                    score.text = scoreValue.ToString();
                    lives.text = "Lives: " + lifecount.ToString();
                    Stage2 = true;
                    if (Input.GetKey("escape"))
                        {
                            Application.Quit();
                        }
                }
            }

        if (lifecount == 0)
        {
            Destroy(gameObject);
            loseText.text = "You Lose.";
        }

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

        if (collision.collider.tag == "Enemy")
        {
            lifecount -=1;
            lives.text = "Lives: " + lifecount.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}