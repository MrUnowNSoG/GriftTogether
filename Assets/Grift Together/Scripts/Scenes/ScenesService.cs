using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class ScenesService : IService {


        private ScenesPresenter _presenter;

        public ScenesService() {
            _presenter = new ScenesPresenter(); 
            _presenter.Initialize();
        }


        public void SwitchScene(string sceneName) {
            _presenter.ShowLoadingScreen();
            GameRoot.CoroutinsService.LaunchCoroutin(LoadScene(sceneName));
        }


        private IEnumerator LoadScene(string sceneName) {
            yield return SceneManager.LoadSceneAsync(ScenesName.BOOT_SCENE);
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitForSeconds(5f);
            _presenter.HideLoadingScreen();
        }
    }
}
