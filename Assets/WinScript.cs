using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Text winText;
    public GameObject pausePanel;
    GameObject[] enemies;
    public GameObject switchGo;
    public GameObject presR;
    public Text conDugme;
    public bool won = false;
    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyTall");
      
    }
    public IEnumerator FadeTextToFullAlpha(float time, Text text,Text con)
    {
        LeanTween.alpha(pausePanel.GetComponent<Image>().rectTransform, 1f, 1f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // Set initial alpha to 0
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
            yield return null;
        }
        con.color = new Color(con.color.r, con.color.g, con.color.b, 0); // Set initial alpha to 0
        while (con.color.a < 0.875f)
        {
            con.color = new Color(con.color.r, con.color.g, con.color.b, con.color.a + (Time.deltaTime / time));
            yield return null;
        }
        presR.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
           // switchGo.GetComponent<SwitchManager>(). = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(FadeTextToFullAlpha(1f,winText,conDugme));
            foreach(var e in enemies)
            {
                Destroy(e);
            }
            won = true;
        }
    }
}
