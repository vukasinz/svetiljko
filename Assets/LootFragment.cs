using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LootFragment : MonoBehaviour
{
    public Animator anim;
    public GameObject[] list;
    public int brojac = 0;
    public GameObject lootMan;
    int flag = 0;
    bool inProgress = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && inProgress == false)
        {
           
                StartCoroutine(loot());
            
        }
    }
   
    IEnumerator loot()
    {
        inProgress = true;
        anim.SetBool("razbijeno", true);
        this.GetComponent<AudioSource>().Play();
       
        int r = Random.Range(0,list.Count());
        list[r].GetComponent<Animator>().enabled = true;
        list[r].gameObject.tag = "g";
        lootMan.GetComponent<LootManager>().lootCount++;
        flag = 1;
        yield break;






    }
    void Start()
    {
        list = GameObject.FindGameObjectsWithTag("fragment");

    }
  
    // Update is called once per frame
    void Update()
    {
        list = GameObject.FindGameObjectsWithTag("fragment");

      
        if (flag == 1)
        {
            this.GetComponent<LootFragment>().enabled = false;
        }
    }
}
