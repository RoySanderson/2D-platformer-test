using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Rigidbody2D rb2d;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void StartShoot(bool isFacingLeft)
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (isFacingLeft)
        {
            rb2d.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("bulletCollision");
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void DestroyOnImpact()
    {
        Destroy(gameObject);
    }




}
