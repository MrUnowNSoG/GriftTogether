using System.Collections;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class ScenesManager : IService {


        private ScenesPresenter _presenter;

        public ScenesManager() {
            _presenter = new ScenesPresenter(); 
            _presenter.Initialize();
        }

        public void ShowLoadingScreen() {
            _presenter.ShowLoadingScreen();
        }

        public void HideLoadingScreen() {
            _presenter.HideLoadingScreen();
        }

        public void SwitchScene(string sceneName, bool autoLoadingScreen = true) {
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name == ScenesManagerConst.SAMPLE_SCENE) {
                HideLoadingScreen();
                return;
            }
#endif
            if (autoLoadingScreen) _presenter.ShowLoadingScreen();
            GameRoot.CoroutinsManager.LaunchCoroutin(LoadScene(sceneName, autoLoadingScreen));
        }


        private IEnumerator LoadScene(string sceneName, bool autoLoadinhScreen) {
            yield return SceneManager.LoadSceneAsync(ScenesManagerConst.BOOT_SCENE);
            yield return SceneManager.LoadSceneAsync(sceneName);

            if(autoLoadinhScreen) _presenter.HideLoadingScreen();
        }
    }
}
