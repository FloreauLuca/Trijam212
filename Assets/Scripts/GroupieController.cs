using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupieController : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _force = 5.0f;
    private SpriteRenderer _spriteRenderer = null;
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    private bool _isRight = true;
    #endregion Fields

    #region Methods
    public void SetRight(bool right)
    {
        _isRight = right;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (!_rigidbody)
            return;
        _rigidbody.velocity = (_isRight ? -1.0f : 1.0f) * Vector2.right * _speed;
        _spriteRenderer.flipX = _isRight;
    }

    public void Kill(bool right, PlayerController player)
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.velocity = (new Vector2(right ? _force : -_force, _force));
        _rigidbody = null;
        GetComponent<Collider2D>().isTrigger = true;
        player.Groupie.Remove(this);
        Destroy(gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            UnityEngine.Debug.Log("OnTriggerEnter2D" + player.Groupie.Count);
            if (!player.Groupie.Contains(this))
            {
                player.Groupie.Add(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.Groupie.Remove(this);
        }
    }
    #endregion Methods
}
