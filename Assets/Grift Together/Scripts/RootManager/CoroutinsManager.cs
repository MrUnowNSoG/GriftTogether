using System.Collections;
using UnityEngine;

namespace GriftTogether {


    public class CoroutinsManager : MonoBehaviour {

        [Header("Info")]
        [SerializeField] private int _countStartCoroutins;


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
    }

}
