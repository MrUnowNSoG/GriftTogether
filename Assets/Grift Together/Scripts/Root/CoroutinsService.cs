using System.Collections;
using UnityEngine;

namespace GriftTogether {


    public class CoroutinsService : MonoBehaviour {

        [Header("Info")]
        [SerializeField] private int _countStartCoroutins;


        public void StartCoroutin(IEnumerator coroutin) {

            _countStartCoroutins++;
            StartCoroutin(coroutin);
        }


        public void StopAllCoroutins() {
            StopAllCoroutins();
        }
    }

}
