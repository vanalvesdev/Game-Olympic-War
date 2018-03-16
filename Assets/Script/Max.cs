using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max : MonoBehaviour
{

    private Animator animator;
    public float speed;
    private SpriteRenderer render;
    private Rigidbody2D body;
    public float jumpForce;
    public LayerMask ground;
    private Vector2 lastVelocity;
    private BoxCollider2D collider;
    private Vector2 normalSizeCollider;
    private Vector2 smallSizeCollider;
    public float fullLife;
    private float currentLife;
    public GameObject lifeBar;
    private GameObject enemy;
    private GameObject controller;
    public AudioClip soco;
    private AudioSource audioSource;
    public AudioClip cair;
    private bool morto;
    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        normalSizeCollider = collider.size;
        smallSizeCollider = new Vector2(0.6025939f, 0.69123f);
        currentLife = fullLife;
        controller = GameObject.Find("Controller");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife > 0 && !morto)
        {


            bool running = false;
            bool isGrounded = body.IsTouchingLayers(ground);
            bool caindo = false;
            if (Input.GetKeyDown(KeyCode.A) && isGrounded)
            {
                audioSource.clip = soco;
                audioSource.Play();
                animator.SetTrigger("Punch");
            }
            else if (Input.GetKeyDown(KeyCode.D) && isGrounded)
            {
                audioSource.clip = soco;
                audioSource.Play();
                animator.SetTrigger("Uppercut");
            }
            else if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
            {
                animator.SetTrigger("Voadora");
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    render.flipX = false;
                }
                else
                {
                    render.flipX = true;
                }


                float direction = Input.GetAxis("Horizontal");
                transform.Translate(direction * speed * Time.deltaTime, 0, 0);
                running = true;
                if (!isGrounded)
                {
                    animator.SetTrigger("Mortal");
                    collider.size = smallSizeCollider;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                lastVelocity = Vector2.zero;
                isGrounded = false;
                animator.SetTrigger("Jump");
            }



            if (!isGrounded && !caindo)
            {
                if (lastVelocity.y > body.velocity.y)
                {
                    caindo = true;
                    lastVelocity = body.velocity;
                }
            }
            else
            {
                collider.size = normalSizeCollider;
            }

            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("Run", running);
            animator.SetBool("Caindo", caindo);
        }
    }

    void LateUpdate()
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Mortal");
        if (body.IsTouchingLayers(ground))
        {
            animator.ResetTrigger("Voadora");
        }
    }

    public void applyJump()
    {
        body.velocity = Vector2.zero;
        body.AddForce(Vector2.up * jumpForce * 10, ForceMode2D.Force);
        enabled = true;
    }

    public void receiverDamage(int damage)
    {
        if (currentLife > 0)
        {
            currentLife -= damage;
            if (currentLife < 0)
                currentLife = 0;
            float barSize = currentLife / fullLife;
            lifeBar.transform.localScale = new Vector3(barSize, 1, 1);
            if (currentLife <= 0)
            {
                if (currentLife < 0)
                    currentLife = 0;
                morto = true;
                collider.size = smallSizeCollider;
                animator.SetTrigger("Morrer");
                audioSource.Stop();
                audioSource.clip = cair;
                audioSource.Play();
            }
        }
    }

    void applyDamage(int damage)
    {
        if (enemy != null)
        {
            enemy.gameObject.SendMessage("receiverDamage", damage);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            enemy = coll.gameObject;
        }
        else if (coll.gameObject.tag == "Laser")
        {
            animator.SetTrigger("Hit");
            receiverDamage(coll.gameObject.GetComponent<Disparo>().damage);
            DestroyObject(coll.gameObject);
        }

    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            enemy = null;

    }

    public void resetVoadora()
    {
        animator.ResetTrigger("Voadora");
        if (enemy != null)
        {
            enemy.SendMessage("derrubar");
        }
    }

    public void desabilitarScript()
    {
        if (currentLife <= 0)
        {
            animator.enabled = false;
            controller.SendMessage("MaxPerdeu");
            this.enabled = false;
        }
    }

}
