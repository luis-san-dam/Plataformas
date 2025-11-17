using UnityEditor.Tilemaps;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    private bool facingRight = true;

    public float speed = 5f;
    public float horizMovement;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update() //mira el input de movimiento
    {
        horizMovement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() //mueve al jugador
    {
        rb2D.linearVelocity = new Vector2(horizMovement * speed, rb2D.linearVelocity.y);
        Flip(horizMovement);
        myAnimator.SetFloat("speed", Mathf.Abs(horizMovement));
    }

    //dar la vuelta
    private void Flip(float horizontal)
    {
        if (horizMovement < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
