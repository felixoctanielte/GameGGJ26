using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{
    public bool isGrounded;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("MOVEMENT")]
    public float runSpeed = 14f;
    private float horizontalInput;

    [Header("MOVEMENT FEEL")]
    public float acceleration = 70f;
    public float deceleration = 90f;
    public float velocityPower = 0.9f;

    [Header("DRAG")]
    public float groundDrag = 8f;
    public float airDrag = 2f;

    [Header("JUMP")]
    public float jumpForce = 18f;
    public int maxJumps = 2;
    private int jumpCount;

    [Header("BETTER GRAVITY")]
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 4.5f;

    [Header("COYOTE TIME")]
    public float coyoteTime = 0.12f;
    private float coyoteCounter;

    [Header("JUMP BUFFER")]
    public float jumpBufferTime = 0.12f;
    private float jumpBufferCounter;

    [Header("GROUND CHECK")]
    public Transform groundCheckPoint;
    public float checkRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("SQUASH & STRETCH")]
    public float runSquashAmount = 0.08f;
    public float runSquashSpeed = 14f;
    private float runTimer;

    public float landSquashAmount = 0.15f;
    private bool wasGrounded;

    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        GroundCheck();
        ApplyDrag();

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            jumpCount = 0;
        }
        else coyoteCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        HandleJump();
        BetterJumpGravity();
        RunSquashEffect();
        LandingSquash();
        FlipSprite();
    }

    void FixedUpdate()
    {
        float targetSpeed = horizontalInput * runSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velocityPower) * Mathf.Sign(speedDiff);

        rb.AddForce(movement * Vector2.right);

        // limit max speed
        if (Mathf.Abs(rb.velocity.x) > runSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * runSpeed, rb.velocity.y);

        // stop pelan biar natural
        if (Mathf.Abs(horizontalInput) < 0.01f && isGrounded)
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.2f), rb.velocity.y);
    }

    void ApplyDrag()
    {
        rb.drag = isGrounded ? groundDrag : airDrag;
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, groundLayer);
    }

    void HandleJump()
    {
        if (jumpBufferCounter > 0 && (coyoteCounter > 0 || jumpCount < maxJumps))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpBufferCounter = 0;
            jumpCount++;
            isGrounded = false;

            
        }
    }

    void BetterJumpGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void RunSquashEffect()
    {
        if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f)
        {
            runTimer += Time.deltaTime * runSquashSpeed;
            float xScale = 1 + Mathf.Sin(runTimer) * runSquashAmount;
            float yScale = 1 - Mathf.Sin(runTimer) * runSquashAmount;
            transform.localScale = new Vector3(originalScale.x * xScale, originalScale.y * yScale, 1);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 12f);
        }
    }

    void LandingSquash()
    {
        if (!wasGrounded && isGrounded)
        {
            transform.localScale = new Vector3(originalScale.x * (1 + landSquashAmount), originalScale.y * (1 - landSquashAmount), 1);
            StartCoroutine(LandRecover());
           
        }

        wasGrounded = isGrounded;
    }

    IEnumerator LandRecover()
    {
        yield return new WaitForSeconds(0.08f);
        transform.localScale = originalScale;
    }

    void FlipSprite()
    {
        if (horizontalInput > 0) spriteRenderer.flipX = false;
        else if (horizontalInput < 0) spriteRenderer.flipX = true;
    }
}
