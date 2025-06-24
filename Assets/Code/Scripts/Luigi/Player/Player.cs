using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [Header("References")]
    public Rigidbody _rigidbody;
    public Camera _camera;

    [Header("Movement Settings")]
    protected float jumpForce;

    [Header("Rotation Settings")]
    protected float sensibility;
    private float OrizontalRotation = 0f;
    private float VerticallRotation = 0f;


    [Header("Ground Check Settings")]
    public float groundCheckDistance = 1f;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Input Actions")]
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference look;

    private Vector3 _moveDirection;
    private Player_Data playerData;


    protected override void Awake()
    {
        base.Awake();
        playerData = (Player_Data)characterData;

        if (playerData != null)
        {
            jumpForce = playerData.jumpForce;
            sensibility = playerData.sensibility;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} non ha un PlayerData assegnato!");
            health = new Health(100);
        }
    }


    private void OnEnable()
    {
        jump.action.performed += OnJumpPerformed;
        look.action.performed += OnLookPerformed;

        look.action.Enable();
        move.action.Enable();
        jump.action.Enable();
    }

    private void OnDisable()
    {
        jump.action.performed -= OnJumpPerformed;
        look.action.performed -= OnLookPerformed;

        look.action.Disable();
        move.action.Disable();
        jump.action.Disable();
    }

    private void Update()
    {
        _moveDirection = move.action.ReadValue<Vector3>();
        _moveDirection.Normalize();

        // Raycast verso il basso per controllare se il player a terra
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance + 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        //if (isGrounded)
        //{
            Vector3 movementDirection = (transform.rotation * _moveDirection) * moveSpeed;
            Vector3 velocity = new Vector3(movementDirection.x, _rigidbody.linearVelocity.y, movementDirection.z);
            _rigidbody.linearVelocity = velocity;
        //}
    }



    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // Salto
        if (isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);
        }
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        float mouseX = Input.GetAxis("Mouse X") * playerData.sensibility;
        float mouseY = Input.GetAxis("Mouse Y") * playerData.sensibility;

        OrizontalRotation += mouseX;
        VerticallRotation += mouseY;
        VerticallRotation = Mathf.Clamp(VerticallRotation, -30f, 0f); // Limita la rotazione

        // Applicare la rotazione alla fotocamera e al giocatore
        transform.rotation = Quaternion.Euler(0, OrizontalRotation, 0);
        _camera.gameObject.transform.rotation = Quaternion.Euler(-VerticallRotation, OrizontalRotation, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        Gizmos.DrawLine(origin, origin + Vector3.down * (groundCheckDistance + 0.2f));
    }

}
