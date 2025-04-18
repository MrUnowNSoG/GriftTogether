using System.Collections;
using TMPro;
using UnityEngine;

namespace GriftTogether {

    public class UiRootView : MonoBehaviour {

        private const string LOADING_TEXT = "Loading";

        [SerializeField] private GameObject _loagingScreeGO;
        [SerializeField] private TMP_Text _loagingText;


        private void Awake() {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen() {
            _loagingScreeGO.SetActive(true);
            StartLoadingAnimation();
        }

        public void HideLoadingScreen() {
            StopLoadingAnimation();
            _loagingScreeGO.SetActive(false);
        }



        private void StartLoadingAnimation() {
            StartCoroutine(LoadingTextAnimation());
        }

        private void StopLoadingAnimation() {
            StopAllCoroutines();
        }

        private IEnumerator LoadingTextAnimation() {
            
            while (true) {

                string endText = LOADING_TEXT;

                for(int i = 0; i < 3; i++) {
                    endText += ".";
                    _loagingText.text = endText;
                    yield return new WaitForSeconds(0.2f);
                }

            }
        }
    }
}
