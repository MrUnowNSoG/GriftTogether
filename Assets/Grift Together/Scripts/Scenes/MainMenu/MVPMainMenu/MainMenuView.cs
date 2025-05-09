using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MainMenuView : MonoBehaviour, IView {

        private MainMenuPresenter _presenter;
        private bool _isProcessing = false;

        [Space(0)] [Header("Main Menu Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _skinButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _exitButton;

        public event Action OnClose;


        public void Initialize(IPresenter presenter) {
            _presenter = (MainMenuPresenter)presenter;
            _isProcessing = false;
            InitButtons();
        }

        private void InitButtons() {
            _playButton.onClick.AddListener(PlayButton);
            _skinButton.onClick.AddListener(SkinButton);
            _skinButton.onClick.AddListener(SettingButton);
            _exitButton.onClick.AddListener(ExitButton);
        }

        private void PlayButton() {  
            if(CanProcess()) _presenter.PlayUI();
        }

        private void SkinButton() {
            if(CanProcess()) _presenter.SkinUI();
        }

        private void SettingButton() {
            if(CanProcess()) _presenter.SettingUI();
        }

        private void ExitButton() {
            if(CanProcess()) _presenter.ExitUI();
        }

        private bool CanProcess() {
            if( _isProcessing == false) {
                _isProcessing = true;
                return true;
            }

            return false;
        }

        public void ShowUI() {
            _isProcessing = false;
            this.gameObject.SetActive(true);
        }

        public void CloseUI() {
            _isProcessing = true;
            this.gameObject.SetActive(false);

            OnClose?.Invoke();
        }

        public void Deinitialize() {
            _playButton.onClick.RemoveAllListeners();
            _skinButton.onClick.RemoveAllListeners();
            _skinButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

    }
}
