using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private const float FADE_TIME = 3f;
    private const float FADE_SPEED = 1f / FADE_TIME;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        Color currentColor = this.spriteRenderer.color;
        while (currentColor.a > 0f)
        {
            currentColor.a -= FADE_SPEED * Time.deltaTime;
            this.spriteRenderer.color = currentColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}
