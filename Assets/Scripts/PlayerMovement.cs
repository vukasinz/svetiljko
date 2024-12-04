using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
   public  Rigidbody2D rb;
    public Vector2 moveDir;
    public Animator anim;
    public string lastD;
    bool flg = false;
    public float energy = 100;
    public Image energyBar;

    
    public bool g = true;
    public bool dashing = false;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    public float x;
    public float y;

      IEnumerator Dashing()
      {
       
          float elapsedTime = 0f;
          float time = 0.25f;
        
        while (elapsedTime < time)
          {
              rb.AddForce(new Vector2(moveDir.x * 450f, moveDir.y * 450f));
              energy -= 2f;
              elapsedTime += Time.fixedDeltaTime;
                
            yield return new WaitForFixedUpdate();

          }
       
          rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(1f);
      

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        allEnemies = allEnemies.Concat(GameObject.FindGameObjectsWithTag("Budilnik")).ToArray();
        foreach (GameObject e in allEnemies)
        {
            if(e.GetComponent<PolygonCollider2D>() != null)
                 Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), e.GetComponent<PolygonCollider2D>(), false);
            else
                Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), e.GetComponent<BoxCollider2D>(), false);

        }
        dashing = false;


      }
    IEnumerator Animacija()
    {
       
        
        yield return new WaitForSeconds(0.7f);
        resetAllBools();
        anim.SetBool("isDashing", false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
      
        if (Input.GetKey(KeyCode.Z) && !dashing && energy >=39f)
        {
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
             allEnemies = allEnemies.Concat(GameObject.FindGameObjectsWithTag("Budilnik")).ToArray();
             foreach (GameObject e in allEnemies)
             {
                if (e.GetComponent<PolygonCollider2D>() != null)
                    Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), e.GetComponent<PolygonCollider2D>(), true);
                else
                    Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), e.GetComponent<BoxCollider2D>(), true);

            }
            anim.SetBool("isDashing", true);
            dashing = true;
            
            StartCoroutine(Animacija());
            StartCoroutine(Dashing());
        }
        if (g == true)
        {
            energyBar.fillAmount = energy / 100;
            Sprint();
            rb.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
            if (energy >= 100)
            {
                energy = 100;
            }
            if (energy < 0)
                energy = 0;
        }
    }
    
  void Sprint()
    {
       
            if (Input.GetKey(KeyCode.LeftShift))
            {

                if (energy > 0)
                {
                    if (moveDir.x != 0 || moveDir.y != 0)
                    {
                        speed = 7.5f;
                        anim.speed = 2;
                        energy -= 10 * Time.deltaTime;
                    }     
                    else
                        anim.speed = 2;


                }
            }
            else
            {
                energy += 4 * Time.deltaTime;
                speed = 5f;
                anim.speed = 1;

            }
        
        
    }
    
  
    public void resetAllBools()
    {
        foreach(var t in anim.parameters)
        {
            if (t.name == "isDashing")
                continue;
            else
                 anim.SetBool(t.name, false);

            
        }
    }
  
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("oci");
        foreach(GameObject g in enemies)
        {
            if(g.GetComponent<Enemy>().ukeban == true)
            {
                resetAllBools();
               
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<PlayerMovement>().enabled = false;
            }
        }

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
        else if (moveY > 0)
        {
            lastD = "up";
        }
        else if (moveY < 0)
        {
            lastD = "down";
        }


        if (moveX == 0 && moveY == 1)
        {
            resetAllBools();
            anim.SetBool("isWalking", true);
            anim.SetBool("isWalking(Up)", true);


        }
        if (moveX == 0 && moveY == -1)
        {
            resetAllBools();
            anim.SetBool("isWalking", true);
            anim.SetBool("isWalking(Down)", true);

            //pomera se na dole
        }
        if (moveX == 1)
        {
            resetAllBools();
            anim.SetBool("isWalking", true);
            anim.SetBool("isWalking(Right)", true);
            //pomera se desno mada moze i po dijagonalama gore-desno dole-desno 
        }
        if (moveX == -1)
        {
            resetAllBools();
            anim.SetBool("isWalking", true);
            anim.SetBool("isWalking(Left)", true);
            //pomera se levo isto po dijagonala moze
        }
        if (moveX == 0 && moveY == 0)
        {
            switch (lastD)
            {
                case "left":
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(Left)", true);

                    break;
                case "right":
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(Right)", true);
                    break;
                case "down":
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(Down)", true);
                    break;
                case "up":
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(Up)", true);
                    break;
            }

        }





    }
}
