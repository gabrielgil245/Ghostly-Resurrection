using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier;
    
    private Rigidbody2D _rb;
    private float _horizontalInput;
    private bool _canJump = true;
    public bool _isGhost = false;
    private float _verticalInput;
    
    // Start is called before the first frame update
    void Awake() {
        _rb = GetComponent<Rigidbody2D>();    
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetHorizontalInput();
        GetVerticalInput();
        SetFacingDirection();
        if(_isGhost) {
            Floating();
            HorizontalMovement();
            VerticalMovement();
        } else {
            Jump();
            Falling();
            HorizontalMovement();
        }
    }

    void GetHorizontalInput() {
        _horizontalInput = Input.GetAxis("Horizontal");
    }
    
    void GetVerticalInput() {
        _verticalInput = Input.GetAxis("Vertical");
    }

    void Jump() {
        if(Input.GetButtonDown("Jump") && _canJump == true) {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    void Falling() { // When player's velocity is within or beyond their jump force
        if(_rb.velocity.y > -_jumpForce && _rb.velocity.y < _jumpForce) { 
            _rb.gravityScale = _fallMultiplier;
        } else {
            _rb.gravityScale = 1f;
        }
    }

    void Floating() {
        _rb.Sleep();
        _rb.gravityScale = 0f;
        _rb.WakeUp();
    }

    void HorizontalMovement() {
        if(Mathf.Abs(_horizontalInput) > 0) {
            transform.Translate(Vector2.right * _horizontalInput * _speed * Time.deltaTime);
        }
    }

    void VerticalMovement() {
        if(Mathf.Abs(_verticalInput) > 0) {
            transform.Translate(Vector2.up * _verticalInput * _speed * Time.deltaTime);
        }
    }

    void SetFacingDirection() {
        if(_horizontalInput > 0) {
            transform.localScale = new Vector2(1, 1);
        } else if(_horizontalInput < 0) {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            _canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            _canJump = false;
        }
    }
}
