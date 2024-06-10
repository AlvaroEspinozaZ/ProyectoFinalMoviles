using UnityEngine;
using ScenesManager;

public class LoadIndicator : MonoBehaviour
{
    [SerializeField] private float _loadingValue;
    [SerializeField] private string _message;

    private void OnEnable()
    {
        SceneGlobalManager.OnProgress += UpdateValue;

        SceneGlobalManager.OnStartProgress += StartLoading;
        SceneGlobalManager.OnFinishProgress += FinishLoading;
    }

    private void OnDisable()
    {
        SceneGlobalManager.OnProgress -= UpdateValue;

        SceneGlobalManager.OnStartProgress -= StartLoading;
        SceneGlobalManager.OnFinishProgress -= FinishLoading;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), string.Format("Loading Value: {0:0.00}", _loadingValue));

        GUI.Label(new Rect(150, 10, 100, 100), string.Format("Status: {0}", _message));
    }

    private void UpdateValue(float value)
    {
        _loadingValue = value;
    }

    private void StartLoading()
    {
        _message = "Start Loading";
    }

    private void FinishLoading()
    {
        _message = "Finish Loading";
    }
}
