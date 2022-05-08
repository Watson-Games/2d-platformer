using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer sprite;
    private Animator animator;
    private new BoxCollider2D collider;

    [SerializeField]
    private float moveSpeed = 7;

    [SerializeField]
    private float jumpSpeed = 14;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState
    {
        Idle    = 0,
        Running = 1,
        Jumping = 2,
        Falling = 3
    }

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var directionX = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(directionX * moveSpeed, rigidbody2D.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        }

        UpdateAnimation(directionX);
    }

    private void UpdateAnimation(float directionX)
    {
        var movementState = MovementState.Idle;
        var isFloating = Mathf.Abs(rigidbody2D.velocity.y) > 0.1;
        var isRunning = directionX != 0;

        if (isFloating)
        {
            movementState = rigidbody2D.velocity.y > 0 ? MovementState.Jumping : MovementState.Falling;
        }
        else if (isRunning)
        {
            movementState = MovementState.Running;
            sprite.flipX = directionX < 0;
        }

        animator.SetInteger("movementState", (int)movementState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, jumpableGround);
    }
}