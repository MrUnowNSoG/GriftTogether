using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GriftTogether {


    public class MainMenuSettingLanguageController : MonoBehaviour {

        [SerializeField] private TMP_Dropdown _dropDown;

        public event Action<int> OnChangeLanguage;

        public void Init(LocalizationLanguage language) {
            InitDropDown();
            SetCurrentValue(language);
            InitDropDownEvent();
        }

        private void InitDropDown() {
            _dropDown.ClearOptions();

            List<TMP_Dropdown.OptionData> option = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < LocalizationManager.COUNT_GAME_LANGUAGE; i++) {
                option.Add(new TMP_Dropdown.OptionData(GameRoot.LocalizationManager.Get(((LocalizationLanguage)i).ToString())));
            }

            _dropDown.AddOptions(option);
        }

        private void SetCurrentValue(LocalizationLanguage language) {
            _dropDown.value = (int)language;
        }

        private void InitDropDownEvent() {
            _dropDown.onValueChanged.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage(int index) {
            OnChangeLanguage?.Invoke(index);
        }

        public void Deinitialize() {
            _dropDown.onValueChanged.RemoveAllListeners();
        }

    }
}
