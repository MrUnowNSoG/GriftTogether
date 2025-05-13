using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MainMenuSkinViewController : MonoBehaviour {

        private const int RIGHT_DIRECTION = 1;
        private const int LEFT_DIRECTION = -1;

        [SerializeField] private TextMeshProUGUI _skinName;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public event Action<int> OnSwipe;

        public void Init(string nameSkin) {
            UpdateSkinName(nameSkin);

            _leftButton.onClick.AddListener(LeftButton);
            _rightButton.onClick.AddListener(RightButton);
        }

        public void UpdateSkinName(string name) {
            _skinName.text = GameRoot.LocalizationManager.Get(name);
        }

        private void RightButton() {
            OnSwipe?.Invoke(RIGHT_DIRECTION);
        }

        private void LeftButton() {
            OnSwipe?.Invoke(LEFT_DIRECTION);
        }

        public void Deinitialize() {
            _leftButton.onClick.RemoveListener(LeftButton);
            _rightButton.onClick.RemoveListener(RightButton);
        }
    }
}
