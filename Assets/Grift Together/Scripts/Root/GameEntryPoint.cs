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
            
            BaseUiSystem();    
            SaveSystem();
            
            GameRoot.ServiceLocator = new ServiceLocator(null);
            
            SettingSystem();
            
            GameRoot.FireStoreManager = new FireStoreManager();

            GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.LOGIN_SCENE, true);

        }

        private void BaseUiSystem() {
            GameRoot.PrefabManager = new PrefabManager();

            GameRoot.CoroutinsManager = new GameObject("[COROUTINS]").AddComponent<CoroutinsManager>();
            Object.DontDestroyOnLoad(GameRoot.CoroutinsManager);

            GameRoot.ScenesManager = new ScenesManager();
            GameRoot.ScenesManager.ShowLoadingScreen();
        }

        private void SaveSystem() {
            GameRoot.PlayerPrefsManager = new PlayerPrefsManager();
            GameRoot.PlayerGlobalManager = new PlayerGlobalManager();
            GameRoot.PlayerGlobalManager.LoadPPSetting();
        }

        private void SettingSystem() {
            
            ResolutionScreenService screen = new ResolutionScreenService();
            screen.SetScreenType((FullScreenMode)GameRoot.PlayerGlobalManager.GetScreenMode);
            screen.TrySetScreenSize(GameRoot.PlayerGlobalManager.GetResolution);

            GameRoot.SoundManager = new SoundManager(GameRoot.PlayerGlobalManager.GetMasterSoundState,
                                         GameRoot.PlayerGlobalManager.GetVolumeSound,
                                         GameRoot.PlayerGlobalManager.GetVolumeMusic);

            GameRoot.CoroutinsManager.OnStartUnity += LateUpdate;

            GameRoot.LocalizationManager = new LocalizationManager(GameRoot.PlayerGlobalManager.GetLanguage);

            GameRoot.ServiceLocator.AddService(screen);
        }

        private void LateUpdate() {

            GameRoot.SoundManager.SetSetting(GameRoot.PlayerGlobalManager.GetMasterSoundState,
                                         GameRoot.PlayerGlobalManager.GetVolumeSound,
                                         GameRoot.PlayerGlobalManager.GetVolumeMusic);
        }
    }
}