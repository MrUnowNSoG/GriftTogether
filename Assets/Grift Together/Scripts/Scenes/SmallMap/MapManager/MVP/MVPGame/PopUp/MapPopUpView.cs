using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GriftTogether {

    public class MapPopUpView : MonoBehaviour, IView {

        [SerializeField] private TextMeshProUGUI _timerText;

        public event Action OnClose;
        public event Action OnEndTimer;

        public void Initialize(IPresenter presenter) {
            ResetTimerText();
        }

        private void ResetTimerText() {
            _timerText.text = string.Empty;
            _timerText.gameObject.SetActive(false);
        }

        public void StartTimer(float timer) {
            StopAllCoroutines();
            StartCoroutine(TimerAnimation(timer));
        }

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            StopAllCoroutines();
            ResetTimerText();
        }


        private IEnumerator TimerAnimation(float timer) {

            _timerText.gameObject.SetActive(true);

            while (timer > 0) {
                int remaining = Mathf.CeilToInt(timer);
                _timerText.text = $"{remaining}";

                yield return new WaitForFixedUpdate();
                timer -= Time.fixedDeltaTime;
            }

            ResetTimerText();
            OnEndTimer?.Invoke();
        }
    }
}
