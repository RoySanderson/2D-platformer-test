using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public AIPatrol aIPatrol;
    public GameObject damageEffect, textPopup, starPrefab;

    public int maxHealth = 10;
    public int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        damageEffect.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 5 && currentHealth > 0)
        {
            if (textPopup)
                ShowTextPopup("'@#!'");
            damageEffect.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            damageEffect.SetActive(false);
            Instantiate(starPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    void ShowTextPopup(string text)
    {
        var go = Instantiate(textPopup, transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = text;
    }
}
