using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour
{
    public float destroyTime = 1.5f;
    public Vector3 offset = new Vector3(0, 2, 0);
   
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
}
