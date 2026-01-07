using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _sprint = 2f;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    private int _jumpCount = 0;
    private int _maxJumps = 1;
    private Rigidbody _rb;
    private Vector3 _movementInput;
    private GroundCheck _groundCheck;
    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _groundCheck = GetComponent<GroundCheck>();
    }

    void MovePlayer()
    {
        Vector3 _targetPosition = _rb.position + _movementInput * _currentSpeed * Time.deltaTime;
        _rb.MovePosition(_targetPosition);
    }

    void RotatePlayer()

    {
        if (_movementInput != Vector3.zero)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_movementInput);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, _targetRotation, _rotationSpeed * Time.deltaTime));
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f , _rb.velocity.z);
        _rb.AddForce(Vector3.up * _jumpForce , ForceMode.Impulse);
        _jumpCount++;
    }
        // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _speed * _sprint;

        }
        else
        {
            _currentSpeed = _speed;
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        _movementInput = new Vector3(moveX, 0f , moveY).normalized;

        

       if (_groundCheck.IsGrounded())
        {
            _jumpCount = 0;
        }
       if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_groundCheck.IsGrounded() || _jumpCount < _maxJumps)
            {
                Jump();
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }
}
