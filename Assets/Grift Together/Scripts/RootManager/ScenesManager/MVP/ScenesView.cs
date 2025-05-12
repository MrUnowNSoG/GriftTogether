using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GriftTogether {

    public class ScenesView : MonoBehaviour, IView {

        private const float ANIMATION_DELEY = 0.5f;
        private const string LOADING_TEXT = "Loading";
        private const int COUNT_POINT_AFTER_LOADING_TEXT = 3;


        public event Action OnClose;

        [Space(0)] [Header("Base Loading Screen")]
        [SerializeField] private GameObject _loadingSceenGO;
        [SerializeField] private TextMeshProUGUI _loadingText;

        [Space(10)] [Header("Base error pop-up")]
        [SerializeField] private GameObject _errorPopUpGO;


        public void Initialize(IPresenter presenter) {
            _loadingSceenGO.SetActive(false);
            _errorPopUpGO.SetActive(false);
        }


        public void ShowUI() {
            _loadingSceenGO.SetActive(true);
            StartCoroutine(LoadingAnimation());
        }

        public void CloseUI() {
            OnClose?.Invoke();
            StopAllCoroutines();
            _loadingSceenGO.SetActive(false);
        }


        public void Deinitialize() {
            CloseUI();
        }


        private IEnumerator LoadingAnimation() {

            string text;

            while (true) { 
                text = GameRoot.LocalizationManager.Get(LOADING_TEXT);

                for(int i = 0; i < COUNT_POINT_AFTER_LOADING_TEXT; i++) {
                    text += ".";
                    _loadingText.text = text;
                    yield return new WaitForSeconds(ANIMATION_DELEY);
                }
            }

        }

    }
}
