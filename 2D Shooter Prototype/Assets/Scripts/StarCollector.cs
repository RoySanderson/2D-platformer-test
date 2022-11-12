using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCollector : MonoBehaviour
{
    [SerializeField] private Text starsCollectedText;
    private int star = 0;
    private CircleCollider2D coll;
    

    private void Start()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            AntiDoublePickup antiDoublePickup = collision.gameObject.GetComponent<AntiDoublePickup>();
            if (!antiDoublePickup.isUsed) 
            { 
                Destroy(collision.gameObject);
                star++;
                starsCollectedText.text = "Stars collected: " + star;
                antiDoublePickup.isUsed = true;
            }
        }   
    }

} 
