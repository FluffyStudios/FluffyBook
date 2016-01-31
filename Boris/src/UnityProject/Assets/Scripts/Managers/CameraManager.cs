using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public float FadeDuration = 60f;
    public float FlashDuration = 60f;
    public float MaxFlashIntensity = 3f;

    private UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration Vignette
    {
        get
        {
            return this.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
        }
    }

    private UnityStandardAssets.ImageEffects.Bloom Bloom
    {
        get
        {
            return this.GetComponent<UnityStandardAssets.ImageEffects.Bloom>();
        }
    }

    public IEnumerator FadeIn()
    {
        float counter = 0;
        while (counter < this.FadeDuration)
        {
            counter++;
            float progression = 1 - (counter / this.FadeDuration);
            this.Vignette.intensity = progression;

            yield return null;
        }

        this.Vignette.intensity = 0;
    }

    public IEnumerator FadeOut()
    {
        float counter = 0;
        while (counter < this.FadeDuration)
        {
            counter++;
            float progression = counter / this.FadeDuration;
            this.Vignette.intensity = progression;

            yield return null;
        }

        this.Vignette.intensity = 1;
    }

    public IEnumerator FlashPhoto()
    {
        float counter = 0;
        while (counter < this.FlashDuration)
        {
            counter++;
            float progression = counter / this.FlashDuration;
            this.Bloom.bloomIntensity = progression * this.MaxFlashIntensity;

            yield return null;
        }

        this.Bloom.bloomIntensity = this.MaxFlashIntensity;

        counter = 0;
        while (counter < this.FlashDuration / 2)
        {
            counter++;
            float progression = 1 - (counter / this.FlashDuration);
            this.Bloom.bloomIntensity = progression * this.MaxFlashIntensity;

            yield return null;
        }

        this.Bloom.bloomIntensity = 0;
    }
}
