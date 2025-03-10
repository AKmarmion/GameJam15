using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


public class NewMonoBehaviourScript : MonoBehaviour
{
    public Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private float xInput;

    private int facingDirection = 1;
    private bool facingRight = true;

    [SerializeField] private int maxJumps = 4;  
    private int currentJumps;

    [SerializeField] private float staminaDelay = 2f; 
    private float delayTimer;

    [SerializeField] private UnityEngine.UI.Image staminaBarFill;

    private bool isGrounded;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        currentJumps = maxJumps;
    }


    void Update()
    {
        
        CheckInput();

        if (xInput == 0)
        {
            Idle();
        }
        else
        {
            Movement();
        }

        
        FlipController();
        AnimatorControllers();

        HandleStaminaRegen();


    }

    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed!");
            Jump();
            anim.SetBool("isJumping", true);
        }
    }

    private void Idle()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
        
    }

    private void Jump()
    {
        if (currentJumps> 0)
        {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        currentJumps--;

        delayTimer = 0f; //timer reset when jumping

        UpdateStaminaBar();
        Debug.Log("Jumped! Jumps left: " + currentJumps);
        }
        else
        {
        Debug.Log("No more jumps available!");
        }
    }

    

    private void AnimatorControllers()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        bool isJumping = rb.linearVelocity.y > 0.1f || rb.linearVelocity.y < -0.1f;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
        
    }

    private void HandleStaminaRegen()
    {
        if (isGrounded && currentJumps < maxJumps)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= staminaDelay)
            {
                currentJumps++;
                delayTimer = 0f;
                UpdateStaminaBar();
            }
        }
    }

    private void UpdateStaminaBar()
    {
        if (staminaBarFill != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentJumps / maxJumps);
            staminaBarFill.fillAmount = fillAmount;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ensure platforms are named "Ground"
        {
            isGrounded = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}


