using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip, isFacingLeft, canShoot = true;
    public float patrolSpeed, range, timeBTWShots;
    private float xDistToPlayer, yDistToPlayer;
    private Animator anim;

    public EnemyHealth enemyHealth;
    public Rigidbody2D rb;
    public Transform groundCheck, player, firepoint;
    public LayerMask groundLayer;
    public GameObject bulletPrefab, eyeGlow;



    void Start()
    {
        anim = GetComponent<Animator>();
        mustPatrol = true;
        eyeGlow.SetActive(false);
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        xDistToPlayer = Mathf.Abs(transform.position.x - player.position.x);
        yDistToPlayer = Mathf.Abs(transform.position.y - player.position.y);

        if (xDistToPlayer <= range && yDistToPlayer < 1.5f)
        {
            if (player.position.x > transform.position.x && transform.localScale.x > 0 || player.position.x < transform.position.x && transform.localScale.x < 0)
            {
                Flip();
            }
            eyeGlow.SetActive(true);
            mustPatrol = false;
            rb.velocity = Vector2.zero;
            anim.enabled = false;

            if (canShoot)
                StartCoroutine(Shoot());
        }
        else
        {
            eyeGlow.SetActive(false);
            mustPatrol = true;
            anim.enabled = true;
        }
            
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustFlip)
        {
            Flip();
        }

        rb.velocity = new Vector2(patrolSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        patrolSpeed *= -1;
        mustPatrol = true;

        if (transform.localScale.x < 0)
            isFacingLeft = false;
        else if (transform.localScale.x > 0)
            isFacingLeft = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBTWShots);
        GameObject b = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        b.GetComponent<Projectile>().StartShoot(isFacingLeft);
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHealth.TakeDamage(5);
        }
            
    }
}
