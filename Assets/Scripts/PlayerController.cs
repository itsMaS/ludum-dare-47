using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speedNormal;
    public float speedInAir;
    public float jumpAmount;
    public float jumpInitialAmount;
    public float jumpChangeTime;

    private bool inAir = true;


    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.AddForce(Input.GetAxis("Horizontal")* Vector2.right * (!inAir ? speedNormal : speedInAir));
        if(Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        float jumpTime = 0;
        rb.AddForce(Vector2.up * jumpInitialAmount, ForceMode2D.Impulse);
        inAir = true;
        while(Input.GetKey(KeyCode.Space) && jumpTime < jumpChangeTime)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            inAir = false;
        }
    }
}
