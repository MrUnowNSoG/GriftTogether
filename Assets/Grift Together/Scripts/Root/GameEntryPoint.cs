using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class GameEntryPoint {



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartEntryGame() {
            new GameEntryPoint().RunGame();
        }


        private void RunGame() {
            //Base UI
            GameRoot.PrefabManager = new PrefabManager();

            GameRoot.CoroutinsManager = new GameObject("[COROUTINS]").AddComponent<CoroutinsManager>();
            Object.DontDestroyOnLoad(GameRoot.CoroutinsManager);

            GameRoot.ScenesManager = new ScenesManager();
            GameRoot.ScenesManager.ShowLoadingScreen();           

            GameRoot.PlayerPrefsManager = new PlayerPrefsManager();
            GameRoot.PlayerGlobalManager = new PlayerGlobalManager();

            GameRoot.LocalizationManager = new LocalizationManager();
            GameRoot.SoundManager = new SoundManager();

            GameRoot.ServiceLocator = new ServiceLocator(null);
            GameRoot.FireStoreManager = new FireStoreManager();

            GameRoot.ScenesManager.SwitchScene(ScenesName.LOGIN_SCENE, true);

        }
    }
}