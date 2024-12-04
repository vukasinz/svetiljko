using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LootManager : MonoBehaviour
{
    public int lootCount = 0;
    public int buttonCount = 0;
    public TextMeshProUGUI lootText;
    public bool skupljeniFragmenti = false;
    public GameObject barijera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lootCount == 4)
        {
            skupljeniFragmenti = true;

        }
        
        lootText.text = lootCount.ToString() + " / 4";
     
    }
}
