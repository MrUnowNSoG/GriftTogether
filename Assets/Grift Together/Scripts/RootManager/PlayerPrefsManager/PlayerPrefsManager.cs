using UnityEngine;

namespace GriftTogether {

    public class PlayerPrefsManager {

        private const string TRUE_STRING = "True";
        private const string FALSE_STRING = "False";


        //Save

        public void SavePlayerData(PlayerSettingData setting, PlayerSkinData skin) {
            SavePlayerSetting(setting);
            SavePlayerSkin(skin);
        }

        public void SavePlayerSetting(PlayerSettingData data) {
            PlayerPrefs.SetInt(PlayerPrefsKey.LANGUAGE_GAME, (int)data.GameLanguage);

            PlayerPrefs.SetInt(PlayerPrefsKey.SCREEN_MODE, (int)data.TypeScreenMode);
            PlayerPrefs.SetString(PlayerPrefsKey.RESOLUTION_SCREEN, data.TypeResolutionScreen);

            SaveBool(PlayerPrefsKey.MASTER_SOUND_STATE, data.MasterSoundState);
            PlayerPrefs.SetFloat(PlayerPrefsKey.SOUND_VOLUME, data.VolumeSound);
            PlayerPrefs.SetFloat(PlayerPrefsKey.MUSIC_VOLUME, data.VolumeMusic);

            SaveBool(PlayerPrefsKey.SETTING_KEY, true);
            PlayerPrefs.Save();
        }

        public void SavePlayerSkin(PlayerSkinData data) {
            PlayerPrefs.Save();
        }

        //Load
        public PlayerSettingData LoadSettingData() {

            PlayerSettingData setting = null;
            
           if(PlayerPrefs.HasKey(PlayerPrefsKey.SETTING_KEY) && GetBool(PlayerPrefsKey.SETTING_KEY)) {
                setting = new PlayerSettingData();

                setting.GameLanguage = (LocalizationLanguage)GetInt(PlayerPrefsKey.LANGUAGE_GAME);
                
                setting.TypeScreenMode = GetInt(PlayerPrefsKey.SCREEN_MODE);
                setting.TypeResolutionScreen = GetString(PlayerPrefsKey.RESOLUTION_SCREEN);

                setting.MasterSoundState = GetBool(PlayerPrefsKey.MASTER_SOUND_STATE);
                setting.VolumeSound = GetFloat(PlayerPrefsKey.SOUND_VOLUME);
                setting.VolumeMusic = GetFloat(PlayerPrefsKey.MUSIC_VOLUME);
           }
            
            return setting;
        }

        //Extend
        private int GetInt(string key) {
            
            if(PlayerPrefs.HasKey(key)) {
                return PlayerPrefs.GetInt(key);
            }

            return 0;
        }


        private float GetFloat(string key) {

            if(PlayerPrefs.HasKey(key)) {
                return PlayerPrefs.GetFloat(key);
            }

            return 0.0f;
        }

        private string GetString(string key) {
            
            if( PlayerPrefs.HasKey(key)) { 
                return PlayerPrefs.GetString(key);
            }

            return string.Empty;
        }

        private bool GetBool(string key) {
            
            string str = GetString(key);
            
            if(str != string.Empty) { 
                if(str == TRUE_STRING) return true;
                if(str == FALSE_STRING) return false;
            }

            return false;
        }

        private void SaveBool(string key, bool state) {

            string str = FALSE_STRING;
            if (state) str = TRUE_STRING;
            
            PlayerPrefs.SetString(key, str);
        }
    }
}
