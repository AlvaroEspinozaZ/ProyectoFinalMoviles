using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScenesManager
{
    [CreateAssetMenu(fileName = "SceneConfiguration", menuName = "Scriptable Objects/Scene Management/Scene Configuration")]
    public class SceneConfiguration : ScriptableObject
    {
        [SerializeField] private string sceneName;
        [SerializeField] private bool isAditive;
        [SerializeField] private bool isInactive;
        public string SceneName => sceneName;
        public bool IsAditive => isAditive;
        public bool IsInactive => isInactive;

        public AsyncOperation LoadScene()
        {
            return SceneManager.LoadSceneAsync(sceneName, isAditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        public AsyncOperation UnloadScene()
        {
            return SceneManager.UnloadSceneAsync(sceneName);
        }

        public void DisableGameObjects(GameObject controller)
        {
            GameObject[] gameObjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();

            foreach (GameObject item in gameObjects)
            {
                if (item == controller)
                    continue;

                item.SetActive(false);
            }
        }

        public void EnableGameObjects(GameObject controller)
        {
            GameObject[] gameObjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();

            foreach (GameObject item in gameObjects)
            {
                if (item == controller)
                    continue;

                item.SetActive(true);
            }
        }
    }
}