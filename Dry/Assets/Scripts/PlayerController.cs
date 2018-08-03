using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }

    [Header("Variables")]
    // player's speed
    public float speed;
    // jump force
    public float jumpForce;

    public GameObject closedUmbrella, openedUmbrella, holder, holder1, holder2;

    [Header("Grind References")]
    public Transform inicio;
    public Transform fim;

    public SoundScript _SoundScript;

    Rigidbody rbPlayer;
    Animator anim;
    public Animator animUmbrella;
    public AudioSource audioSource;
    public AudioClip walking;
    public AudioClip walkingOnPuddle;
    public AudioClip swoosh1, swoosh2, swoosh3, open, close, landing;
    /* -------------------------
     *                         *
     *    PLAYER MECHANICS     *
     *                         *
     * ------------------------*/
    public bool jumping;
    bool gliding;
    public bool isFalling;
    bool grabbing;
    public bool facingRight = true;
    bool canGrind;
    bool grinding;
    bool walkingOnWater;
    bool umbrella;
    public bool isAttacking;
    public bool changeUmbrella;
    float h;
    public bool isGrabbedOnWall;
    bool noToldo;
    bool Dead;
   

    // umbrella mechanics
    public bool umbrellaOpen = true;
     // if player is touch a wall's edge (to see with he is gonna grab or not)
     bool touchingWallEdge;

    public float hp;

    bool cachorra, cachorra1, cachorra2;

    WallEdge currenteEdge;
    public GameObject welcomeHome;
    public PlayerDamage playerDamage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    void Start()
    {
        Cursor.visible = false;

        // get player rigidbody
        rbPlayer = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hp = 0;
        noToldo = true;

        
    }

    void Update()
    {
       
        if (umbrellaOpen && !noToldo)
        {
            hp += Time.deltaTime * 2;
        }
        if (walkingOnWater)
        {
            hp += Time.deltaTime * 10;
        }

        playerDamage.currentDamage = (int)(100f - hp) / 10;

        if (hp >= 100)
        {
            IsDead();
        }


        Debug.Log(hp);

        if (hp > 20 && cachorra == false)
        {
            cachorra = true;
            _SoundScript.MudaMusica(6, 1);
        }

        else if (hp > 70 & hp <= 20 && !cachorra1)
        {
            cachorra1 = true;
            _SoundScript.MudaMusica(3, 1);
        }

        else if (hp >= 70 && !cachorra2)
        {
            cachorra2 = true;
            _SoundScript.MudaMusica(6, 1);
        }

        // get movement value
        if (!isAttacking)
        {
            h = Input.GetAxis("Horizontal");
        }

        if (this.anim.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            h = 0;
        }

        if (h > 0 || h < 0)
        {
            if (!audioSource.isPlaying)
            {
                if (!walkingOnWater)
                {
                    audioSource.PlayOneShot(walking);
                }
                else
                {
                    audioSource.PlayOneShot(walkingOnPuddle);
                }
                
            }
            anim.SetBool("Running", true);
        }
        else if(/*h == 0 && Input.GetMouseButtonDown(0) || */h == 0 && Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("Running", false);
            audioSource.Stop();
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (h < 0 && facingRight)
        {
            facingRight = false;
            this.transform.Rotate(0, 180, 0);
        }
        else if (h > 0 && !facingRight)
        {
            facingRight = true;
            this.transform.Rotate(0, 180, 0);
        }

        #region GravityMechanics

        if (rbPlayer.velocity.y < -0.1)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        // gravity
        if (gliding && jumping && isFalling)
        {
            rbPlayer.AddForce(new Vector3(0, -3.5f , 0), ForceMode.Force);
        }
        else if (isGrabbedOnWall)
        {
            rbPlayer.AddForce(Vector3.zero);
        }
        else
        {
            rbPlayer.AddForce(new Vector3(0, -15, 0), ForceMode.Force);
        }
        #endregion

        #region PlayerMovement
        // move player
        if (!grinding && !isGrabbedOnWall)
        {
            rbPlayer.transform.position += new Vector3(h, 0, 0) * speed * Time.deltaTime;
        }
        #endregion

        #region Jump
        // jump
        //if (!noToldo && umbrellaOpen)
        //{


            if (Input.GetButtonDown("Jump") && !jumping && !isFalling)
            {
                anim.SetBool("Jumping", true);
                jumping = true;
                Invoke("Jump", 0.0f);
            }
        //}

        if (jumping)
        {
            audioSource.Stop();
        }
        #endregion

        #region Glide
        // glide
        if ((Input.GetButton("Jump")) && !umbrellaOpen && jumping)
        {
            gliding = true;
            holder1.SetActive(false);
            holder2.SetActive(true);

        }
        else
        {
            // holder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            gliding = false;
            holder1.SetActive(true);
            holder2.SetActive(false);
        }
        #endregion

        #region Grabbing
        // grabbing
        if (Input.GetKeyDown(KeyCode.Z))
        {
            grabbing = true;
        }
        else
        {
            grabbing = false;
        }

        if (grabbing && touchingWallEdge)
        {
            isGrabbedOnWall = true;
        }

        if (isGrabbedOnWall)
        {
            GrabbedOnWall();
        }
        anim.SetBool("GrabbingOnWall", isGrabbedOnWall);


        #endregion

        #region Grinding
        // griding
        if (canGrind && Input.GetKeyDown(KeyCode.E) && !grinding)
        {
            StartCoroutine(Grinding());
        }

        if (!umbrella && Input.GetKeyDown(KeyCode.R))
        {
            animUmbrella.SetBool("Open", true);
            umbrella = true;

        }

        if (umbrella && Input.GetKeyDown(KeyCode.R))
        {
            animUmbrella.SetBool("Open", false);
            umbrella = false;
        }
        #endregion

        // Attack
        if (Input.GetButtonDown("Fire1") && umbrellaOpen && !isAttacking)
        {
            isAttacking = true;


            int rand = Random.Range(0, 11);

            if (rand < 3)
           {
                print(rand);
                audioSource.PlayOneShot(swoosh1);
            }

            else if (rand >= 3 && rand < 6)
            {
                print(rand);
                audioSource.PlayOneShot(swoosh2);
            }

            else
           {
                print(rand);
                audioSource.
                    PlayOneShot(swoosh3);
           }

            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (umbrellaOpen)
            {
                umbrellaOpen = false;
                changeUmbrella = false;

                audioSource.PlayOneShot(close);

                anim.SetBool("UmbrellaOpen", true
                    );
                openedUmbrella.SetActive(true);
                closedUmbrella.SetActive(false);
                animUmbrella.SetBool("Close", false);
            }
            else
            {
                animUmbrella.SetBool("Close", true);
                //float tempo = animUmbrella.GetCurrentAnimatorStateInfo(0).length;

                //if()
                //m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                if (changeUmbrella)
                {
                    StartCoroutine(CloseUmbrella());

                }
                umbrellaOpen = true;

            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            anim.SetBool("Jumping", false);
            audioSource.PlayOneShot(landing);
            jumping = false;
            gliding = false;
            holder1.SetActive(true);
            holder2.SetActive(false);

        }

        if (other.tag == "WallEdge")
        {
            currenteEdge = other.gameObject.GetComponent<WallEdge>();
            touchingWallEdge = true;
        }

        if (other.tag == "Grind")
        {
            canGrind = true;
        }

        if (other.tag == "Water")
        {

            // anim.SetBool("Jumping", false);
            audioSource.Stop();
            walkingOnWater = true;
            //jumping = false;
            //gliding = false;
            holder1.SetActive(true);
            holder2.SetActive(false);
        }
        if (other.tag == "Water40")
        {

            hp += 10;
        }



        if (other.tag == "Toldo")
        {
            noToldo = true;
        }

        if (other.tag == "Fim")
        {
            //SceneManager.LoadScene("SampleScene");
            StartCoroutine(EndGame());
        }

       
    }

    IEnumerator EndGame()
    {
        if (welcomeHome != null)
        {
            welcomeHome.SetActive(true);
        }

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            audioSource.Stop();
            walkingOnWater = false;
            
        }

        if (other.tag == "WallEdge")
        {
            touchingWallEdge = false;
        }
        if (other.tag == "Toldo")
        {
            noToldo = false;
        }

    }

    IEnumerator Grinding()
    {
        grinding = true;
        float vel = 0.5f;

        for(float i = 0; i < 1; i += vel * Time.deltaTime)
        {
            this.transform.position = Vector3.Lerp(inicio.position, fim.position, i);            
            yield return null;
        }
        rbPlayer.AddForce(new Vector3(3f, 30f, 0), ForceMode.Impulse);
        grinding = false;
    }
    
    public void Jump()
    {        
        if(!isFalling)
            rbPlayer.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);        
    }

    public void StopAttacking()
    {
        isAttacking = false;
    }

    void GrabbedOnWall()
    {
        if (currenteEdge != null && umbrellaOpen)
        {
            transform.position = Vector3.Lerp(transform.position, currenteEdge.playerPosition.position, 5f * Time.deltaTime);

            if (facingRight != currenteEdge.playerFacingRight)
            {
                facingRight = currenteEdge.playerFacingRight;
                transform.Rotate(0, 180, 0);
            }

            // if the player is grabbing sth, he can push himself up (!DIFFERENT ANIMATION!)
            jumping = false;
            rbPlayer.velocity = Vector3.zero;

            if (Input.GetButtonDown("Jump"))
            {
                isGrabbedOnWall = false;
                jumping = true;
                rbPlayer.AddForce(new Vector3(0f, 0.4f, 0f) * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            isGrabbedOnWall = false;
        }
        anim.SetBool("GrabbingOnWall", isGrabbedOnWall);
    }

    IEnumerator CloseUmbrella()
    {

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("UmbrellaOpen", false);
        audioSource.PlayOneShot(open);

        openedUmbrella.SetActive(false);
        closedUmbrella.SetActive(true);
    }

    void IsDead()
    {
        //hp = 0;
        //anim.SetBool("Dead", true);
        ////Dead = true;
        //rbPlayer.transform.position = new Vector3(31, 0.72f, 1.52f);
        SceneManager.LoadScene("Main");
    }

}
