using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _maxLife = 10.0f;
    [SerializeField] private AudioSource _sound1 = null;
    [SerializeField] private AudioSource _sound2 = null;
    private float _score = 0.0f;
    [SerializeField] private BoxCollider2D _hammerCollider = null;
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private bool _isRight = true;
    private bool _canHit = true;
    [SerializeField] private float _coolDown = 2.5f;
    public bool CanMove = false;
    public List<GroupieController> Groupie = null;
    #endregion Fields

    #region Properties
    public float Score { get => _score; }
    #endregion Properties

    #region Methods
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (CanMove == false)
        {
            return;
        }
        if (Groupie.Count > _maxLife)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        _rigidbody.velocity = Vector2.right * Input.GetAxis("Horizontal") * _speed;
        if (Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
        {
            _isRight = _rigidbody.velocity.x > 0.0f;
        }

        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
        _spriteRenderer.flipX = !_isRight;
        if (Input.GetButtonDown("Hit") && _canHit)
        {
            _sound1.Play();
            _sound2.Play();
            _animator.SetTrigger("Hit");
            _canHit = false;
            StartCoroutine(CoolDown());

            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (_isRight ? Vector3.right: Vector3.left), Vector2.one * 1.0f, 0.0f);;
            UnityEngine.Debug.Log("Collider " + colliders.Length);
            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<GroupieController>(out GroupieController controller))
                {
                    controller.Kill(_isRight, this);
                    _score++;
                }
            }
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDown);
        _canHit = true;
    }
    #endregion Methods
}
