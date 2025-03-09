using UnityEngine;
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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        Movement();
        CheckInput();

        FlipController();
        AnimatorControllers();


    }

    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.linearVelocity.x != 0;

        anim.SetBool("isMoving", isMoving);

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

}


