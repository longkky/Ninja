using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = System.Numerics.Vector3;

public class PlayerAttack : Character
{
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] public Transform attackPoint;
    [SerializeField] private Player player;
    [SerializeField] private Kunai kunaiPrefab;

    [SerializeField] protected float attackDelay = 1.5f; //attackspeed
    [SerializeField] protected float attackTimer = 0f;

    private bool _isAttacking;
     public float attackArea = 0.3f;
    public float attackDamage = 20f;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        attackPoint = transform.Find("AttackPoint");
        enemyLayers = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (player.IsDead()) return;
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay && Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        if (attackTimer >= attackDelay && Input.GetKeyDown(KeyCode.U))
        {
            Throw();
        }
    }

    public void Attack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        animator.SetTrigger("attack");
        attackTimer = 0;
        // Đợi cho đến khi hoạt ảnh tấn công kết thúc
        Invoke(nameof(EndAttack), attackDelay);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackArea, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Character>().OnHit(attackDamage);
            Debug.Log("hit" + enemy.name);
        }
    }
    public void Throw()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        animator.SetTrigger("throw");

        Instantiate(kunaiPrefab, attackPoint.position, attackPoint.rotation);
        Invoke(nameof(EndAttack), attackDelay);
    }

    private void EndAttack()
    {
        _isAttacking = false;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackArea);
    }
}