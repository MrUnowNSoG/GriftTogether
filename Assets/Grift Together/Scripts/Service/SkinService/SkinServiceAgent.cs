using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class SkinServiceAgent : MonoBehaviour {

        [SerializeField] private GameObject _hatParent;
        public GameObject GetHatParent => _hatParent;

        [SerializeField] private GameObject _colorParent;
        public GameObject GetColorParent => _colorParent;

        [SerializeField] private GameObject _faceParent;
        public GameObject GetFaceParent => _faceParent;


        private GameObject _currentHat;
        private GameObject _currentColor;
        
        private GameObject _currentFace;
        
        public void SetHat(GameObject ob) {
            if (_currentHat != null) ClearHat();
            _currentHat = ob;
        }

        private void ClearHat() {
            if(_currentHat != null) {
                Destroy(_currentHat);
                _currentHat = null;
            }
        }

        public void SetColor(GameObject ob) {
            if (_currentColor != null) ClearColor();
            _currentColor = ob;
        }

        private void ClearColor() {
            if (_currentColor != null) {
                Destroy(_currentColor);
                _currentColor = null;
            }
        }

        public void SetFace(GameObject ob) {
            if (_currentFace != null) ClearFace();
            _currentFace = ob;
        }

        private void ClearFace() {
            if (_currentFace != null) {
                Destroy(_currentFace);
                _currentFace = null;
            }
        }

    }
}
