using System;
using UnityEngine;

public class PlayerMovement : Character
{
    [Header("Settings")] [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private PlayerAttack playerAttack;

    private float _horizontal;
    private bool _isGrounded;

    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (_player.IsDead()) return;

        HandleInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (_player.IsDead()) return;
        if (playerAttack.IsAttacking() && !_isGrounded)
        {
            return;
        }
        if (playerAttack.IsAttacking())
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            Move();
        }
        Flip();
    }

    private void HandleInput()
    {
        //_horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.K))
        {
            Jump();
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool("run", Mathf.Abs(_horizontal) > 0.1f);
        animator.SetBool("grounded", _isGrounded);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _isGrounded = false;
            animator.SetTrigger("jump");
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(_horizontal * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        // Chỉ Flip khi hướng thay đổi
        if (_horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        else if (_horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void SetMove(float horizontal)
    {
        _horizontal = horizontal;
    }
}