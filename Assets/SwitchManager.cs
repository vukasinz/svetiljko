using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Linq;

public class SwitchManager : MonoBehaviour
{
    bool progress = false;
    public bool isCat;
   
    public GameObject cat;
    public GameObject player;
    public int switches = 1;
    public GameObject playerCam;
    GameObject[] go;
    public AudioSource chasemuzika;
    public AudioSource uhvacen_muzika;
    public bool uhvacen = false;
    bool flag = false;
    public bool st = true;
    void Start()
    {
        
        cat.SetActive(false);
        Physics2D.IgnoreCollision(cat.GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
        

    }
    IEnumerator Funkcija()
    {
       
        Color c = panelImage.color;
        c.a = 1f;
        panelImage.color = c;
        float duration = 1.5f;
        float timeElapsed = 0f;
        
        while (timeElapsed < duration)
        {
            Color color = panelImage.color;
            color.a = Mathf.Lerp(1f, 0f, timeElapsed / duration); // Fade out from 1 to 0 alpha
            panelImage.color = color;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Catting()
    {
        progress = true;
        yield return new WaitForSeconds(10f);
        progress = false;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (cat.activeInHierarchy && flag == false)
        {
            Physics2D.IgnoreCollision(cat.GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
            flag = true;
        }
    }
    public GameObject text;
    public Image panelImage;
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.0000001f);
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    IEnumerator FadeInOutAndReload()
    {
        float duration = 1.5f;
        float timeElapsed = 0f;

        // Phase 1: Fade In
        while (timeElapsed < duration)
        {
            Color color = panelImage.color;
            color.a = Mathf.Lerp(0f, 1f, timeElapsed / duration); // Fade in from 0 to 1 alpha
            panelImage.color = color;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is fully set to 1 after fade in
        Color finalColor = panelImage.color;
        finalColor.a = 1f;
        panelImage.color = finalColor;
        StartCoroutine(Reload());
       

       
        
    }

    GameObject[] g;
    public bool jure = false;
    float temporaryEnergy = 0f;
    void Update()
    {
        if(uhvacen_muzika.isPlaying== true)
        {
            uhvacen = true;
        }
        if (chasemuzika.mute == false)
        {
            jure = true;
        }
        else
            jure = false;
     
        if (st == true)
        {
            StartCoroutine(Funkcija());
            st = false;
        }
        if(text.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(FadeInOutAndReload());
               


            }
        }
        if (isCat)
        {
        
            if (Input.GetKeyDown(KeyCode.Q) && switches > 0 && jure == false)
            {
                switches = switches - 1;
                cat.transform.position = player.transform.position;
                Vector2 nova = cat.transform.position;
                nova.y += 1.8f;
                cat.transform.position = nova;
                //
               
                cat.SetActive(true);

                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.GetComponent<PlayerMovement>().speed = 0f;
                player.GetComponent<PlayerMovement>().resetAllBools();
                cat.gameObject.tag = "Player";
                playerCam.SetActive(false);
                this.GetComponent<AudioSource>().Play();
                player.GetComponent<Animator>().SetBool("isIdle", true);

                StartCoroutine(Catting());
               
            }
            else if((Input.GetKeyDown(KeyCode.Q) && switches == 0) || (progress == false && switches == 0))
            {

                cat.SetActive(false);


                cat.gameObject.tag = "NotPlayer";
                playerCam.SetActive(true);
                player.GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }
}
