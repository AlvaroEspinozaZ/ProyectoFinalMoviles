using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenesManager;

public class LocalScene : MonoBehaviour
{
    [SerializeField] private SceneConfiguration LocalConfiguration;

    private void OnEnable()
    {
        if (LocalConfiguration.IsInactive)
        {
            LocalConfiguration.DisableGameObjects(this.gameObject);
        }
        else
        {
            LocalConfiguration.EnableGameObjects(this.gameObject);
        }

        
    }

    private void OnDisable()
    {
        
    }

    [ContextMenu("Enable GameObjects")]
    public void EnableGameObjects()
    {
        LocalConfiguration.EnableGameObjects(this.gameObject);
    }

    [ContextMenu("Disable GameObjects")]
    public void DisableGameObjects()
    {
        LocalConfiguration.DisableGameObjects(this.gameObject);
    }
}
