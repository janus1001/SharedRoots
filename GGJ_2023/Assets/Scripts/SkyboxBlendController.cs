using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxBlendController : MonoBehaviour
{
    public static SkyboxBlendController Instance { get; private set; }

    private float targetBlend;
    private float currentBlend;

    public float blendSpeed = 0.1f;
    public float setBlendTo = -1;

    private void Awake()
    {
        Instance = this;

        //targetBlend = currentBlend = RenderSettings.skybox.GetFloat("_CubemapTransition");

        if(setBlendTo != -1)
        {
            //SetBlendColor(setBlendTo);
        }
    }

    private void SetBlendColor(float blend)
    {
        targetBlend = blend;
        StartCoroutine(TransitionSkybox());
    }

    private IEnumerator TransitionSkybox()
    {
        while (Mathf.Abs(currentBlend - targetBlend) > Mathf.Epsilon)
        {
            currentBlend = Mathf.MoveTowards(currentBlend, targetBlend, blendSpeed * Time.deltaTime);
            RenderSettings.skybox.SetFloat("_CubemapTransition", currentBlend);
            yield return null;
        }
    }
}
