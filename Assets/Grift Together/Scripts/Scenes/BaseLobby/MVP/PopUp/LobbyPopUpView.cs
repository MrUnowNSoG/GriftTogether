using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPopUpView : MonoBehaviour, IView {

        [Space(0)][Header("Timer")]
        [SerializeField] private TextMeshProUGUI _timerText;

        [Space(0)][Header("Dust")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 1.5f;
        [SerializeField] private TextMeshProUGUI _changeDustText;
        [SerializeField] private TextMeshProUGUI _dustText;

        public event Action OnClose;
        public event Action OnEndTimer;

        public void Initialize(IPresenter presenter) {
            ResetTimerText();
            _canvasGroup.gameObject.SetActive(false);
        }

        private void ResetTimerText() {
            _timerText.text = string.Empty;
            _timerText.gameObject.SetActive(false);
        }

        public void StartTimerForStart(float timer) {
            StopAllCoroutines();
            _canvasGroup.gameObject.SetActive(false);
            StartCoroutine(TimerAnimation(timer));
        }

        public void ChangeDust(string message, string dust) {
            StopAllCoroutines();
            StartCoroutine(FadeOutAndDisable(message, dust));
        }

        public void ShowUI() {}
        public void CloseUI() {}
        public void Deinitialize() {
            StopAllCoroutines();
        }

        private IEnumerator TimerAnimation(float timer) {

            _timerText.gameObject.SetActive(true);

            while (timer > 0) {
                int remaining = Mathf.CeilToInt(timer);
                _timerText.text = $"{remaining}";
                
                yield return new WaitForFixedUpdate();
                timer -= Time.fixedDeltaTime;
            }

            GameRoot.ScenesManager.ShowLoadingScreen();
            ResetTimerText();
            OnEndTimer?.Invoke();
        }

        private IEnumerator FadeOutAndDisable(string message, string dust) {

            _changeDustText.text = GameRoot.LocalizationManager.Get(message);
            _dustText.text = dust;
            
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;

            float elapsed = 0f;
            while (elapsed <= _fadeDuration) {
                elapsed += Time.fixedDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);
                yield return new WaitForFixedUpdate();
            }

            _canvasGroup.alpha = 0f;
            _canvasGroup.gameObject.SetActive(false);
        }
    }
}
