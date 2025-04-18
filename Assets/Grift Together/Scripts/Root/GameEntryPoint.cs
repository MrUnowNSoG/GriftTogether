using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class GameEntryPoint {


        private static GameEntryPoint _instance;
        
        private Coroutines _coroutines;
        private UiRootView _uiRootView;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartEntryGame() {
            //Main setting
            //Application.targetFrameRate = 60;
            //Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.RunGame();
            Debug.Log("Count Loading");

        }


        private GameEntryPoint() {
            _coroutines = new GameObject("[COROUTINS]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad( _coroutines );

            UiRootView prefabUiRoot = Resources.Load<UiRootView>("UiRootView");
            _uiRootView = Object.Instantiate(prefabUiRoot);
            Object.DontDestroyOnLoad( _uiRootView );

            RunGame();
        }


        private void RunGame() {
#if UNITY_EDITOR
            string scene  = SceneManager.GetActiveScene().name;

            if(scene == ScenesName.MENU_SCENE) {
                return;
            }

            if(scene != ScenesName.BOOT_SCENE) {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene() {

            _uiRootView.ShowLoadingScreen();

            yield return SceneManager.LoadSceneAsync(ScenesName.BOOT_SCENE);
            yield return SceneManager.LoadSceneAsync(ScenesName.MENU_SCENE);
            yield return new WaitForSeconds(5);

            _uiRootView.HideLoadingScreen();
        }
    }
}