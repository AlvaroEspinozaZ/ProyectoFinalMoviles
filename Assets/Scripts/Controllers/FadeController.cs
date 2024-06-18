using System.Collections;
using UnityEngine;
using ScenesManager;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image FadePicture;
    [SerializeField] Color FadeInColor;
    [SerializeField] Color FadeOutColor;
    [SerializeField] float FadeTime = 0.5f;

    private Color _currentColor;

    private void Awake()
    {
        _currentColor = FadePicture.color;
    }

    private void OnEnable()
    {
        SceneGlobalManager.OnStartProgress += StartFadeIn;
        SceneGlobalManager.OnFinishProgress += StartFadeOut;
    }

    private void OnDisable()
    {
        SceneGlobalManager.OnStartProgress -= StartFadeIn;
        SceneGlobalManager.OnFinishProgress -= StartFadeOut;
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeRoutine(FadeTime, FadeOutColor, FadeInColor));
    }

    private void StartFadeIn()
    {
        
        StartCoroutine(FadeRoutine(FadeTime, FadeInColor, FadeOutColor));
    }

    IEnumerator FadeRoutine(float time, Color currentColor, Color targetColor)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            this._currentColor = Color.Lerp(currentColor, targetColor, i);

            FadePicture.color = _currentColor;
            yield return null;
        }

        FadePicture.color = targetColor;
    }
}
