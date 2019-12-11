using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;

    private bool faceRight = true;
    public Animator animator;
    public static bool hitGoodStuff;
    public static bool hitBadStuff;
    public static bool fight = true;

    public AudioSource ads;
    public AudioClip flower_ads;
    public AudioClip bomb_ads;
    public AudioClip jump_ads;
    public AudioClip kill_ads;

    public int health = 2;
    public Text CountText;

    bool isDead = false;
    public DeathMenu deathMenu;
    public HighscoreTable highscoreTable;

    public FixedJoystick fixedJoystick;
    public FightBtn fightBtn;
    public JumpBtn jumpBtn;

    bool hitEnemy = false;
    bool fallOfPf = false;

    bool jump = false, keyA = false, keyD = false, keyS = false;

    public Joystick joystick; // mobile
    bool jumpBtnDown = false;

    void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        hitGoodStuff = false;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        ads = audioSources[0];
        flower_ads = audioSources[0].clip;
        bomb_ads = audioSources[1].clip;
        jump_ads = audioSources[2].clip;
        kill_ads = audioSources[3].clip;
    }

    void Update()
    {
        if (health < 0 || hitEnemy || fallOfPf) // health < 0 || hit by the boss || fall of the platform
        {
            Die();
        }

        CountText.text = "Health: " + health.ToString(); //indication of how many shots are available



        /*
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        */

        if (IsGrounded() &&  jumpBtnDown)
        {
            jump = true;
            jumpBtnDown = false;
        }

        /*
        if (Input.GetKey(KeyCode.A))
        {
            keyA = true;
        }
        */

        if(joystick.Horizontal <= -0.2f)
        {
            keyA = true;
        }

        /*
        if (Input.GetKey(KeyCode.D))
        {
            keyD = true;
        }
        */

        if (joystick.Horizontal >= 0.2f)
        {
            keyD = true;
        }
        

        /*
        if (Input.GetKey(KeyCode.S))
        {
            keyS = true;
        }
        */

        if (joystick.Vertical <= -0.2)
        {
            keyS = true;
        }


    }

    public void JumpBtnDown() //mobile
    {
        jumpBtnDown = true;
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleMovement();
        SetAnimation();

        jump = false;
        keyA = false;
        keyD = false;
        keyS = false;

        if(transform.position.y < -20)
        {
            fallOfPf = true;
        }
    }

    void Die()
    {
        //Debug.Log("Call Die()....");
        isDead = true;
        deathMenu.ToggleEndMenu(health);
        fixedJoystick.ToggleEndMenu();
        jumpBtn.ToggleEndMenu();
        fightBtn.ToggleEndMenu();


    }

    public void CheckRecord()
    {
        highscoreTable.ToggleEndMenu(health);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }
    
    void HandleJump()
    {
        if (jump)
        {
            float jumpVelocity = 8f;
            rigidbody2D.velocity = Vector2.up * jumpVelocity;
            ads.PlayOneShot(jump_ads);
        }
    }
    void HandleMovement()
    {
        float moveSpeed = 3f;

        if (keyA)
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
            if (faceRight) Flip();
        }
        else
        {
            if (keyD)
            {
                rigidbody2D.velocity = new Vector2(+moveSpeed, rigidbody2D.velocity.y);
                if (!faceRight) Flip();
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }
        }
        
        if (keyS) // cancel face animation
        {
            hitGoodStuff = false;
            hitBadStuff = false;
            fight = false;
        }
        
    }

    private void Flip()
    {
        faceRight = !faceRight;
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        transform.Rotate(0f, 180f, 0f);
    }
    
    void SetAnimation()
    {
        if (!IsGrounded())
        {
            animator.SetBool("Anim_isGrouned", false);
        }
        else
        {
            animator.SetBool("Anim_isGrouned", true);
        }
       
        if(rigidbody2D.velocity.x == 0)
        {
            animator.SetBool("Anim_isMoving", false);
        }
        else
        {
            animator.SetBool("Anim_isMoving", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Flower"))
        {
            collision.gameObject.SetActive(false);
            ads.PlayOneShot(flower_ads);
            hitGoodStuff = true;
            hitBadStuff = false;
            health = health + 1;
        }
        

        if (collision.gameObject.CompareTag("Bomb"))
        {
            collision.gameObject.SetActive(false);
            ads.PlayOneShot(bomb_ads);
            hitBadStuff = true;
            hitGoodStuff = false;
            health = health - 1;
        }

        if (collision.gameObject.CompareTag("Diamond"))
        {
            collision.gameObject.SetActive(false);
            ads.PlayOneShot(flower_ads);
            hitGoodStuff = true;
            hitBadStuff = false;
            health = health + 2;
        }

        if (collision.gameObject.CompareTag("Landmine"))
        {
            collision.gameObject.SetActive(false);
            ads.PlayOneShot(bomb_ads);
            hitBadStuff = true;
            hitGoodStuff = false;
            health = health - 2;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            hitEnemy = true;
            ads.PlayOneShot(kill_ads);
        }




    }
 


}
