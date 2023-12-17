using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    //Attack variables
    public float attackRange = 0.5f;
    public float attackDamage = 30f;
    [SerializeField] Transform _attackPoint;

    //Health variables
    private float _currentHealth;
    [SerializeField] float _maxHealth;
    bool _isDead = false;
    [SerializeField] HealthBar _healthBar;

    //Move variables
    private bool _isFacingRight = false;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] float _speed = 3f;

    //States variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _factory;

    //Components variables
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb2d;
    [SerializeField] Transform _spawnTransform;

    //getters/setters
    public float Speed { get { return _speed; } set { _speed = value; } }
    public Rigidbody2D Rb2d { get { return _rb2d; } set { _rb2d = value; } }
    public float HorizontalInput { get { return _horizontalInput; } set { _horizontalInput = value; } }
    public float VerticalInput { get { return _verticalInput; } set { _verticalInput = value; } }
    public Animator Animator { get { return _animator; } set { _animator = value; } }
    public bool IsFacingRight { get { return _isFacingRight; } set { _isFacingRight = value; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    public LayerMask EnemyLayer { get => _enemyLayer; set => _enemyLayer = value; }
    public Transform AttackPoint { get => _attackPoint; set => _attackPoint = value; }
    public Transform SpawnTransform { get => _spawnTransform; set => _spawnTransform = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    //properties
    public bool IsWalking
    {
        get
        {
            Vector2 movement = new(_horizontalInput, _verticalInput);
            if (movement.magnitude > 0f)
            {
                return true;
            }
            return false;
        }
        private set
        {
            _isFacingRight = value;
        }
    }
    public HealthBar HealthBar { get => _healthBar; set => _healthBar = value; }


    #region States
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }






    #endregion

    private void Awake()
    {
        transform.position = _spawnTransform.position;
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _factory = new PlayerStateFactory(this);
        _currentState = _factory.Idle();
    }
    private void Start()
    {
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
    
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _animator.SetTrigger("Hurt");
        _healthBar.SetHealth(_currentHealth);
        if(_currentHealth <= 0f)
        {
            _isDead = true;
            _animator.SetBool("IsDead", _isDead);
            _currentState = _factory.Dead();
        }
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint == null) return;

        Gizmos.DrawWireSphere(_attackPoint.position, attackRange);
    }
}
