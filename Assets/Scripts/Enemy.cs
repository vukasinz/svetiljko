using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

namespace Pathfinding
{
    public class Enemy : MonoBehaviour
    {

        public Vector2 direction;
        public GameObject enemy;
        public Animator anim;
        public float distance;
        public GameObject player;
        public PolygonCollider2D oci;
        public bool invisible = false;
        PolygonCollider2D playerCol;
        public Rigidbody2D rb;
        public int moveX = 0;
        public int moveY = 0;
        public int flag = 0;
        public float speed = 400f;
        public GameObject gameover;
        public Transform mesto;
        public bool ukeban = false;
        public GameObject energy;
        public AudioSource uhvacenmuzika;

        public GameObject srce;
        #region Pathfinding
        public bool pronadjen = false;
        public GameObject pathFinderGameObject;
        public bool hit = false;
        public GameObject svetiljkoDeath;
        public GameObject enemyDeathAnim;
    
        #endregion
        void Start()
        {
            
            pathFinderGameObject.GetComponent<AIPath>().autoRepath.mode = Pathfinding.AutoRepathPolicy.Mode.Dynamic;

            
        }
        public void resetAllBools()
        {
            foreach (var t in anim.parameters)
            {

                anim.SetBool(t.name, false);

            }
        }


        private void FixedUpdate()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            direction = (player.transform.position - enemy.transform.position).normalized;
           
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
             
            }
        }
        public bool uPotjeri = false;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject == player)
            {
               
                if (distance < 10)
                {
                    if (pathFinderGameObject.GetComponent<Patrol>() != null)
                        pathFinderGameObject.GetComponent<Patrol>().enabled = false;
                    uPotjeri = true;
                    pathFinderGameObject.GetComponent<AIDestinationSetter>().enabled = true;
                    hit = false;
                    pathFinderGameObject.GetComponent<AIPath>().autoRepath.mode = Pathfinding.AutoRepathPolicy.Mode.Dynamic;
                    pathFinderGameObject.GetComponent<AIDestinationSetter>().target = mesto;
                    pathFinderGameObject.GetComponent<AIPath>().maxSpeed = 5f;
                    resetAllBools();
                    anim.SetBool("isWalking", true);
                    if (direction.x >= 0 && direction.y > -0.7 && direction.y < 0.7)
                    {
                        print("Desno");
                        oci.transform.rotation = Quaternion.Euler(0, 0, 90);
                        oci.transform.localPosition = new Vector3(0.292f, 0.201f, 0);
                        resetAllBools();
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isWalking(R)", true);
                        moveX = 1;
                        moveY = 0;



                    }
                    else if (direction.x < 0 && direction.y > -0.7 && direction.y < 0.7)
                    {
                        print("Levo");
                        oci.transform.rotation = Quaternion.Euler(0, 0, -90);
                        oci.transform.localPosition = new Vector3(-0.236f, 0.201f, 0);
                        resetAllBools();
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isWalking(L)", true);
                        moveX = -1;
                        moveY = 0;

                    }
                    else if (direction.y >= 0.5)
                    {
                        print("Gore");
                        oci.transform.rotation = Quaternion.Euler(0, 0, 180);
                        oci.transform.localPosition = new Vector3(-0.009f, 0.477f, 0);
                        resetAllBools();
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isWalking(U)", true);
                        moveY = 1;
                        moveX = 0;

                    }
                    else if (direction.y < -0.5)
                    {
                        print("Dole");
                        oci.transform.rotation = Quaternion.Euler(0, 0, 0);
                        oci.transform.localPosition = new Vector3(0.039f, -0.026f, 0);
                        resetAllBools();
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isWalking(D)", true);
                        moveY = -1;
                        moveX = 0;

                    }
                    print("SETNJA");
                }


                else if (distance > 10)
                {
                    uPotjeri = false;
                    hit = false;



                }
            }

        }


        int flg = 0;
        public float x;
        public float y;
        public GameObject ending;
        public GameObject camera;
        public GameObject cig;
        public bool Po = true;
        public GameObject presrbutton;
        public GameObject switchmanag;
        IEnumerator finis()
        {
            switchmanag.GetComponent<SwitchManager>().isCat = false;
            player.GetComponent<PlayerMovement>().g = false;
            player.GetComponent<PlayerMovement>().speed = 0;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<PlayerMovement>().resetAllBools();
            player.GetComponent<PolygonCollider2D>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            pathFinderGameObject.GetComponent<AIPath>().enabled = false;
            pathFinderGameObject.GetComponent<AIDestinationSetter>().enabled = false;
            resetAllBools();
           // uhvacenmuzika.GetComponent<AudioSource>().Play();
            resetAllBools();
            anim.SetBool("isIdle", true);
            srce.GetComponent<Animator>().SetBool("vremeJe", true);
            yield return new WaitForSeconds(1f);
            enemyDeathAnim.SetActive(true);
            enemy.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(2f);
            svetiljkoDeath.SetActive(true);
            player.GetComponent<SpriteRenderer>().sortingOrder = 0;
            yield return new WaitForSeconds(2f);
            Destroy(srce);

            Destroy(energy);

            float fov = camera.GetComponent<Camera>().orthographicSize;
            camera.GetComponent<Camera>().orthographicSize = fov * 0.9f;

            camera.transform.parent = null;
            /*
                        yield return new WaitForSeconds(1f);


                        camera.transform.localPosition = new Vector3(-12.41f, 40.09f, -10f);
                        ending.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        ending.GetComponent<Animator>().SetBool("vremeJe2", true);
                        yield return new WaitForSeconds(1.15f);
                        if (ending.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Smrt") && ending.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !ending.GetComponent<Animator>().IsInTransition(0))
                        {



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
    
       
        private void Update()
        {
      
         
            if (ukeban == true)
            {
                player.GetComponent<PlayerMovement>().resetAllBools();
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            }
            playerCol = player.GetComponent<PolygonCollider2D>();
            if (flag == 0)
            {
                x = enemy.transform.position.x;
                y = enemy.transform.position.x;
            }
            else
            {
                if (enemy.transform.position.x > x)
                {
                    //desno
                    moveX = 1;
                }
                else if (enemy.transform.position.x < x)
                {
                    moveX = -1;
                    //levo
                }
                if (enemy.transform.position.y > y)
                {
                    moveY = 1;
                    //gore
                }
                else if (enemy.transform.position.y < y)
                {
                    moveY = -1;
                    //dole
                }
            }
          
                distance = Vector2.Distance(rb.position, player.transform.position);
            
                if (distance > 10)
                {
                    pathFinderGameObject.GetComponent<AIPath>().autoRepath.mode = Pathfinding.AutoRepathPolicy.Mode.Never;
                    flag = 1;

               
                 }
            if (pathFinderGameObject.GetComponent<AIPath>().reachedEndOfPath == true && distance < 1f && Po == true && player.GetComponent<PlayerMovement>().dashing == false)
            {
                resetAllBools();
                enemy.GetComponent<Animator>().enabled = false;
                StartCoroutine(finis());
                ukeban = true;
            }
            if (pathFinderGameObject.GetComponent<AIPath>().reachedEndOfPath == true && distance > 10)
            {
                pathFinderGameObject.GetComponent<AIDestinationSetter>().enabled = false;
                if (pathFinderGameObject.GetComponent<Patrol>() == null)
                    pathFinderGameObject.GetComponent<AIPath>().maxSpeed = 0;

                if (moveX == 1)
                {
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(R)", true);
                    oci.transform.rotation = Quaternion.Euler(0, 0, 90);
                    oci.transform.localPosition = new Vector3(0.292f, 0.201f, 0);

                }
                else if (moveX == -1)
                {
                    resetAllBools();

                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(L)", true);
                    oci.transform.rotation = Quaternion.Euler(0, 0, -90);
                    oci.transform.localPosition = new Vector3(-0.236f, 0.201f, 0);

                }
                if (moveY == 1)
                {
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(U)", true);
                    oci.transform.rotation = Quaternion.Euler(0, 0, 180);
                    oci.transform.localPosition = new Vector3(-0.009f, 0.477f, 0);

                }
                else if (moveY == -1)
                {
                    resetAllBools();
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isIdle(D)", true);
                    oci.transform.rotation = Quaternion.Euler(0, 0, 0);
                    oci.transform.localPosition = new Vector3(0.039f, -0.026f, 0);

                }
            }
        }





    }



}
