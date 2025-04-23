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

            GameRoot.serviceLocator = new ServiceLocator(null);

            GameRoot.coroutinsService = new GameObject("[COROUTINS]").AddComponent<CoroutinsService>();
            Object.DontDestroyOnLoad(GameRoot.coroutinsService);

            
            //TODO: Service Locator and ScenesManager
        }
    }
}