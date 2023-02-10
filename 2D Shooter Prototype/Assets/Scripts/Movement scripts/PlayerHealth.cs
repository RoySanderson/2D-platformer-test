using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public PlayerMovement playerMovement;
    public GameObject bloodEffect1, bloodEffect2, textPopup;

    public int maxHealth = 9;
    public int currentHealth;

    
    void Start()
    {
        bloodEffect1.SetActive(false);
        bloodEffect2.SetActive(false);
        currentHealth = maxHealth;
        globalLight.intensity = 0.7f;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 6 && currentHealth > 3)
        {
            if (textPopup)
                ShowTextPopup("'OW!'");
            playerMovement.moveSpeed = 6;
            bloodEffect1.SetActive(true);
            globalLight.intensity = 0.5f;
        }
        if (currentHealth <= 3 && currentHealth > 0)
        {
            if (textPopup)
                ShowTextPopup("'AH #*@%!'");
            playerMovement.moveSpeed = 5;
            bloodEffect2.SetActive(true);
            globalLight.intensity = 0.3f;
        }
        if (currentHealth <= 0)
        {
            bloodEffect1.SetActive(false);
            bloodEffect2.SetActive(false);
            playerMovement.Die();
        }
    }

    void ShowTextPopup(string text)
    {
        var go = Instantiate(textPopup, transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = text;
    }
}
