using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LoginRegisterViewScreen : MonoBehaviour {
    
        [Space(0)] [Header("Input")]
        [SerializeField] private TMP_InputField _loginInput;
        [SerializeField] private TMP_InputField _userNameInput;
        [SerializeField] private TMP_InputField _passwordInput;

        [Space(10)] [Header("Button")]
        [SerializeField] private Button _reguestButton;

        [Space(10)] [Header("Text for Error")]
        [SerializeField] TextMeshProUGUI _errorText;

        private TextValidatorService _textValidator;

        public Action<LoginRegisterData> OnReguestDate;

        public void Init() {
            _textValidator = new TextValidatorService();
            _errorText.text = "";

            InitInput();
            InitButton();
        }

        private void InitInput() {
            _loginInput.onEndEdit.AddListener(LoginStringValidation);
            _userNameInput.onEndEdit.AddListener(UserNameStringValidator);
            _passwordInput.onEndEdit.AddListener(PasswordStringValidator);
        }

        private void LoginStringValidation(string str) => ValidateInput(str, TextValidatorType.Login);
        private void UserNameStringValidator(string str) => ValidateInput(str, TextValidatorType.UserName);
        private void PasswordStringValidator(string str) => ValidateInput(str, TextValidatorType.Password);

        private void ValidateInput(string str, TextValidatorType type) {

            if (_textValidator.ValidationText(str, TextValidatorType.Login)) {
                _errorText.text = "";
                return;
            }

            _errorText.text = _textValidator.RuleValidationText(TextValidatorType.Login);
        }

        private void InitButton() {
            _reguestButton.onClick.AddListener(ReguestButton);
        }

        private void ReguestButton() {

            LoginRegisterData loginRegisterData = new LoginRegisterData();
            loginRegisterData.Login = _loginInput.text;
            loginRegisterData.UserName = _userNameInput.text;
            loginRegisterData.Password = _passwordInput.text;

            OnReguestDate?.Invoke(loginRegisterData);
        }

        public void SetErrorText(string str) {
            _errorText.text = str;
        }
    }
}
