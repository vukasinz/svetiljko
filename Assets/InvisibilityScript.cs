using System.Collections;
using UnityEngine;

public class InvisibilityScript : MonoBehaviour
{
    public bool seen = false;
    public bool isInvisible = false;
    private Color startColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    IEnumerator EndInvisibility()
    {
        this.gameObject.tag = "Player";
        isInvisible = false;
        float elapsedTime = 0f;
        float duration = 1f;
        Color currentColor = spriteRenderer.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentColor.a, startColor.a, elapsedTime / duration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Invisibility()
    {
        this.gameObject.tag = "NotPlayer";
        isInvisible = true;
        float elapsedTime = 0f;
        float duration = 1f;
        Color currentColor = spriteRenderer.color;
      

         while (elapsedTime < duration)
         {
             elapsedTime += Time.deltaTime;
             float newAlpha = Mathf.Lerp(currentColor.a, 0.125f, elapsedTime / duration); // Set alpha to 0 for full invisibility
             spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Q") && GetComponent<Rigidbody2D>().velocity == Vector2.zero && !isInvisible)
        {
            Debug.Log("pocetak prve");
            StartCoroutine(Invisibility());
        }
        else if ((Input.GetButtonDown("Q") && isInvisible) || (GetComponent<Rigidbody2D>().velocity != Vector2.zero && isInvisible))
        {
            Debug.Log("pocetak druge");
            StartCoroutine(EndInvisibility());
        }
    }
}