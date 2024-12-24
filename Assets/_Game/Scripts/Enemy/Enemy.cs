using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public LayerMask playerLayers;
    [SerializeField] private Rigidbody2D rb;


    private bool _isGrounded;
    private IState _currentState;
    private bool _isRight = true;

    public float attackRange = 1.5f;
    public float attackDamage = 30f;
    public float attackArena = 0.5f;
    private Character _target;
    public Character Target => _target;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        attackPoint = transform.Find("AttackPoint");
        playerLayers = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (_currentState != null && !isDead)
        {
            _currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
        Debug.Log("Enemy Die");
    }

    public void ChangeState(IState newState)
    {
        // đổi sang State mới thì check state cũ có = null ko
        if (_currentState != null)
        {
            // nếu != null thì OnExit State cũ
            _currentState.OnExit(this);
        }

        //gán State mới
        _currentState = newState;

        if (_currentState != null)
        {
            // nếu != null thì truy cập vào OnEnter State mới
            _currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        animator.SetBool("run", true);
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        if (animator != null)
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", _isGrounded); // Chỉ bật "idle" nếu Enemy chạm đất
        }

        rb.velocity = Vector2.zero;
    }

    public void SetTarget(Character character)
    {
        this._target = character;

        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (_target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackArena, playerLayers);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Character>().OnHit(attackDamage);
            Debug.Log("hit" + player.name);
        }
    }

    public bool IsTargetInRange()
    {
        if (_target != null && Vector2.Distance(_target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }

        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWall"))
        {
            ChangeDiraction(!_isRight);
        }
    }

    public void ChangeDiraction(bool isRight)
    {
        this._isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackArena);
    }
}