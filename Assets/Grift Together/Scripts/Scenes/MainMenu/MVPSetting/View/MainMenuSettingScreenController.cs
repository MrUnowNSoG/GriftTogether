using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuSettingScreenController : MonoBehaviour {

        [SerializeField] private TMP_Dropdown _screenDropDown;
        [SerializeField] private TMP_Dropdown _resolutionDropDown;

        private ResolutionScreenService _resolutionScreenService;
        private ResolutionScreenConst _resolutios;

        private int _currentScreenMode;
        private string _currentResolution;

        public event Action<int, string> OnChangeScreen; 

        public void Init(ResolutionScreenService resolutionScreenService, int CurrentScreenMode, string CurrentResolution) {
            
            _resolutionScreenService = resolutionScreenService;
            _resolutios = _resolutionScreenService.GetResolutinConst;
            
            _currentScreenMode = CurrentScreenMode;
            _currentResolution = CurrentResolution;
            InitDropDownOptions();
            SetCurrentValue();
            InitDropDownEvent();
        }

        private void InitDropDownOptions() {
            _screenDropDown.ClearOptions();
            
            List<TMP_Dropdown.OptionData> optionScreen = new List<TMP_Dropdown.OptionData>() {
            new TMP_Dropdown.OptionData(GameRoot.LocalizationManager.Get(FullScreenMode.ExclusiveFullScreen.ToString())),
            new TMP_Dropdown.OptionData(GameRoot.LocalizationManager.Get(FullScreenMode.FullScreenWindow.ToString())),
            new TMP_Dropdown.OptionData(GameRoot.LocalizationManager.Get(FullScreenMode.MaximizedWindow.ToString())),
            new TMP_Dropdown.OptionData(GameRoot.LocalizationManager.Get(FullScreenMode.Windowed.ToString()))
            };

            _screenDropDown.AddOptions(optionScreen);


            _resolutionDropDown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionRes = new List<TMP_Dropdown.OptionData>();
            foreach(var item in _resolutios.GameResolution) {
                optionRes.Add(new TMP_Dropdown.OptionData(GameRoot.LocalizationManager.Get(item.Key)));
            }

            _resolutionDropDown.AddOptions(optionRes);
        }
        
        private void SetCurrentValue() {
            _screenDropDown.value = _currentScreenMode;

            if (InteractibleResolutionDropDown(_currentScreenMode)) {
             
                int value = 0;
                foreach (var item in _resolutios.GameResolution.Keys) {
                    if (item == _currentResolution) break;
                    value++;
                }

                _resolutionDropDown.value = value;
            }
        } 
    
        private void InitDropDownEvent() {
            _screenDropDown.onValueChanged.AddListener(ScreenModeChange);
            _resolutionDropDown.onValueChanged.AddListener(ResolutionChange);
        }

        private void ScreenModeChange(int index) {
            _currentScreenMode = index;
            InteractibleResolutionDropDown(index);
            OnChangeScreen?.Invoke(_currentScreenMode, _currentResolution);
        } 

        private void ResolutionChange(int index) {
            var key = _resolutios.GameResolution.Keys.ToList();
            _currentResolution = key[index];

            OnChangeScreen?.Invoke(_currentScreenMode, _currentResolution);
        }

        private bool InteractibleResolutionDropDown(int screenMode) {

            if (_resolutionScreenService.CanChangeScreenSize(screenMode) == false) {

                _resolutionDropDown.value = 0;
                ResolutionChange(0);
                _resolutionDropDown.interactable = false;
                return false;

            } else {
                _resolutionDropDown.interactable = true;
                return true;
            }
        }

        public void Deinitialize() {
            _screenDropDown.onValueChanged.RemoveListener(ScreenModeChange);
            _resolutionDropDown.onValueChanged.RemoveListener(ResolutionChange);
        }
    }
}
