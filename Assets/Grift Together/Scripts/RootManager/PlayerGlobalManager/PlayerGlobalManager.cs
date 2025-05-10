using UnityEngine;

namespace GriftTogether {

    public class PlayerGlobalManager {

        private const float SOUND_TURN_ON = 100f;

        private PlayerSettingData _settingData;
        private PlayerSkinData _skinData;
        private PlayerServerData _serverData;


        public PlayerGlobalManager() { }

        public void DownloadPPSetting() {
            _settingData = GameRoot.PlayerPrefsManager.LoadSettingData();
            
            if(_settingData == null ) { 
                _settingData = new PlayerSettingData();

                _settingData.GameLanguage = LocalizationLanguage.English;

                _settingData.TypeScreenMode = (int)FullScreenMode.FullScreenWindow;
                _settingData.TypeResolutionScreen = ResolutionScreen.NATIVE;

                _settingData.MasterSoundState = true;
                _settingData.VolumeSound = SOUND_TURN_ON;
                _settingData.VolumeMusic = SOUND_TURN_ON;
            }
        }

        public LocalizationLanguage GetLanguage => _settingData.GameLanguage;

    }
}
