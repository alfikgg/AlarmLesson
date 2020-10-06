using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    
    private Rigidbody2D _body;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        _body.velocity = new Vector2(horizontal * _speed, _body.transform.position.y);
        if (_body.velocity.x > 0 || _body.velocity.x < 0)
        {
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }
    }
}
