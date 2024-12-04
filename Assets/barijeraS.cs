using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barijeraS : MonoBehaviour
{
    public GameObject lootman;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           if(lootman.GetComponent<LootManager>().buttonCount == 2 && lootman.GetComponent<LootManager>().skupljeniFragmenti == true)
            {
                this.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
                this.transform.parent.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                this.GetComponent<AudioSource>().Play();
            }
        }
    }
}
