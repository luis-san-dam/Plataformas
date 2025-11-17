using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{
    [Header("Jump details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    [Header("Ground details")]
    public bool grounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Components")]
    private Rigidbody2D rb2D;
    private Animator myAnimator;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }
    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }

        //Espacio o W salta
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
        }

        //Mantener espacio o W para seguir saltando
        if (Input.GetButton("Jump") && !stoppedJumping && jumpTimeCounter > 0)
        {
            //Sigue saltando
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("falling", true);
            myAnimator.ResetTrigger("jump");
        }

        if (rb2D.linearVelocity.y < 0)
        {
            myAnimator.SetBool("falling", true);
        }
    }
    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

}
