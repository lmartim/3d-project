using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Core.Singleton;

public class FXManager : Singleton<FXManager>
{
    public PostProcessVolume processVolume;
    public Vignette vignette;
    public float duration = 1f;

    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0f;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.white, Color.red, time / duration);
            vignette.color.Override(c);

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        time = 0f;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.white, time / duration);
            vignette.color.Override(c);

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
}
