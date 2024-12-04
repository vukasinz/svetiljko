using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpriteChanger : MonoBehaviour
{
    // Start is called before the first frame update
     Light2D spriteLight;
     SpriteRenderer renderer;
    void Start()
    {
        spriteLight = this.GetComponent<Light2D>();
        renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteLight.lightCookieSprite = renderer.sprite;
    }
}
