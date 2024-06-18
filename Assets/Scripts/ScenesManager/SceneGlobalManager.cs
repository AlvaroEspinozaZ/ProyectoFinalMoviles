using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ScenesManager
{
    public class SceneGlobalManager : MonoBehaviour
    {
        public static SceneGlobalManager Instance { get; private set; }

        private List<SceneConfiguration> _loadedScenes;
        private List<AsyncOperation> _loadingProcesses;

        public static Action<float> OnProgress;
        public static Action OnStartProgress;
        public static Action OnFinishProgress;

        private IEnumerator _AsyncProcesses;

        private CancellationTokenSource tokenSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);

            Instance = this;
            _AsyncProcesses = AsyncProcess();
            _loadedScenes = new();
            _loadingProcesses = new();
        }

        private void Start()
        {
            tokenSource = new CancellationTokenSource();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            tokenSource.Cancel();
        }

        public void LoadSingle(SceneConfiguration SceneToLoad)
        {
            _loadedScenes.Add(SceneToLoad);
            AsyncProncessLoad(SceneToLoad);
        }
        public void ChangeScene(SceneConfiguration SceneToLoad)
        {
            SceneManager.LoadScene(SceneToLoad.SceneName);
        }
        public void LoadScene(SceneConfiguration SceneToLoad)
        {
            _loadedScenes.Add(SceneToLoad);
            _loadingProcesses.Add(SceneToLoad.LoadScene());

            StartCoroutine(_AsyncProcesses);
        }

        public void LoadScene(SceneConfiguration SceneToLoad, SceneConfiguration CurrentScene = null)
        {
            _loadedScenes.Add(SceneToLoad);
            Debug.Log(SceneToLoad.LoadScene().progress);
            _loadingProcesses.Add(SceneToLoad.LoadScene());

            if (CurrentScene != null)
            {
                _loadedScenes.Remove(CurrentScene);
                _loadingProcesses.Add(CurrentScene.UnloadScene());
            }

            StartCoroutine(_AsyncProcesses);
        }

        public void UnloadScene(SceneConfiguration SceneToUnload)
        {
            _loadedScenes.Remove(SceneToUnload);
            _loadingProcesses.Add(SceneToUnload.UnloadScene());

            StartCoroutine(_AsyncProcesses);
        }

        private IEnumerator AsyncProcess()
        {
            OnStartProgress?.Invoke();

            yield return new WaitForSeconds(0.5f);

            float totalProgress = 0;
            for (int i = 0; i < _loadingProcesses.Count; i++)
            {
                while (!_loadingProcesses[i].isDone)
                {
                    totalProgress += _loadingProcesses[i].progress;

                    OnProgress?.Invoke(totalProgress / _loadingProcesses.Count);

                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);

            OnFinishProgress?.Invoke();

            _loadingProcesses = new();

            _AsyncProcesses = AsyncProcess();
        }

        private async void AsyncProncessLoad(SceneConfiguration SceneToLoad)
        {
            OnStartProgress?.Invoke();

            AsyncOperation loadingOperation = SceneToLoad.LoadScene();

            loadingOperation.allowSceneActivation = false;

            do
            {
                await Task.Delay(100);
                OnProgress?.Invoke(loadingOperation.progress);
            } while (loadingOperation.progress < 0.9f);

            OnFinishProgress?.Invoke();

            loadingOperation.allowSceneActivation = true;
        }


        private async void GetRandomNumberAsync()
        {
            var number =  await WaitForRandom();

            if (tokenSource.IsCancellationRequested)
            {
                Debug.Log("Task stoped");
            }

            Debug.Log(number);
        }

        private async Task<float> WaitForRandom()
        {
            Debug.Log("Hacking your Mind . . .");

            float _randomNumber = UnityEngine.Random.Range(-5f, 5f);

            await Task.Delay(TimeSpan.FromSeconds(1), tokenSource.Token);

            if (tokenSource.IsCancellationRequested)
            {
                return 0;
            }

            return _randomNumber;
        }

        private async void GetRandomNumberTask()
        {
            var number = await WaitForRandom2();
            if (tokenSource.IsCancellationRequested)
            {
                Debug.Log("Task stoped");
            }
            Debug.Log(number);
        }
        private Task<float> WaitForRandom2()
        {
            Debug.Log("Hacking your Mind . . .");
            return Task.Run(() =>
            {
                if (tokenSource.IsCancellationRequested)
                {
                    return 0;
                }
                return FunctionThatReturnsRandomNumber();
            }, tokenSource.Token);
        }
        private float FunctionThatReturnsRandomNumber()
        {
            return UnityEngine.Random.Range(-5f, 5f);
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}