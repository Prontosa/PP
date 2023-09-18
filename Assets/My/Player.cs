using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private BoxCollider2D collLeg;
    [SerializeField] private LayerMask isGround;
    

    private bool isGrounded;

    private void Awake()
    {
        if (collLeg ==  null)
        {
            collLeg = transform.Find("Leg").GetComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        ChackGround();
        Moving();
        Jump();
    }

    void ChackGround()
    {
        isGrounded = collLeg.IsTouchingLayers(isGround);
    }


    void Moving()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 moveVelocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, rigid.velocity.y);
        rigid.velocity = new Vector2(moveVelocity.x / Time.deltaTime, rigid.velocity.y);
        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        //Vector2 moveVelocity = new Vector2(horizontalInput * moveSpeed , rigid.velocity.y);
        //rigid.velocity = moveVelocity;

    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
            rigid.velocity = new Vector2(rigid.velocity.x , jumpForce);
        }
        else
        {
            isGrounded = true;
        }
    }

    private void OncollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
