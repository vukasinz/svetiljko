using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;


public class budilnik : MonoBehaviour
{
    public GameObject bk;
    public GameObject player;
    public GameObject srce;
    public GameObject camera;
    public GameObject ending;
    public GameObject cig;
    public GameObject energy;
    public GameObject gameover;
    Boolean p = false;
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && bk.GetComponent<BudilnikController>().enabled == true&& player.GetComponent<PlayerMovement>().enabled == true) 
        {
            
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("EnemyTall");

            
            GameObject[] gameObjectArray2 = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject g in gameObjectArray2)
            {
                g.GetComponentInChildren<Enemy>().resetAllBools();
                g.GetComponent<Animator>().SetBool("isIdle", true);
                g.GetComponent<Animator>().SetBool("isIdle(D)", true);
            }
            foreach (GameObject go in gameObjectArray)
            {
                go.GetComponent<AIPath>().canMove = false;

            }
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerMovement>().resetAllBools();
            StartCoroutine(bk.GetComponent<BudilnikController>().Skracena());
            player.GetComponent<PlayerMovement>().resetAllBools();
            player.GetComponent<PlayerMovement>().anim.SetBool("isIdle", true);
           


        }
    }
    public GameObject presrbutton;
    public GameObject[] deathAnims;
    int index;
    public bool isRunning = false;
    public IEnumerator finis()
    {
        isRunning = true;
        player.GetComponent<PlayerMovement>().g = false;
        player.GetComponent<PlayerMovement>().speed = 0;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<PlayerMovement>().resetAllBools();
        player.GetComponent<Animator>().SetBool("isIdle", true);


        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("EnemyTall");

        foreach (GameObject go in gameObjectArray)
        {
            go.GetComponent<AIPath>().canMove  = false;
        }
        yield return new WaitForSeconds(1f);
        int r = Random.Range(0, 1);
        
        deathAnims[r].SetActive(true);
        player.GetComponent<SpriteRenderer>().sortingOrder = 0;
        
        yield return new WaitForSeconds(1.5f);
        Destroy(srce);
        Destroy(energy);

        player.GetComponent<PlayerMovement>().enabled = false;
        camera.transform.parent = null;
        float fov = camera.GetComponent<Camera>().orthographicSize;
        camera.GetComponent<Camera>().orthographicSize = fov * 0.9f;


        /* camera.transform.localPosition = new Vector3(-39.092f, 46.774f, -10f);

         yield return new WaitForSeconds(1f);

         //  cig.SetActive(false);
         ending.GetComponent<Animator>().SetBool("isDead", true);
         yield return new WaitForSeconds(2f);


         if (ending.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("budilnikKill") && ending.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !ending.GetComponent<Animator>().IsInTransition(0))
         {

             cig.SetActive(true);


             Destroy(ending);



         }*/
        camera.transform.localPosition = new Vector3(-40.37f, 64.37f, -10f);
        yield return new WaitForSeconds(1f);
        gameover.SetActive(true);
        yield return new WaitForSeconds(4f);
        presrbutton.SetActive(true);
        gameover.GetComponent<Animator>().SetBool("isLooping", true);
        Destroy(this.gameObject);

    }
    // Update is called once per frame
    void Update()
    {
       
    }

}
