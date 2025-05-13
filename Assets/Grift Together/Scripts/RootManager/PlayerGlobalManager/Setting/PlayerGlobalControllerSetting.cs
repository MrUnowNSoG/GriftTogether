using UnityEngine;

namespace GriftTogether {
    public class PlayerGlobalControllerSetting {

        private PlayerSettingData _settingData;

        public int GetScreenMode => _settingData.TypeScreenMode;
        public string GetResolution => _settingData.TypeResolutionScreen;

        public LocalizationLanguage GetLanguage => _settingData.GameLanguage;

        public bool GetMasterSoundState => _settingData.MasterSoundState;
        public float GetVolumeSound => _settingData.VolumeSound;
        public float GetVolumeMusic => _settingData.VolumeMusic;


        public PlayerGlobalControllerSetting() {
            SetStartValue();
        }

        private void SetStartValue() {
            _settingData = new PlayerSettingData();

            _settingData.GameLanguage = LocalizationLanguage.English;

            _settingData.TypeScreenMode = (int)FullScreenMode.FullScreenWindow;
            _settingData.TypeResolutionScreen = ResolutionScreenConst.NATIVE;

            _settingData.MasterSoundState = true;
            _settingData.VolumeSound = SoundManagerConst.GAME_VOLUME_ON;
            _settingData.VolumeMusic = SoundManagerConst.GAME_VOLUME_ON;
        }

        public void LoadPPSetting() {
            _settingData = GameRoot.PlayerPrefsManager.LoadSettingData();

            if (_settingData == null) SetStartValue();    
        }

        public void SetSettingData(PlayerSettingData settingData) {
            _settingData = settingData;
            SavePlayerSetting();
        }

        public void SavePlayerSetting() {
            GameRoot.PlayerPrefsManager.SavePlayerSetting(_settingData);
        }

    }
}
