using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class PatrolManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cekanje());
    }
   
    public Transform mesto;
    public Vector2 direction;
    IEnumerator cekanje()
    {
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<AIPath>().autoRepath.mode = AutoRepathPolicy.Mode.Dynamic;

    }
    private void FixedUpdate()
    {
        direction = (this.GetComponent<Patrol>().targets[this.GetComponent<Patrol>().index].transform.position - this.transform.position).normalized;
        
    }
    public void resetAllBools()
    {
        foreach (var t in child.GetComponent<Enemy>().anim.parameters)
        {
                child.GetComponent<Enemy>().anim.SetBool(t.name, false);
        }
    }
    public GameObject child;
    bool flag = false;
    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<AIPath>().reachedEndOfPath)
        {
            flag = true;
            child.GetComponent<Enemy>().anim.SetBool("isIdle", true);
            child.GetComponent<Enemy>().anim.SetBool("isWalking", false);
        }
        else
            flag = false;
        
        if (this.GetComponent<Patrol>() != null && this.GetComponent<Patrol>().enabled == true && flag == false)
        {
            this.GetComponent<AIPath>().speed = 5f;
            child.GetComponent<Enemy>().anim.SetBool("isWalking", true);
            this.GetComponent<AIPath>().autoRepath.mode = AutoRepathPolicy.Mode.Dynamic;
            if (direction.x >= 0 && direction.y > -0.7 && direction.y < 0.7)
            {
                print("Desno");
                child.GetComponent<Enemy>().oci.transform.rotation = Quaternion.Euler(0, 0, 90);
                child.GetComponent<Enemy>().oci.transform.localPosition = new Vector3(0.292f, 0.201f, 0);
                child.GetComponent<Enemy>().resetAllBools();
                child.GetComponent<Enemy>().anim.SetBool("isWalking", true);
                child.GetComponent<Enemy>().anim.SetBool("isWalking(R)", true);
                child.GetComponent<Enemy>().moveX = 1;
                child.GetComponent<Enemy>().moveY = 0;
            }
            else if (direction.x < 0 && direction.y > -0.7 && direction.y < 0.7)
            {
                print("Levo");
                child.GetComponent<Enemy>().oci.transform.rotation = Quaternion.Euler(0, 0, -90);
                child.GetComponent<Enemy>().oci.transform.localPosition = new Vector3(-0.236f, 0.201f, 0);
                child.GetComponent<Enemy>().resetAllBools();
                child.GetComponent<Enemy>().anim.SetBool("isWalking", true);
                child.GetComponent<Enemy>().anim.SetBool("isWalking(L)", true);
                child.GetComponent<Enemy>().moveX = -1;
                child.GetComponent<Enemy>().moveY = 0;
            }
            else if (direction.y >= 0.5)
            {
                print("Gore");
                child.GetComponent<Enemy>().oci.transform.rotation = Quaternion.Euler(0, 0, 180);
                child.GetComponent<Enemy>().oci.transform.localPosition = new Vector3(-0.009f, 0.477f, 0);
                child.GetComponent<Enemy>().resetAllBools();
                child.GetComponent<Enemy>().anim.SetBool("isWalking", true);
                child.GetComponent<Enemy>().anim.SetBool("isWalking(U)", true);
                child.GetComponent<Enemy>().moveY = 1;
                child.GetComponent<Enemy>().moveX = 0;
            }
            else if (direction.y < -0.5)
            {
                print("Dole");
                child.GetComponent<Enemy>().oci.transform.rotation = Quaternion.Euler(0, 0, 0);
                child.GetComponent<Enemy>().oci.transform.localPosition = new Vector3(0.039f, -0.026f, 0);
                child.GetComponent<Enemy>().resetAllBools();
                child.GetComponent<Enemy>().anim.SetBool("isWalking", true);
                child.GetComponent<Enemy>().anim.SetBool("isWalking(D)", true);
                child.GetComponent<Enemy>().moveY = -1;
                child.GetComponent<Enemy>().moveX = 0;
            }
        }
        if (this.GetComponent<AIDestinationSetter>().enabled == true)
        {
            this.GetComponent<Patrol>().enabled = false;
            this.GetComponent<AIDestinationSetter>().target = mesto;
            child.GetComponent<Enemy>().resetAllBools();
            this.GetComponent<PatrolManager>().enabled = false;
           
            
            
        }
    }
}
