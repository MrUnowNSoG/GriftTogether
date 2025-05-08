using System.Collections;
using UnityEngine;

namespace GriftTogether {


    public class CoroutinsManager : MonoBehaviour {

        [Header("Info")]
        [SerializeField] private int _countStartCoroutins;


        //Base
        public void LaunchCoroutin(IEnumerator coroutin) {

            _countStartCoroutins++;
            StartCoroutine(coroutin);
        }


        public void ReinCoroutin(IEnumerator coroutin) {

            _countStartCoroutins++;
            StopCoroutine(coroutin);
        }

        public void ReinAllCoroutins() {
            _countStartCoroutins = 0;
            StopAllCoroutines();
        }


        //Animation
        public void FadeAnimation(CanvasGroup group, float startValue, float endValue, float time, bool isActive = true) {

            if (time <= 0) {
                Debug.LogError("Fade Animation with zero time!");
                return;
            }
            if (startValue == endValue) {
                Debug.LogError("Fade Animation with equals start and end value!");
                return;
            }
            if(group == null) {
                Debug.LogError("Fade Animation Canvas droup equals null!");
                return;
            }

            LaunchCoroutin(FadeAnimationInstrucion(group, startValue, endValue, time, isActive));
        }

        private IEnumerator FadeAnimationInstrucion(CanvasGroup group, float startValue, float endValue, float time, bool isActive = true) {
            group.alpha = startValue;

            float timer = 0;

            while(timer <= time) {
                timer += Time.fixedDeltaTime;

                float t = Mathf.Lerp(0, time, timer);
                group.alpha = Mathf.Lerp(startValue, endValue, t);

                yield return new WaitForFixedUpdate();
            }

            group.alpha = endValue;
            group.gameObject.SetActive(isActive);
        }
    }

}
