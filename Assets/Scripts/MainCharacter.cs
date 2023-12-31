using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    private float _moveSpeed;
    private bool _moveable;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        _moveSpeed = 6.0f;
        _moveable = true;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        _animator.SetBool("walk", false);

        if (_moveable) { 

            if (Input.GetKey(KeyCode.A))
            {
                if (transform.position.x > -26.3f)
                {
                    transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
                }
                _spriteRenderer.flipX = true;
                _animator.SetBool("walk", true);

            }

            else if (Input.GetKey(KeyCode.D))
            {
                if (transform.position.x < 44f)
                {
                    transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
                }
                _spriteRenderer.flipX = false;
                _animator.SetBool("walk", true);
            }



        }     
    }

    public void SetMoveable(bool b)
    {
        _moveable = b;
    }

    public bool GetMoveable()
    {
        return _moveable;
    }
    
}
