using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class ScenesService : IService {


        private readonly CoroutinsService _coroutinsService;


        public ScenesService(CoroutinsService coroutin) {
            _coroutinsService = coroutin;
        }


        public void SwitchScene() {

            string scene = SceneManager.GetActiveScene().name;

            if (scene == ScenesName.MENU_SCENE) {
                return;
            }

            if (scene != ScenesName.BOOT_SCENE) {
                return;
            }

            _coroutinsService.StartCoroutin(LoadScene());
        }


        private IEnumerator LoadScene() {

            yield return SceneManager.LoadSceneAsync(ScenesName.BOOT_SCENE);
            yield return SceneManager.LoadSceneAsync(ScenesName.MENU_SCENE);
            yield return new WaitForSeconds(5);

        }
    }
}
