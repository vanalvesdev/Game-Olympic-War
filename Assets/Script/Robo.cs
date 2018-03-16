using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo : MonoBehaviour
{
    public float speed;
    private float normalDistance;
    private GameObject max;
    private SpriteRenderer render;
    private Rigidbody2D body;
    public float fullLife;
    public float currentLife;
    public GameObject lifeBar;
    private Animator animator;
    public float timeToAttack;
    private float currentTimeToAttack;
    public GameObject prefab;
    private Vector2 normalSizeCollider;
    private Vector2 smallSizeCollider;
    private BoxCollider2D collider;
    private Vector3 inicialPosition;
    private Transform laserLauncher;
    public GameObject controller;
    public AudioClip laser;
    public AudioClip soco;
    public AudioClip cair;
    private AudioSource audioSource;
    private bool caido;
    // Use this for initialization

    void Awake()
    {
        max = GameObject.Find("Max");
        render = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        normalSizeCollider = collider.size;
        smallSizeCollider = new Vector2(0.5134401f, 0.4793196f);
        inicialPosition = transform.position;
        laserLauncher = GameObject.Find("LaserLaunch").transform;
        controller = GameObject.Find("Controller");
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

        normalDistance = transform.position.x - max.transform.position.x;
        currentLife = fullLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife > 0)
        {
            if (!caido)
            {
                float distance = transform.position.x - max.transform.position.x;
                currentTimeToAttack += Time.deltaTime;
                bool move = false;
                if (distance > 0)
                {
                    render.flipX = true;
                }
                else
                {
                    render.flipX = false;
                }

                if (Mathf.Abs((int)distance) > Mathf.Abs((int)normalDistance))
                {
                    move = true;
                    if (distance > 0)
                    {
                        render.flipX = true;
                        transform.Translate(Vector3.left * speed * Time.deltaTime);
                    }
                    else
                    {
                        render.flipX = false;
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                    }
                }

                if (currentLife <= (fullLife / 2))
                {
                    if (currentTimeToAttack >= (timeToAttack * 3))
                    {
                        audioSource.clip = laser;
                        audioSource.Play();
                        GameObject temp = Instantiate(prefab);
                        temp.transform.position = laserLauncher.position;
                        temp.GetComponent<Disparo>().direction = render.flipX ? Vector2.left : Vector2.right;
                        currentTimeToAttack = 0;
                    }
                }
                else if (currentTimeToAttack >= timeToAttack)
                {
                    audioSource.clip = soco;
                    audioSource.Play();
                    animator.SetTrigger("Punch");
                    currentTimeToAttack = 0;
                }

                animator.SetBool("Move", move);
            }
            animator.SetBool("Vivo", currentLife >= 0);
        }
    }

    public void receiverDamage(int damage)
    {
        if (currentLife > 0)
        {
            animator.SetTrigger("Damage");
            currentLife -= damage;

            float barSize = currentLife / fullLife;
            lifeBar.transform.localScale = new Vector3(barSize, 1, 1);
            if (currentLife <= 0)
            {
                if (currentLife < 0)
                    currentLife = 0;
                derrubar();
            }
        }
    }

    void applyDamage(int damage)
    {
        if (GetComponent<Collider2D>().IsTouching(max.GetComponent<Collider2D>()))
        {
            max.SendMessage("receiverDamage", damage);
        }
    }


    void derrubar()
    {
        caido = true;
        collider.size = smallSizeCollider;
        animator.SetTrigger("Derrubado");
        body.constraints = RigidbodyConstraints2D.FreezePositionX;
        currentTimeToAttack = 0;
    }


    public void levantar()
    {
        caido = false;
        collider.size = normalSizeCollider;
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = new Vector3(transform.position.x, inicialPosition.y, transform.position.z);
    }

    public void desabilitarScript()
    {
        if (currentLife <= 0)
        {
            animator.enabled = false;
            controller.SendMessage("MaxVenceu");
            this.enabled = false;
        }
    }


    public void somCair()
    {
        audioSource.Stop();
        audioSource.clip = cair;
        audioSource.Play();
    }
}
