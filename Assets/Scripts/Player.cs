using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float deathBorderY;
    [SerializeField] float stepInterval;
    [SerializeField] float pitchDistortion;
    [SerializeField] Collision_Detection groundCheck;
    [SerializeField] AudioSource JumpSound;
    public static Transform respawnPosition;
    Rigidbody2D rigid;
    AudioSource StepSound;
    Animator anim;
    float stepTimer;
    float rngPitch = 0;


    void Start()
    {
        StepSound = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(transform.position.y < deathBorderY || Input.GetKeyDown(KeyCode.R)) 
        {
            onPlayerDeath();
            transform.position = respawnPosition.position;
        }
        if(Input.GetKeyDown(KeyCode.W) && groundCheck.isColliding)
        {
            rigid.AddForce(Vector2.up * jumpForce);
            JumpSound.Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector2(-movementSpeed, rigid.velocity.y);
            transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
            anim.SetBool("isRun", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector2(movementSpeed, rigid.velocity.y);
            transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
            anim.SetBool("isRun", true);
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            anim.SetBool("isRun", false);
        }

        if(groundCheck.isColliding)
        {
            anim.SetBool("isJump", false);
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                Step();
            }
        }
        else
        {
            anim.SetBool("isJump", true);
        }
    }

    void Step()
    {
        stepTimer += Time.deltaTime;
        if (stepTimer > stepInterval)
        {
            StepSound.pitch -= rngPitch;
            rngPitch = Random.Range(-pitchDistortion, pitchDistortion);
            StepSound.pitch += rngPitch;
            StepSound.Play();
            stepTimer = 0;
        }
    }
}
