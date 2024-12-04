using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    bool inChase = false;
    GameObject[] go;
    
    public GameObject mirnaMuzika;
    public GameObject chaseMuzika;
    public Transform mesto;
   
    public GameObject svetiljkoDeath;
   // Start is called before the first frame update
   void Start()
    {
         go = GameObject.FindGameObjectsWithTag("EnemyTall");
        go = go.Concat(GameObject.FindGameObjectsWithTag("NotPlayer")).ToArray();
        mirnaMuzika.GetComponent<AudioSource>().Play();
        chaseMuzika.GetComponent<AudioSource>().mute = true;
        chaseMuzika.GetComponent<AudioSource>().Play();

    }
    // Update is called once per frame
    void Update()
    {
     
       
            int brojac = 0;
        foreach (GameObject x in go)
        {
            if (x.GetComponentInChildren<Enemy>() != null)
            {
                if (x.GetComponent<AIDestinationSetter>().enabled == true && x.GetComponentInChildren<Enemy>().uPotjeri == true && brojac == 0)
                {
                    mirnaMuzika.GetComponent<AudioSource>().mute = true;
                    chaseMuzika.GetComponent<AudioSource>().mute = false;

                    brojac = 1;
                    break;
                }
            }
            if(x.GetComponentInChildren<BudilnikController>() != null)
            {
                if (x.GetComponent<AIDestinationSetter>().enabled == true && x.GetComponentInChildren<BudilnikController>().jurnjava == true && brojac == 0)
                {
                    mirnaMuzika.GetComponent<AudioSource>().mute = true;
                    chaseMuzika.GetComponent<AudioSource>().mute = false;

                    brojac = 1;
                    break;
                }
            }

            }
            if (brojac == 0)
            {

                chaseMuzika.GetComponent<AudioSource>().mute = true;
                mirnaMuzika.GetComponent<AudioSource>().mute = false;
            }
        
       
     
        
    }
}
