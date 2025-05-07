using System;
using UnityEngine;
using UnityEngine.UI;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

namespace GriftTogether {

    public class LoginRegisterViewController : MonoBehaviour {

        private const string ERROR_DATE = "Error with date!";

        [Space(0)] [Header("Button Change Window")]
        [SerializeField] private Button _loginScreenButton;
        [SerializeField] private Button _registerScreenButton;

        [Space(10)] [Header("Windows")]
        [SerializeField] private LoginRegisterViewScreen _loginScreen;
        [SerializeField] private LoginRegisterViewScreen _registerScreen;


        private bool _isLogginScreen;
        private TextValidatorService _textValidatorService;

        public event Action<LoginRegisterData> OnLogin;
        public event Action<LoginRegisterData> OnRegister;

        public void Init() {
            BaseSetting();
            InitButton();
            InitScreen();
        }

        private void BaseSetting() {
            _isLogginScreen = false;
            ChangeWindow();
            _textValidatorService = new TextValidatorService();
        }

        private void InitButton() {
            _loginScreenButton.onClick.AddListener(ChangeWindow);
            _registerScreenButton.onClick.AddListener(ChangeWindow);
        }

        private void ChangeWindow() {
            _isLogginScreen = !_isLogginScreen;
            _loginScreen.gameObject.SetActive(_isLogginScreen);
            _registerScreen.gameObject.SetActive(!_isLogginScreen);
        }

        private void InitScreen() {
            _loginScreen.OnReguestDate += TryLogin;
            _registerScreen.OnReguestDate += TryRegister;

            _loginScreen.Init();
            _registerScreen.Init();
        }

        private void TryLogin(LoginRegisterData date) {

            if (_isLogginScreen == false) return; 

            if(ValidateString(date.Login, TextValidatorType.Login) == false) {
                _loginScreen.ErrorText(GameRoot.LocalizationManager.Get(ERROR_DATE));
                return;
            }

            if(ValidateString(date.Password, TextValidatorType.Password) == false) {
                _loginScreen.ErrorText(GameRoot.LocalizationManager.Get(ERROR_DATE));
                return;
            }

            OnLogin?.Invoke(date);
        }

        private void TryRegister(LoginRegisterData date) {

            if ((!_isLogginScreen) == false) return;

            if (ValidateString(date.Login, TextValidatorType.Login) == false) {
                _registerScreen.ErrorText(GameRoot.LocalizationManager.Get(ERROR_DATE));
                return;
            }

            if (ValidateString(date.UserName, TextValidatorType.UserName) == false) {
                _registerScreen.ErrorText(GameRoot.LocalizationManager.Get(ERROR_DATE));
                return;
            }

            if (ValidateString(date.Password, TextValidatorType.Password) == false) {
                _registerScreen.ErrorText(GameRoot.LocalizationManager.Get(ERROR_DATE));
                return;
            }

            OnRegister?.Invoke(date);
        }

        private bool ValidateString(string str,TextValidatorType type ) {
            return (str.Length > 0 && _textValidatorService.ValidationText(str, TextValidatorType.Login));
        }


        public void Deinitialize() {
            _loginScreen.OnReguestDate -= TryLogin;
            _registerScreen.OnReguestDate -= TryRegister;

            _loginScreen.gameObject.SetActive(false);
            _registerScreen.gameObject.SetActive(false);
        }
    }

}
