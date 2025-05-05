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
            GameRoot.PrefabManager = new PrefabManager();

            GameRoot.ServiceLocator = new ServiceLocator(null);

            GameRoot.CoroutinsManager = new GameObject("[COROUTINS]").AddComponent<CoroutinsManager>();
            Object.DontDestroyOnLoad(GameRoot.CoroutinsManager);

            GameRoot.ScenesManager = new ScenesManager();
            GameRoot.ScenesManager.SwitchScene(ScenesName.LOGIN_SCENE);

            GameRoot.LocalizationManager = new LocalizationManager();
            GameRoot.SoundManager = new SoundManager();

            GameRoot.FireStoreManager = new FireStoreManager();
        }
    }
}