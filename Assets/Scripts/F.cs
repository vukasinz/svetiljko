using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class F : MonoBehaviour
{
    public int brojPuta;

    public float cekanje = 0;
    private void Start()
    {

        InvokeRepeating("StartFlicker", 1, Random.Range(3f, 6f));
    }
    private void StartFlicker()
    {
        StartCoroutine(Flicker());
    }
    IEnumerator Flicker()
    {

        brojPuta = Random.Range(1, 4);
        for (int i = 0; i < brojPuta; i++)
        {
            cekanje = Random.Range(0.1f, 0.3f);
            this.GetComponent<Light2D>().intensity = 0;
            yield return new WaitForSeconds(cekanje);
            this.GetComponent<Light2D>().intensity = Random.Range(1.7f, 2.21f);
            yield return new WaitForSeconds(cekanje);
        }

    }

    void Update()
    {

    }
}
