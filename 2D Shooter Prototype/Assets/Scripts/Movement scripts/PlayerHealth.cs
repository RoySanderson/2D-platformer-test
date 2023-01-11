using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject bloodEffect1, bloodEffect2;

    public int maxHealth = 9;
    public int currentHealth;

    
    void Start()
    {
        bloodEffect1.SetActive(false);
        bloodEffect2.SetActive(false);
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 6 && currentHealth > 3)
        {
            playerMovement.moveSpeed = 6;
            bloodEffect1.SetActive(true);
        }
        if (currentHealth <= 3)
        {
            playerMovement.moveSpeed = 5;
            bloodEffect2.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            bloodEffect1.SetActive(false);
            bloodEffect2.SetActive(false);
            playerMovement.Die();
        }
    }

}
