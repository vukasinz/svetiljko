using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmokeSlower : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Zastajkivanje(GameObject objekat)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1)
        {
            Debug.Log("U whileu pre - Zastajkivanje");
            float t = elapsedTime / 1;
            objekat.GetComponent<AIPath>().maxSpeed = Mathf.Lerp(5f, 1.5f, t);
            elapsedTime += Time.deltaTime;
            Debug.Log("U whileu prvom - Zastajkivanje");
            yield return null;
        }
        objekat.GetComponent<AIPath>().maxSpeed = 1.5f;
        Debug.Log("Finished Zastajkivanje");
    }

    IEnumerator Pokretanje(GameObject objekat)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1)
        {
            Debug.Log("U whileu pre - Pokretanje");
            float t = elapsedTime / 1;
            objekat.GetComponent<AIPath>().maxSpeed = Mathf.Lerp(1.5f, 5f, t);
            elapsedTime += Time.deltaTime;
            Debug.Log("U whileu prvom - Pokretanje");
            yield return null;
        }
        objekat.GetComponent<AIPath>().maxSpeed = 5f;
        Debug.Log("Finished Pokretanje");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Halo");
        Transform parentTransform = collision.transform.parent;
        if (parentTransform != null)
        {
            GameObject parentObject = parentTransform.gameObject;
            if (parentObject.CompareTag("EnemyTall"))
            {
                Debug.Log("EnemyTall parent detected in OnTriggerEnter2D");
                StartCoroutine(Zastajkivanje(parentObject));
            }
        }
        else
        {
            Debug.Log("No parent found for the collided object.");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("HaloExit");
        Transform parentTransform = collision.transform.parent;
        if (parentTransform != null)
        {
            GameObject parentObject = parentTransform.gameObject;
            if (parentObject.CompareTag("EnemyTall"))
            {
                Debug.Log("EnemyTall parent detected in OnTriggerExit2D");
                StartCoroutine(Pokretanje(parentObject));
            }
        }
        else
        {
            Debug.Log("No parent found for the collided object.");
        }
    }
}

