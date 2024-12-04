using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubica : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] mesta;
    void Start()
    {
        this.GetComponent<AIDestinationSetter>().target = mesta[Random.Range(0,mesta.Length)];
    }

    public bool stigao;
    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<AIPath>().reachedEndOfPath == true)
            stigao = true;
        if(Random.Range(1,5000) == 53)
        {
            StartCoroutine(Zastajkivanje());
        }
        if(stigao == true)
        {
            int r = Random.Range(0, mesta.Length);
            stigao = false;
            this.GetComponent<AIDestinationSetter>().target = mesta[r];
            StartCoroutine(Zastajkivanje());
        }
    }
    IEnumerator Zastajkivanje()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            // Calculate the interpolation factor (0 to 1)
            float t = elapsedTime / 1;

            // Smoothly interpolate between the current and target values
            this.GetComponent<AIPath>().maxSpeed = Mathf.Lerp(5f, 0f, t);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        this.GetComponent<AIPath>().maxSpeed = 0f;
        yield return new WaitForSeconds(Random.Range(0,2f));

        while (elapsedTime < 1)
        {
            // Calculate the interpolation factor (0 to 1)
            float t = elapsedTime / 1;

            // Smoothly interpolate between the current and target values
            this.GetComponent<AIPath>().maxSpeed = Mathf.Lerp(0f, 5f, t);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        this.GetComponent<AIPath>().maxSpeed = 5f;
    }
}
