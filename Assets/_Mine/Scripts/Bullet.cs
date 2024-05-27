using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("DestroyThis", 0.01f);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
