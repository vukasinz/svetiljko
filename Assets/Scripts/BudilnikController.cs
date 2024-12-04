using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BudilnikController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool found = false;
    public GameObject player;
    public GameObject ob;
    public GameObject debris;
    public GameObject srce;
    Animator debriAnim;
    public bool jurnjava = false;
    Animator anim;
    public GameObject bk;
    public void resetAllBoolsPlayer()
    {
        foreach (var t in player.GetComponent<Animator>().parameters)
        {

            player.GetComponent<Animator>().SetBool(t.name, false);

        }
    
    }
    void Start()
    {
        debriAnim = debris.GetComponent<Animator>();
        anim = this.GetComponent<Animator>();
    }
    bool flag = false;
    bool skracena = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && flag == false && player.GetComponent<PlayerMovement>().enabled == true)
        {
            jurnjava = true;
            found = true;
            StartCoroutine(Bomb());

            ob.GetComponent<AIDestinationSetter>().enabled = true;
            ob.GetComponent<AIPath>().enabled = true;
            ob.GetComponent<AIPath>().maxSpeed = 7f;
        }
    }
    public Vector2 direction;
    private void FixedUpdate()
    {
        direction = (player.transform.position - this.transform.position).normalized;
    }
    public IEnumerator Skracena()
    {
        resetAllBoolsPlayer();
        print("ALOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
        skracena = true;
        flag = true;
        player.GetComponent<PlayerMovement>().g = false;
        this.GetComponent<AudioSource>().Play();
        player.GetComponent<PlayerMovement>().speed = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
        resetAllBoolsPlayer();
        anim.SetBool("WalkingB", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Idle", false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        anim.SetBool("explosion", true);
        srce.GetComponent<Animator>().SetBool("vremeJe", true);    
        this.gameObject.transform.localScale = new Vector3(5, 5, 5);
        yield return new WaitForSeconds(0.7f);
        ob.GetComponent<AIDestinationSetter>().enabled = false;
        ob.GetComponent<AIPath>().enabled = false;
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        debriAnim.SetBool("Debris", true);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(bk.GetComponent<budilnik>().finis());

    }
   public  IEnumerator Bomb()
    {

        flag = true;
       
        yield return new WaitForSeconds(5f);
        this.GetComponent<AudioSource>().Play();



        anim.SetBool("WalkingB", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Idle", false);
        anim.SetBool("explosion", true);
        this.gameObject.transform.localScale = new Vector3(5, 5, 5);
          ob.GetComponent<AIDestinationSetter>().enabled = false;
           ob.GetComponent<AIPath>().enabled = false;
        yield return new WaitForSeconds(0.7f);
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        debriAnim.SetBool("Debris", true);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BudilnikController>().enabled = false;
        jurnjava = false;


        //  Destroy(this.gameObject);
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (found == true)
        {
            if (direction.y >= 0.5)
            {
                anim.SetBool("WalkingB", true);
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", false);

            }
            else
            {
                anim.SetBool("WalkingB", false);
                anim.SetBool("Walking", true);
                anim.SetBool("Idle", false);
            }
        }
    }
}
