using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LoginRegisterViewController : MonoBehaviour {

        [Space(0)] [Header("Button Change Window")]
        [SerializeField] private Button _loginScreenButton;
        [SerializeField] private Button _registerScreenButton;

        [Space(10)] [Header("Windows")]
        [SerializeField] private LoginRegisterViewScreen _loginScreen;
        [SerializeField] private LoginRegisterViewScreen _registerScreen;


        private TextValidatorService _textValidatorService;

        public event Action<LoginRegisterData> OnLogin;
        public event Action<LoginRegisterData> OnRegister;

        public void Init() {
            BaseSetting();
            InitButton();
            InitScreen();
        }

        private void BaseSetting() {
            OpenLoadingScreen();
            GameRoot.ServiceLocator.Resolve(out _textValidatorService);
        }

        private void InitButton() {
            _loginScreenButton.onClick.AddListener(OpenLoadingScreen);
            _registerScreenButton.onClick.AddListener(OpenRegisterScreen);
        }

        private void OpenLoadingScreen() {
            _loginScreen.gameObject.SetActive(true);
            _registerScreen.gameObject.SetActive(false);
        }

        private void OpenRegisterScreen() {
            _loginScreen.gameObject.SetActive(false);
            _registerScreen.gameObject.SetActive(true);
        }

        private void InitScreen() {
            _loginScreen.OnReguestDate += TryLogin;
            _registerScreen.OnReguestDate += TryRegister;

            _loginScreen.Init();
            _registerScreen.Init();
        }

        private void TryLogin(LoginRegisterData date) {

            if(ValidateString(date.Login, TextValidatorType.Login) == false) {
                _loginScreen.SetErrorText(GameRoot.LocalizationManager.Get(_textValidatorService.RuleValidationText(TextValidatorType.Login)));
                return;
            }

            if(ValidateString(date.Password, TextValidatorType.Password) == false) {
                _loginScreen.SetErrorText(GameRoot.LocalizationManager.Get(_textValidatorService.RuleValidationText(TextValidatorType.Password)));
                return;
            }

            OnLogin?.Invoke(date);
        }

        private void TryRegister(LoginRegisterData date) {

            if (ValidateString(date.Login, TextValidatorType.Login) == false) {
                _registerScreen.SetErrorText(GameRoot.LocalizationManager.Get(_textValidatorService.RuleValidationText(TextValidatorType.Login)));
                return;
            }

            if (ValidateString(date.UserName, TextValidatorType.UserName) == false) {
                _registerScreen.SetErrorText(GameRoot.LocalizationManager.Get(_textValidatorService.RuleValidationText(TextValidatorType.UserName)));
                return;
            }

            if (ValidateString(date.Password, TextValidatorType.Password) == false) {
                _registerScreen.SetErrorText(GameRoot.LocalizationManager.Get(_textValidatorService.RuleValidationText(TextValidatorType.Password)));
                return;
            }

            OnRegister?.Invoke(date);
        }

        private bool ValidateString(string str,TextValidatorType type ) {
            return (str.Length > 0 && _textValidatorService.ValidationText(str, TextValidatorType.Login));
        }

        public void SetErrorText(string str) {
            str = GameRoot.LocalizationManager.Get(str);
            _loginScreen.SetErrorText(str);
            _registerScreen.SetErrorText(str);
        }

        public void Deinitialize() {
            _loginScreen.OnReguestDate -= TryLogin;
            _registerScreen.OnReguestDate -= TryRegister;

            _loginScreen.gameObject.SetActive(false);
            _registerScreen.gameObject.SetActive(false);
        }
    }

}
