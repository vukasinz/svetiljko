using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void onExitClick()
    {
        go.GetComponent<AudioSource>().Play();

        Application.Quit();
    }
    public void onPlayClick()
    {
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("SampleScene");
    }
}
