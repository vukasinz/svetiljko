using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class teleport : MonoBehaviour
{
    private GameObject[] list;
    public GameObject[] fullList;
    public GameObject player;
    public GameObject kitty;
    Animator anim;
    void Start()
    {
        int i = 0;
        anim = this.GetComponent<Animator>();

        list = GameObject.FindGameObjectsWithTag("teleport");
        fullList = new GameObject[list.Length - 1];
        foreach (GameObject x in list)
        {
            if(x != this.gameObject)
            {
                fullList[i++] = x;
            }
        }
   
    }
    public int b = 0;
    // Update is called once per frame
    void Update()
    {
        if (kitty.activeInHierarchy)
        {
            player = kitty;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (inTeleport == true && available == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(availab());
                StartCoroutine(tp());
               

            }
        }

        if (b > 2)
        {
            b = 0;
        }
    }
    IEnumerator availab()
    {
        available = false;
        yield return new WaitForSeconds(2f);
        available = true;
    }
    public bool inTeleport = false;
    public bool available = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inTeleport = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inTeleport = false;
        }

    }
    IEnumerator tp()
    {

      
        anim.SetBool("isWarping", true);
        anim.SetBool("isIdle", false);
        fullList[b].GetComponent<Animator>().SetBool("isIdle", false);
        fullList[b].GetComponent<Animator>().SetBool("isWarping", true);
        yield return new WaitForSeconds(0.5f);

        player.transform.position = fullList[b].transform.position;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isWarping", false);
        anim.SetBool("isIdle", true);
        fullList[b].GetComponent<Animator>().SetBool("isIdle", true);
        fullList[b].GetComponent<Animator>().SetBool("isWarping", false);
       
      

        b = (b + 1) % fullList.Length;
    }
}
