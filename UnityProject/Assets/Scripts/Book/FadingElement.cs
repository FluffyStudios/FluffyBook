using UnityEngine;
using System.Collections;

public class FadingElement : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private float FadeDuration = 20f;
    private bool fadingIn = false;
    private bool fadingOut = false;

    public void Show(bool instant = false)
    {
        if (instant)
        {
            Color newColor = this.SpriteRenderer.color;
            newColor.a = 1;
            this.SpriteRenderer.color = newColor;
            this.StopAllCoroutines();
            this.fadingIn = false;
            this.fadingOut = false;
        }
        else
        {
            if (this.SpriteRenderer.color.a != 1 && !this.fadingIn)
            {
                this.StartCoroutine(this.FadeIn());
            }
        }
    }

    public void Hide(bool instant = false)
    {
        if (instant)
        {
            Color newColor = this.SpriteRenderer.color;
            newColor.a = 0;
            this.SpriteRenderer.color = newColor;
            this.StopAllCoroutines();
            this.fadingIn = false;
            this.fadingOut = false;
        }
        else
        {
            if (this.SpriteRenderer.color.a != 0 && !this.fadingOut)
            {
                this.StartCoroutine(this.FadeOut());
            }
        }
    }

    public IEnumerator FadeIn()
    {
        float counter = 0;
        Color newColor = this.SpriteRenderer.color;
        this.fadingIn = true;

        while (counter < this.FadeDuration)
        {
            counter++;
            float progression = counter / this.FadeDuration;
            
            newColor.a = progression;
            this.SpriteRenderer.color = newColor;

            yield return null;
        }
        
        newColor.a = 1;
        this.SpriteRenderer.color = newColor;
        this.fadingIn = false;
    }

    public IEnumerator FadeOut()
    {
        float counter = 0;
        Color newColor = this.SpriteRenderer.color;
        this.fadingOut = true;

        while (counter < this.FadeDuration)
        {
            counter++;
            float progression = 1 - (counter / this.FadeDuration);

            newColor.a = progression;
            this.SpriteRenderer.color = newColor;

            yield return null;
        }

        newColor.a = 0;
        this.SpriteRenderer.color = newColor;
        this.fadingOut = false;
    }
}
