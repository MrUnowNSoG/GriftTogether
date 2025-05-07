using System.Text.RegularExpressions;
using UnityEngine;

namespace GriftTogether {

    public class TextValidatorService {

        private const string LOGIN_RULE = @"Login must be at least 6 characters long and may only include letters, digits, underscore, at-sign for email.";
        private const string NAMEUSER_RULE = @"Username must be 4–16 characters long and may only include letters, digits, and underscore.";
        private const string PASSWORD_RULE = @"Password must be at least 6 characters long and may only include letters, digits and special symbol(underscore, hyphen,at-sign, dollar sign).";

        private Regex Login = new Regex("^[A-Za-z0-9_@.]{6,}$", RegexOptions.Compiled);
        private Regex NameUser = new Regex("^[A-Za-z0-9_]{4,16}$", RegexOptions.Compiled);
        private Regex Password = new Regex("^[A-Za-z0-9_@$-]{6,}$", RegexOptions.Compiled);


        public TextValidatorService() { }

        public bool ValidationText(string text, TextValidatorType type) { 
        
            switch (type) {
                case TextValidatorType.Login:
                    return Login.IsMatch(text);

                case TextValidatorType.UserName:
                    return NameUser.IsMatch(text);

                case TextValidatorType.Password:
                    return Password.IsMatch(text);
            }

            return false;
        }

        public string RuleValidationText(TextValidatorType type) {

            switch (type) {
                case TextValidatorType.Login:
                    return GameRoot.LocalizationManager.Get(LOGIN_RULE);

                case TextValidatorType.UserName:
                    return GameRoot.LocalizationManager.Get(NAMEUSER_RULE);

                case TextValidatorType.Password:
                    return GameRoot.LocalizationManager.Get(PASSWORD_RULE);
            }

            return "";
        }

    }
}
