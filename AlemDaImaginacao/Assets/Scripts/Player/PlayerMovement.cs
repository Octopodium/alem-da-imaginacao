using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region Declaration
    public static PlayerMovement instance { get; private set; }

    public float speed = 8f;
    public float jumpForce = 10f;
    
    private Vector3 movement;
    public Vector2 movementDirection;
    public Vector2 direction;
    private Rigidbody rb;
    
    private Actions actions => GameManager.instance.actions;

    private bool isGrounded;
    private bool isJumping;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheckPoint; 
    [SerializeField] private float _groundCheckSize = 0.2f; 
    [SerializeField] private LayerMask groundLayer;

    private float coyoteTime = 0.2f;
    private float coyoteTimer;

    [Header("Ref Camera")]
    public Transform meio;
    public Transform frente;
    public float tempoEasingDeGiroFrente = 0.1f;

    #endregion

    void Start(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }

        rb = GetComponent<Rigidbody>();
        
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        
        actions.Player.Move.performed += OnMovePerformed;
        actions.Player.Move.canceled += OnMoveCanceled;
        actions.Player.Jump.performed += OnJumpPerformed;


        GameManager.instance.HandlePlayerSpawn(this);
    }

    void Update()
    {
        if(isGrounded){
            coyoteTimer = coyoteTime;
        }else{
            coyoteTimer -= Time.deltaTime;
        }

        Movement();
        
        GroundCheck();
        
        if(isGrounded && rb.linearVelocity.y <= 0){
            isJumping = false;
        }
    }

    public void Movement(){
        if(movement.magnitude > 0){
            Vector3 velocity = new Vector3(movement.x * speed, rb.linearVelocity.y, 0);
            rb.linearVelocity = velocity;
        }else{
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    public void Move(Vector2 dir){
        movementDirection = dir;
        
        if(movementDirection.magnitude > 0f)  {
            direction = movementDirection.normalized;
            MudarDirecao(direction);
        }

        movement = new Vector3(movementDirection.x, 0, 0);
    }

    public void Jump()
    {
        if((isGrounded || coyoteTimer > 0) && !isJumping){
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            coyoteTimer = 0;
        }
    }

    private void GroundCheck(){
        isGrounded = Physics.CheckSphere(_groundCheckPoint.position, _groundCheckSize, groundLayer);
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx){
        Move(ctx.ReadValue<Vector2>());
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx){
        Move(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx){
        Jump();
    }

    private void OnDrawGizmos(){
        if(_groundCheckPoint != null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckSize);
        }
    }

    public void OnDisable(){
        if(actions != null){
            actions.Player.Move.performed -= OnMovePerformed;
            actions.Player.Move.canceled -= OnMoveCanceled;
            actions.Player.Jump.performed -= OnJumpPerformed;
        }
    }


    Coroutine c = null;
    void MudarDirecao(Vector3 dir) {
        if (meio.forward == dir.normalized) return;

        if (c != null) StopCoroutine(c);
        c = StartCoroutine(GirarMeioSuaveParaCamera(Mathf.Abs(meio.localEulerAngles.y) <= 90));
    }

    IEnumerator GirarMeioSuaveParaCamera(bool estaNaDireita = true) {
        float rotacaoInicial = meio.localEulerAngles.y;
        float rotacaoFinal = estaNaDireita ? 180f : 0f;
        float tempoPassado = 0f;

        while (tempoPassado < tempoEasingDeGiroFrente) {
            tempoPassado += Time.fixedDeltaTime;
            float yRot = Mathf.Lerp(rotacaoInicial, rotacaoFinal, tempoPassado / tempoEasingDeGiroFrente);
            meio.localRotation = Quaternion.Euler(0,yRot,0);
            yield return new WaitForFixedUpdate();
        }

        meio.localRotation = Quaternion.Euler(0,rotacaoFinal,0);


        c = null;
    }
}