using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemyContext : MonoBehaviour
{
    //graphics
    [SerializeField]SpriteRenderer spriteRenderer;

    //animation
    Animator _animator;

    //movement
    [SerializeField] float _speed = 2.5f;
    Rigidbody2D _rb2d;

    //raycast
    [SerializeField] Transform _raycast;
    [SerializeField] LayerMask _raycastLayer;
    public float raycastLength;
    RaycastHit2D _raycastHit;
    GameObject _target;
    float _distance;
    bool _isInRange;


    //health
    [SerializeField] float _maxHealth;
    float _currentHealth;
    bool _isDead = false;

    //attack
    bool _isAttack;
    public float attackDamage = 30f;
    public float attackDistance = 2f;
    public float timer;
    float initTimer;

    public bool IsInRange { get => _isInRange; set => _isInRange = value; }
    public GameObject Target { get => _target; set => _target = value; }

    private void Awake()
    {
        initTimer = timer;
        timer = 0;
        _currentHealth = _maxHealth;
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_isInRange && !_isDead)
        {
            var targetScript = _target.GetComponent<PlayerContext>();

            if (!targetScript.IsDead)
            {
                var direction = (_target.transform.position - _raycast.position).normalized;
                _raycastHit = Physics2D.Raycast(_raycast.position, direction, raycastLength, _raycastLayer);
                RaycastDebugger();
                if (_raycastHit.collider)
                {
                    EnemyLogic();
                }
            }
            else
            {
                _animator.SetBool("IsWalking", false);
                _isAttack = false;
            }
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            _isAttack = false;
        }
    }
    
    void EnemyLogic()
    {
        _distance = Vector2.Distance(transform.position, _target.transform.position);
        
        if(_distance > attackDistance)
        {
            Move();
            _isAttack = false;
        } 
        else
        {
            Attack();
        }
    }

    void Move()
    {
        _animator.SetBool("IsWalking", true);
        Flip();
        var target = new Vector2(_target.transform.position.x, _target.transform.position.y);
        _rb2d.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }
    void Attack()
    {
        _animator.SetBool("IsWalking", false);
        timer -= Time.deltaTime;
        _isAttack = true;
        if(timer <= 0)
        {
            _animator.SetTrigger("Attack");
            timer = initTimer;
        }
    }
    void RaycastDebugger()
    {
        if(_distance > attackDistance)
        {
            Debug.DrawLine(_raycast.position, _raycastHit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(_raycast.position, _raycastHit.point, Color.green);
        }
    }
    void Flip()
    {
        Vector2 scale = transform.localScale;
        if (_target.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }
    public void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _currentHealth -= damage;
            _animator.SetTrigger("Hurt");
            if (_currentHealth <= 0f)
            {
                _isDead = true;
                Dead();
            }
        }
    }

    void Dead()
    {
        _animator.SetBool("IsDead", _isDead);
        StartCoroutine(DeadDelay(2.5f));
    }
    IEnumerator DeadDelay(float seconds)
    {
        GetComponentInChildren<Collider2D>().enabled = false;
        spriteRenderer.sortingOrder = 9;
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
