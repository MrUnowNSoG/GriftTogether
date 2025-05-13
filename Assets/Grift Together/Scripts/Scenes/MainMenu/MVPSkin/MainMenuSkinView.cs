using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MainMenuSkinView : MonoBehaviour, IView {

        [Space(0)][Header("UI Controllers")]
        [SerializeField] private MainMenuSkinViewController _hatController;
        [SerializeField] private MainMenuSkinViewController _colorController;
        [SerializeField] private MainMenuSkinViewController _faceController;

        [Space(10)]
        [Header("Button")]
        [SerializeField] private Button _backButton;
        
        private MainMenuSkinPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MainMenuSkinPresenter)presenter;
            _backButton.onClick.AddListener(CloseUI);
        }

        public void InitUi(string nameHat, string nameColor, string nameFace) {
            _hatController.Init(nameHat);
            _colorController.Init(nameColor);
            _faceController.Init(nameFace);

            _hatController.OnSwipe += HatChange;
            _colorController.OnSwipe += ColorChange;
            _faceController.OnSwipe += FaceChange;
        }

        private void HatChange(int direction) {
            string str = _presenter.HatChange(direction);
            _hatController.UpdateSkinName(str);
        }
        private void ColorChange(int direction) {
            string str = _presenter.ColorChange(direction);
            _colorController.UpdateSkinName(str);
        }
        private void FaceChange(int direction) {
            string str = _presenter.FaceChange(direction);
            _faceController.UpdateSkinName(str);
        }

        public void ShowUI() => gameObject.SetActive(true);

        public void CloseUI() {
            gameObject.SetActive(false);
            OnClose?.Invoke();
        }

        public void Deinitialize() {
            _faceController.OnSwipe -= FaceChange;
            _colorController.OnSwipe -= ColorChange;
            _hatController.OnSwipe -= HatChange;

            _faceController.Deinitialize();
            _colorController.Deinitialize();
            _hatController.Deinitialize();

            _backButton.onClick.RemoveListener(CloseUI);
        }

    }
}
