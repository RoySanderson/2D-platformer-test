using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsCeiling;
    [SerializeField] private float jumpForce, moveSpeed, crouchSpeed;   
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bulletPrefab;
    private float axisX;
    private bool isCrouching, isFacingLeft;

    private Animator anim;
    private Rigidbody2D myBody;
    private BoxCollider2D playerHead;
    private CircleCollider2D playerFeet;

    private enum MovementState { idle, running, jumping, falling }


    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHead = GetComponent<BoxCollider2D>();
        playerFeet = GetComponent<CircleCollider2D>();
    }


    private void FixedUpdate()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        myBody.velocity = new Vector2(axisX * moveSpeed, myBody.velocity.y);
    }

    private void Update()
    {
        Crouch();
        UpdateAnimationState();

        if (Input.GetButtonDown("Crouch") && !isCrouching)
        {
            isCrouching = true;
            anim.SetBool("Crouching", true);
            moveSpeed *= crouchSpeed;
        } 
        else if (Input.GetButtonUp("Crouch") && !CeilingCheck())
        {
            isCrouching = false;
            anim.SetBool("Crouching", false);
            moveSpeed *= 2;
        }
            
        if (Input.GetButtonDown("Jump") && IsGrounded() && !isCrouching)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1") && IsGrounded() && !isCrouching)
        {
            Shoot();
            anim.SetBool("Shoot", true);
        }
        else if (Input.GetButtonUp("Fire1"))
            anim.SetBool("Shoot", false);
    }


    private void UpdateAnimationState()
    {
        MovementState state;

        if (axisX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            state = MovementState.running;
            isFacingLeft = false;
        }
        else if (axisX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            state = MovementState.running;
            isFacingLeft = true;
        }
        else
            state = MovementState.idle;

        if (myBody.velocity.y > 0.1f && !IsGrounded())
            state = MovementState.jumping;
        else if (myBody.velocity.y < -0.1f && !IsGrounded())
            state = MovementState.falling;

        anim.SetInteger("State", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.CircleCast(playerFeet.bounds.center, playerFeet.radius, Vector2.down, 0.1f, whatIsGround);
    }

    private bool CeilingCheck()
    {
        return Physics2D.CircleCast(playerFeet.bounds.center, playerFeet.radius, Vector2.up, 0.1f, whatIsCeiling);
    }

    private void Crouch()
    {
        if (isCrouching == true)
            playerHead.enabled = false;
        else if (isCrouching == false)
            playerHead.enabled = true;
    }

    private void Shoot()
    {
        GameObject b = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        b.GetComponent<Projectile>().StartShoot(isFacingLeft);
    }

    private void Die()
    {
        playerHead.isTrigger = true;
        playerFeet.enabled = false;
        myBody.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

