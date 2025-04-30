using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class GameEntryPoint {


        public static GameEntryPoint _instance;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartEntryGame() {
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }


        private void RunGame() {
            GameRoot.ServiceLocator = new ServiceLocator(null);

            GameRoot.PrefabService = new PrefabService();

            GameRoot.CoroutinsService = new GameObject("[COROUTINS]").AddComponent<CoroutinsService>();
            Object.DontDestroyOnLoad(GameRoot.CoroutinsService);

            GameRoot.ScenesService = new ScenesService();

            GameRoot.ScenesService.SwitchScene(ScenesName.MENU_SCENE);
        }
    }
}