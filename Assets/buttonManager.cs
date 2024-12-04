using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    public GameObject lManager;
    public bool inRange = false;
    public GameObject svetlo;
    public GameObject fragment;
    bool flg = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(inRange == true && lManager.GetComponent<LootManager>().skupljeniFragmenti == true && Input.GetKey(KeyCode.E) && flg == false)
        {
            this.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            this.transform.parent.gameObject.GetComponent<AudioSource>().Play();

            this.transform.parent.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            fragment.GetComponent<Animator>().enabled = true;
            lManager.GetComponent<LootManager>().buttonCount += 1;
            Destroy(svetlo);
            flg = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
