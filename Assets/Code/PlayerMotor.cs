using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    public float acceleration = 10;
    public float stoppingForce = 10;
    public float maxSpeedX = 10;
    public float stoppingPoint = 0.1f;
    public float jumpForce = 5;
    public float wallJumpForce = 5;
    public float enemyHitForce = 5;

    [Header("Wall Slide Settings")]
    public LayerMask wallLayer;
    public float wallSlideSpeed = 2f;
    public int maxWallJumps = 1; // LIMIT SKOKÓW OD ŒCIANY
    private int _wallJumpsRemaining; // Licznik pozosta³ych skoków
    private bool isWallSliding;
    private int wallDir;

    [Header("Double Jump Settings")]
    public int maxJumps = 2;
    private int _jumpsRemaining;

    [Header("Animation & Visuals")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jumpsRemaining = maxJumps;
        _wallJumpsRemaining = maxWallJumps; // Inicjalizacja limitu
    }

    private void FixedUpdate()
    {
        CheckForWall();

        MovePlayer();
        LimitMaxSpeed();
        UpdateAnimations();
    }

    private void CheckForWall()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallLayer);

        if (hitRight.collider != null) { isWallSliding = true; wallDir = 1; }
        else if (hitLeft.collider != null) { isWallSliding = true; wallDir = -1; }
        else { isWallSliding = false; }

        if (_isGrounded || _rigidbody2D.linearVelocityY > 0)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, Mathf.Clamp(_rigidbody2D.linearVelocityY, -wallSlideSpeed, float.MaxValue));
        }
    }

    private void LimitMaxSpeed()
    {
        if (_rigidbody2D.linearVelocityX >= maxSpeedX) _rigidbody2D.linearVelocityX = maxSpeedX;
        else if (_rigidbody2D.linearVelocityX <= -maxSpeedX) _rigidbody2D.linearVelocityX = -maxSpeedX;
    }

    private void MovePlayer()
    {
        if (!isWallSliding)
        {
            if (direction.x != 0) _rigidbody2D.AddForce(new Vector2(direction.x * acceleration, 0));
            else if (_rigidbody2D.linearVelocityX != 0)
            {
                if (_rigidbody2D.linearVelocityX < stoppingPoint && _rigidbody2D.linearVelocityX > -stoppingPoint)
                    _rigidbody2D.linearVelocity = new Vector2(0.0f, _rigidbody2D.linearVelocityY);
                else
                    _rigidbody2D.AddForce(new Vector2(-_rigidbody2D.linearVelocityX * stoppingForce, 0));
            }
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null || spriteRenderer == null) return;

        animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.linearVelocityX));
        animator.SetFloat("yVelocity", _rigidbody2D.linearVelocityY);
        animator.SetBool("isGrounded", _isGrounded);
        animator.SetBool("isWallSliding", isWallSliding);

        if (_rigidbody2D.linearVelocityX > 0.1f) spriteRenderer.flipX = false;
        else if (_rigidbody2D.linearVelocityX < -0.1f) spriteRenderer.flipX = true;
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnJump()
    {
        if (isWallSliding)
        {
            // Sprawdzamy, czy gracz ma wci¹¿ dostêpne skoki od œciany
            if (_wallJumpsRemaining > 0)
            {
                _rigidbody2D.linearVelocity = new Vector2(wallDir * -wallJumpForce, jumpForce);
                isWallSliding = false;
                _wallJumpsRemaining--; // Zabieramy jeden skok

                if (animator != null)
                {
                    animator.SetTrigger("WallJumpTrigger");
                }
            }
        }
        else if (_jumpsRemaining > 0)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, 0);
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpsRemaining--;

            if (animator != null)
            {
                animator.SetTrigger("JumpTrigger");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            if (_rigidbody2D.linearVelocityY <= 0.1f)
            {
                _jumpsRemaining = maxJumps;
                _wallJumpsRemaining = maxWallJumps; // Odnowienie limitu skoków od œciany po l¹dowaniu
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            if (_rigidbody2D.linearVelocityY <= 0.1f)
            {
                _jumpsRemaining = maxJumps;
                _wallJumpsRemaining = maxWallJumps; // Odnowienie limitu skoków od œciany po l¹dowaniu
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void OnHealthChanged(int oldHealth, int amountChanged, Vector3 origin)
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
        Vector2 knockbackDirection = new Vector2(transform.position.x - origin.x, 1f).normalized;
        _rigidbody2D.AddForce(knockbackDirection * enemyHitForce, ForceMode2D.Impulse);
    }
}