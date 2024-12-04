using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 moveDir;
    public string lastD;
    public Vector2 rb_vel;
    bool notMoving = true;
    public GameObject player;
    public float speed = 3f;
    // Update is called once per frame
    private void FixedUpdate()
    {
        player.GetComponent<PlayerMovement>().energyBar.fillAmount = player.GetComponent<PlayerMovement>().energy / 100;
        rb.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
        rb_vel = rb.velocity;
        if (rb.velocity == new Vector2(0, 0))
        {
            player.GetComponent<PlayerMovement>().energy += 5 * Time.deltaTime;
            notMoving = true;
            switch (lastD)
            {
                case "left":
                    resetallbools();
                    anim.SetBool("isIdle(L)", true);
                    break;
                case "right":
                    resetallbools();
                    anim.SetBool("isIdle(R)", true);
                    break;


            }

        }
        else
        {
            player.GetComponent<PlayerMovement>().energy -= 5 * Time.deltaTime;
            notMoving = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 9f;
            anim.speed = 2;
            player.GetComponent<PlayerMovement>().energy -= 10 * Time.deltaTime;
        }
        else
        {
            speed = 6f;
            anim.speed = 1;
        }
    }
    void resetallbools()
    {
        foreach (var t in anim.parameters)
        {
            anim.SetBool(t.name, false);
        }
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
        if (moveX < 0)
        {
            lastD = "left";
        }
        else if (moveX > 0)
        {
            lastD = "right";
        }
        if (notMoving == false)
        {
            switch (lastD)
            {
                case "left":
                    resetallbools();
                    anim.SetBool("isWalking(L)", true);
                    break;
                case "right":
                    resetallbools();
                    anim.SetBool("isWalking(R)", true);
                    break;


            }
        }


    }
}
