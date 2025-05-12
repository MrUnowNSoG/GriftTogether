using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {
    
    public class MainMenuSettingView : MonoBehaviour, IView {

        [SerializeField] private MainMenuSettingScreenController _screenController;
        [SerializeField] private MainMenuSettingLanguageController _languageController;

        [Space(0)][Header("Sound Block")]
        [SerializeField] private Toggle _masterSoundToggle;
        [SerializeField] private Slider _soundVolumeSlider;
        [SerializeField] private Slider _musicVoluimeSlider;

        [SerializeField] private Button _backButton;


        private MainMenuSettingPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
           _presenter = (MainMenuSettingPresenter)presenter;

            _screenController.Init(_presenter.GetScreenService, _presenter.GetScreenType, _presenter.GetSreenResolution);
            _screenController.OnChangeScreen += ChangeScreen;

            _languageController.Init(_presenter.GetLanguage);
            _languageController.OnChangeLanguage += ChangeLanguage;

            _backButton.onClick.AddListener(BackButton);
        }

        private void ChangeScreen(int screen, string resolution) => _presenter.ChangeScreenValue(screen, resolution);

        private void ChangeLanguage(int language) => _presenter.ChangeLanguage(language);

        private void BackButton() {
            OnClose?.Invoke();
        }


        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            _screenController.OnChangeScreen -= ChangeScreen;
            _screenController.Deinitialize();

            _languageController.OnChangeLanguage -= ChangeLanguage;
            _languageController.Deinitialize();

            _backButton.onClick.RemoveListener(BackButton);
        }

    }
}
