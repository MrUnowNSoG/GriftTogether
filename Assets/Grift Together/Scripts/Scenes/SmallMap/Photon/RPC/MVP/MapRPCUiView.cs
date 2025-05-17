using System;
using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

namespace GriftTogether {

    public class MapRPCUiView : MonoBehaviour, IView {

        [Header("FadeGroup")]
        [SerializeField] private CanvasGroup _fadeGroup;
        [SerializeField] private TextMeshProUGUI _fadeText;
        [SerializeField] private float _fadeDuration = 3f;

        public event Action OnClose;


        public void Initialize(IPresenter presenter) {
            DisableFadeGroup();
        }

        public void ShowFadeLog(string message) {
            StopAllCoroutines();
            StartCoroutine(FadeOutAndDisable(message));
        }

        public void ShowUI() {}

        public void CloseUI() => DisableFadeGroup();

        private void DisableFadeGroup() {
            _fadeGroup.gameObject.SetActive(false);
            _fadeGroup.alpha = 0f;
            _fadeText.text = string.Empty;
        }

        public void Deinitialize() {
            DisableFadeGroup();
        }


        private IEnumerator FadeOutAndDisable(string message) {

            _fadeText.text = message;
            _fadeGroup.alpha = 1f;
            _fadeGroup.gameObject.SetActive(true);

            float elapsed = 0f;
            while (elapsed <= _fadeDuration) {
                elapsed += Time.fixedDeltaTime;
                _fadeGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);
                yield return new WaitForFixedUpdate();
            }

            DisableFadeGroup();
        }

    }
}
