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
            GameRoot.PrefabService = new PrefabService();

            GameRoot.ServiceLocator = new ServiceLocator(null);

            GameRoot.CoroutinsService = new GameObject("[COROUTINS]").AddComponent<CoroutinsService>();
            Object.DontDestroyOnLoad(GameRoot.CoroutinsService);

            GameRoot.ScenesService = new ScenesService();
            GameRoot.ScenesService.SwitchScene(ScenesName.MENU_SCENE);
        }
    }
}