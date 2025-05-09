using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;

    Rigidbody2D rb;
    Animator anim;
    bool isGrounded = false;
    bool isJurang = false;
    AudioSource audioSource;
    public AudioClip[] footstepAudio;
    int audioNumber = 0;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isJurang)
        {
            Jump();
            Facing();
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Budi_drop"))
            {
                Movement();
            }
        }

        Animation();

    }

    public void playFootstepAudio()
    {
        audioSource.clip = footstepAudio[audioNumber];
        if (audioNumber == 1)
        {
            audioSource.time = 10.5f;
        }
        else if (audioNumber == 0)
        {
            audioSource.time = 5.8f;
        }
        audioSource.Play();
    }

    public void stopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void Movement()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0);

        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(0, 1) * jumpForce;
        }
    }

    private void Facing()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(3.5f, 3.5f, 0);
            
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-3.5f, 3.5f, 0);
        }
    }

    private void Animation()
    {
        anim.SetFloat("Running", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isJurang", isJurang);
        anim.SetBool("isDropped", anim.GetCurrentAnimatorStateInfo(0).IsName("Budi_drop"));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Wall" && isJurang)
        {
            collision.isTrigger = true;
        }else if (collision.tag == "Wall" && !isJurang)
        {
            collision.isTrigger = false;
        }


        if (collision.tag == "Concrete/Dirt")
        {
            audioNumber = 0;
        }
        else if (collision.tag == "Grass/Dirt")
        {
            if (audioNumber == 0)
            {
                audioNumber = 1;
            }
            else if (audioNumber == 1)
            {
                audioNumber = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Jurang"))
        {
            isGrounded = true;
            isJurang = true;
            if (collision.gameObject.tag == "Jurang/Dirt")
            {
                AudioManager.Instance.PlaySfx("SlideDirt");

            }
            if (collision.gameObject.tag == "Jurang/Grass")
            {
                AudioManager.Instance.PlaySfx("SlideGrass");
            }
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Jurang"))
        {
            AudioManager.Instance.StopSfxAudio();
            isJurang = false;
        }
    }

}
