using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine Instance { get; private set; }

    [Header("Health")]
    public float hp = 100;
    public PlayerDamage playerDamage;

    [Header("Movement")]
    public float speed = 5f;
    public bool isFacingRight = true;
    public bool isGrounded;
    public bool isOnWater;
    public bool isTouchingWallEdge;
    public float jumpForce = 10f;
    public float glidingForce = 2f;
    public LayerMask walkable;

    [Header("Umbrella")]
    public bool isUmbrellaOpen;
    public GameObject umbrellaOpen;
    public GameObject umbrellaClosed;
    public Transform GrabbingPoint;
    public Animator animatorUmbrella;

    [Header("Actions")]
    public bool isGliding;
    public bool isAttacking;
    public bool isHooking;
    public bool isGrabbing;

    
    [HideInInspector] public float horizontal = 0f;
    [HideInInspector] public bool InputJump;
    [HideInInspector] public bool InputJumpDown;
    [HideInInspector] public bool InputAttack;
    [HideInInspector] public bool InputGrab;
    [HideInInspector] public Rigidbody rigidbody;
    [HideInInspector] public Animator animator;

    RaycastHit _hit;
    WallEdge _wallEdge;

    private PlayerState currentState;

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

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();



        SetState(new Idle(this));
    }

    private void Update()
    {
        isGrounded = IsItGrounded();
        isOnWater = IsOnWater();
        GetInput();
        RotatePlayer();
        UpdateUmbrella();
        TakeDamege();
        
        currentState.Tick();
    }

    private void FixedUpdate()
    {
        currentState.FixedTick();
    }



    public void SetState(PlayerState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        Debug.Log("Player - " + currentState.GetType().ToString());

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }



    #region UpdateMethods

    bool IsItGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 0.1f, walkable);
    }

    bool IsOnWater()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 0.1f, LayerMask.NameToLayer("Water"));
    }

    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.C))
        {
            isUmbrellaOpen = !isUmbrellaOpen;
        }

        InputJump = Input.GetButton("Jump");
        InputJumpDown = Input.GetButtonDown("Jump");
        InputAttack = Input.GetButtonDown("Fire1");
        InputGrab = Input.GetKeyDown(KeyCode.Z);
    }

    void RotatePlayer()
    {
        if (horizontal < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.Rotate(0, 180, 0);
        }
        else if (horizontal > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.Rotate(0, 180, 0);
        }
    }

    void UpdateUmbrella()
    {
        animator.SetBool("UmbrellaOpen", isUmbrellaOpen);
        animatorUmbrella.SetBool("Open", isUmbrellaOpen);

        if (isUmbrellaOpen)
        {
            umbrellaOpen.SetActive(true);
            umbrellaClosed.SetActive(false);
        }
        else
        {
            umbrellaOpen.SetActive(false);
            umbrellaClosed.SetActive(true);
        }
    }

    void TakeDamege()
    {
        if (!isUmbrellaOpen)
        {
            hp -= Time.deltaTime * 2;
        }

        if (isOnWater)
        {
            hp -= Time.deltaTime * 10;
        }

        playerDamage.currentDamage = (int)(hp / 10f);

        if (hp <= 0)
        {
            SceneManager.LoadScene("Main");
        }

    }

    #endregion


    #region AnimationMethods

    public void StopAttacking()
    {
        isAttacking = false;
    }

    public void StopHookining()
    {
        isHooking = false;
    }

    #endregion


    #region StatesMethod

    public void Moving()
    {
        if (horizontal != 0)
        {
            //rigidbody.transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;
            rigidbody.velocity = new Vector3(horizontal * speed, rigidbody.velocity.y, 0f);
        }
    }

    public void Landing()
    {
        if (horizontal != 0)
        {
            SetState(new Running(this));
        }
        else
        {
            SetState(new Idle(this));
        }
    }

    public void Attacking()
    {
        if (InputAttack && !isUmbrellaOpen)
        {
            SetState(new Hitting(this));
        }
    }

    public void Hooking()
    {
        if (InputGrab && !isUmbrellaOpen)
        {
            SetState(new Hooking(this));
        }
    }

    //public void Grabbing()
    //{
    //    if (InputGrab && !isUmbrellaOpen && isTouchingWallEdge)
    //    {
    //        SetState(new Grabbing(this));
    //    }
    //}

    public void GrabbingPosition()
    {
        //transform.position = Vector3.Lerp(transform.position, _wallEdge.playerPosition.position - (GrabbingPoint.position - transform.position), 10f * Time.deltaTime);
        transform.position = _wallEdge.playerPosition.position - (GrabbingPoint.position - transform.position);

        if (isFacingRight != _wallEdge.playerFacingRight)
        {
            isFacingRight = _wallEdge.playerFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    #endregion


    #region Triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallEdge")
        {
            _wallEdge = other.gameObject.GetComponent<WallEdge>();
            isTouchingWallEdge = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WallEdge")
        {
            _wallEdge = null;
            isTouchingWallEdge = false;
        }
    }


    #endregion
}
