using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 startPos;
    public float acceleration= 1.0f;
    public float jumpForce= 10.0f;
    private bool isJumping=false;
    private Rigidbody2D rb = null;
   //private Vector2 startPos = transform.position;

   
   //private Vector2 startPosx = Vector2.x;
   //private Vector2 startPosy = Vector2.y;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        startPos=transform.position;
        
    }
    bool isGrounded(){
        int layerMask = LayerMask.GetMask("Ground");
        Vector2 offset = new Vector2(0.0f, 0.0f);
        Vector2 position= new Vector2(transform.position.x,transform.position.y);
        return Physics2D.Raycast(position + offset, Vector2.down, 0.6f, layerMask);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJumping=true;
        }
        
       
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float mass = rb.mass;
        if(Input.GetKey(KeyCode.A))
        {
            Vector2 movement = new Vector2(-acceleration * mass,0.0f);
            rb.AddForce(movement,ForceMode2D.Force);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Vector2 movement = new Vector2(acceleration * mass,0.0f);
            rb.AddForce(movement,ForceMode2D.Force);

        }
        if (isJumping && isGrounded())
        {
            isJumping=false;
            rb.AddForce(new Vector2(0.0f,jumpForce), ForceMode2D.Impulse);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.CompareTag("Trigger"))
        {
            Die();
          //  transform.position.x = startPosx;
          //  transform.position.y = startPosy;
           // rb.velocity = Vector3.zero;
           
        }
        if(collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

     void Die(){
          transform.position = startPos;
    }
  

}
