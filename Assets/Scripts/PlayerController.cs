using Cinemachine;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator ani;
    //public float MoveSpeed = 5f;
   // public float sprintMultiplier = 2f;
   // private float horizontalInput;
   // private float verticalInput;
   // private Vector3 inputVector;
    public float speed = 15f;
    float moveFB, moveLR;
    float gravity = -9.8f *2;
    private Rigidbody rb;
    public CapsuleCollider capsuleCollider;
    public Transform cameraTransform;
    public float jumpforce = 12f;
    public float GroundDistance = 0.5f;
    public LayerMask GroundMask;
    public float RollForce = 15f;
    public GameObject dust , snowpart;


    private bool isGrounded = false;
    private bool isRolling = false;
    private bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
       capsuleCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;

        //rb.freezeRotation = true;
    }

   
   

    void FixedUpdate()
    {
        HandleInput();
        CheckGroundStatus();
        //MovePlayer();
        //UpdateAnimator();
        dust.transform.position = transform.position;
        snowpart.transform.position = transform.position;


    }



    public void HandleInput()


     
    {
        if (isRolling || isAttacking) return;

        moveLR = Input.GetAxis("Horizontal");
        moveFB = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveLR * speed  + transform.forward * moveFB *speed ;

        if (Input.GetKey(KeyCode.C))
        {
            if (moveFB != 0 || moveLR != 0)
            {
                Roll(moveLR, moveFB);
            }
            
        }


        if (isGrounded)
        {

            

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightShift))
            {
                print("jump");
                movement.y = jumpforce;
                movement = new Vector3(0, jumpforce, 0);
                ani.SetTrigger("jump");
            }
            
            else
            {
                print("into the else");
                movement.y = 0;

            }
        }
        else 
        {
            movement.y = rb.velocity.y + gravity * Time.fixedDeltaTime;
        }
      
        float angle = Mathf.Atan2(moveLR, moveFB) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
       
        rb.velocity = movement;

        ani.SetFloat("SpeedX", moveLR);
        ani.SetFloat("SpeedZ", moveFB);

       
    }

    private void Roll(float moveLR, float moveFB)
    {
        if (!isRolling)
        {
            isRolling = true;
            if(moveFB == 1)
            {
              ani.SetTrigger("roll");   
            }

            if (moveLR == -1)
            {
                ani.SetTrigger("RollLeft");
            }

            Vector3 rollDirection = (transform.right*moveLR + transform.forward*moveFB).normalized; 
            rb.AddForce(rollDirection* RollForce, ForceMode.Impulse);

            Invoke("EndRoll", 0.5f);

        }
    }

    private void EndRoll()
    {
        isRolling = false;  
        rb.velocity = Vector3.zero;
        ani.SetFloat("SpeedX", 0);
        ani.SetFloat("SpeedZ", 0);
    }



    void CheckGroundStatus()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,Vector3.down, out hit , GroundDistance,GroundMask)) 
        {
           
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * GroundDistance);
    }
    public void OnAttackStart()
    {
        isAttacking = true;
        rb.velocity = Vector3.zero; 
    }
    public void OnAttackEnd()
    {
        isAttacking = false;
    }


}