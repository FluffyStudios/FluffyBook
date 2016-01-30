using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public float FadeDuration = 60f;

    private UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration Vignette
    {
        get
        {
            return this.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
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
}
