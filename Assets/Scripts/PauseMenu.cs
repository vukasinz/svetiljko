using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseAnimObject;
    public Animator anim;
    public bool isPaused = false;
    public GameObject resumeButton;
    public GameObject exitButton;
    public GameObject srce;
    public GameObject menuButton;
    public GameObject winGb;

    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        Color color = pausePanel.GetComponent<Image>().color;
        color.a = 0;
        pausePanel.GetComponent<Image>().color = color;
    }
    IEnumerator paused()
    {

        
        Color color = pausePanel.GetComponent<Image>().color;
        color.a = 0;
        pausePanel.GetComponent<Image>().color = color;

        // Use LeanTween to fade in
        LeanTween.alpha(pausePanel.GetComponent<Image>().rectTransform, 0.5f, 1f);
        exitButton.SetActive(true);
        menuButton.SetActive(true);
        resumeButton.SetActive(true);
        LeanTween.alpha(menuButton.GetComponent<Image>().rectTransform, 1f, 0.7f);
        LeanTween.alpha(resumeButton.GetComponent<Image>().rectTransform, 1f, 0.7f);
        LeanTween.alpha(exitButton.GetComponent<Image>().rectTransform, 1f, 0.7f);
        yield return new WaitForSeconds(0.5f);
        
        pauseAnimObject.GetComponent<SpriteRenderer>().enabled = true;

        this.GetComponent<Light2D>().enabled = true;
        this.GetComponent<Light2D>().lightCookieSprite = pauseAnimObject.GetComponent<SpriteRenderer>().sprite;
        anim.SetBool("isBreaking", false);
        anim.SetBool("isSwinging", true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        flag = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Menu()
    {

        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator unpaused()
    {

        Time.timeScale = 1;
        this.GetComponent<Light2D>().lightCookieSprite = pauseAnimObject.GetComponent<SpriteRenderer>().sprite;

        anim.SetBool("isSwinging", false);
        anim.SetBool("isBreaking", true);
        
        LeanTween.alpha(resumeButton.GetComponent<Image>().rectTransform, 0f, 0.5f);
        LeanTween.alpha(exitButton.GetComponent<Image>().rectTransform, 0f, 0.5f);
        LeanTween.alpha(menuButton.GetComponent<Image>().rectTransform, 0f, 0.5f);


        
        this.gameObject.GetComponent<Light2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        menuButton.SetActive(false);
        LeanTween.alpha(pausePanel.GetComponent<Image>().rectTransform, 0f, 1f);
        
        pauseAnimObject.GetComponent<SpriteRenderer>().enabled = false;
        
        isPaused = false;
    }
    void inPose()
    {
        if (isPaused == true && flag == true)
        {
            flag = false;
            StartCoroutine(unpaused());
            
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused && srce.activeInHierarchy && winGb.GetComponent<WinScript>().won == false)
            {
                isPaused = true;
                StartCoroutine(paused());
            }
            else
            {
                flag = true;
            }
        }


    }
}
