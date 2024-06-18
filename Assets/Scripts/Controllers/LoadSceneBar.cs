using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScenesManager;

public class LoadSceneBar : MonoBehaviour
{
    [SerializeField] private GameObject Holder;
    [SerializeField] private Slider barSlider;
    [SerializeField] private SceneConfiguration MainScene;
    [SerializeField] private SceneConfiguration CurrrentScene;
    [SerializeField] private SceneGlobalManager _SceneGlobalManager;

    private void Start()
    {
        _SceneGlobalManager.LoadSingle(MainScene);
        /*if (_SceneGlobalManager == null)
            _SceneGlobalManager = SceneGlobalManager.Instance;

        _SceneGlobalManager.LoadScene(MainScene, CurrrentScene);*/
    }

    private void OnEnable()
    {
        SceneGlobalManager.OnProgress += UpdateSlider;

        SceneGlobalManager.OnStartProgress += SetUpLoader;
        SceneGlobalManager.OnFinishProgress += UnsetUpLoader;
    }

    private void OnDisable()
    {
        SceneGlobalManager.OnProgress -= UpdateSlider;

        SceneGlobalManager.OnStartProgress -= SetUpLoader;
        SceneGlobalManager.OnFinishProgress -= UnsetUpLoader;
    }

    private void SetUpLoader()
    {
        Holder.SetActive(true);
    }

    private void UnsetUpLoader()
    {
        barSlider.value = 1.0f;

        Holder.SetActive(false);
    }

    private void UpdateSlider(float value)
    {
        barSlider.value = Mathf.Clamp01(value);
    }
}
