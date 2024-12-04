using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trenutna;
    public Animator anim;
    public bool Kliknuto = false;
    public GameObject[] enemies;
    GameObject[] svetla;
    public bool zvonio = false;
    void Start()
    {
        svetla = GameObject.FindGameObjectsWithTag("zvonoSvetla");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("bell_anim") && Kliknuto == true)
        {

            anim.SetBool("Wither", true);
            foreach (GameObject s in svetla)
                s.SetActive(false);
        }
    }
    public void Zvonjava()
    {
        if (Kliknuto == false)
        {
            Kliknuto = true;
            anim.SetBool("isRinging", true);
             enemies = GameObject.FindGameObjectsWithTag("EnemyTall");
            foreach (GameObject enem in enemies)
            {
                if (enem.GetComponent<PatrolManager>() == null)
                {
                    enem.GetComponent<AIPath>().autoRepath.mode = Pathfinding.AutoRepathPolicy.Mode.Dynamic;

                    enem.GetComponent<AIDestinationSetter>().enabled = true;
                    enem.GetComponent<AIDestinationSetter>().target = trenutna;
                    enem.GetComponent<AIPath>().autoRepath.mode = Pathfinding.AutoRepathPolicy.Mode.Dynamic;
                    
                }
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.E))
        {
            Zvonjava();
            if (zvonio == false)
            {
                this.GetComponent<AudioSource>().Play();
                zvonio = true;
            }
        }
       
    }
}
