using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity; //keep track of gravity and jumping

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    public bool canAttack = true;
    public float attackCooldown = 1f;
    
    
    [SerializeField] private float jumpHeight;
    //ComboAttack
   

    // REFERENCES
    private CharacterController controller;
    private Animator anim;
    public CapsuleCollider weaponCollider;
    public ParticleSystem swordParticle;
    public Transform weaponHolder;
    public Combo combo;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        combo = GetComponentInChildren<Combo>();
        weaponCollider.enabled = false;
        // swordParticle.Play();
        swordParticle.Stop();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown((KeyCode.Mouse0)))
        {
            if (canAttack)
            {
                combo.NormalAttack();
                if (weaponHolder.childCount > 1)
                {
                    swordParticle.Play();
                }
            }
        }
    }

    private void Move()
    {
       
        
        
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        //remember to set the terrain layer to terrain
        if (isGrounded && velocity.y < 0) // it stops applying gravity when we are grounded
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection =
            transform.TransformDirection(
                moveDirection); // to po to zeby osie były git, tzn jak obrocimy gracza to nadal klawisz W to pojscie do przodu

        System.Console.WriteLine("Hello World!");
        if (isGrounded) // dont move if we jump or fall or sth
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Run
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                //idle
                Idle();
            }

            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }


        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetTrigger("Jump");
    }

    private void Attack()
    {
        weaponCollider.enabled = true;
        anim.SetTrigger("Attack");
        canAttack = false;
        swordParticle.Stop();
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        weaponCollider.enabled = false;
    }

  
    
    
}