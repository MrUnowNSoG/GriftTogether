using UnityEngine;

namespace GriftTogether {

    public class PlayerPrefsManager {

        private const string TRUE_STRING = "True";
        private const string FALSE_STRING = "False";
        private const float TURN_ON_SOUND = 100f;
        private const float TURN_OFF_SOUND = 0f;

        private PlayerPrefsLocalData _localData;
        private PlayerPrefsServerData _serverData;

        public PlayerPrefsManager() {

           _localData = new PlayerPrefsLocalData();
            _serverData = new PlayerPrefsServerData();

            if(PlayerPrefs.HasKey(PlayerPrefsKey.FISRT_SESSION)) {
                if(GetBool(PlayerPrefsKey.FISRT_SESSION)) {

                    _localData.AllSound = GetBool(PlayerPrefsKey.ALL_SOUND_STATE);
                    _localData.GameLanguage = (LocalizationLanguage)GetInt(PlayerPrefsKey.LANGUAGE_GAME);
                    _localData.VolumeSound = GetFloat(PlayerPrefsKey.SOUND_STATE);
                    _localData.VolumeMusic = GetFloat(PlayerPrefsKey.MUSIC_STATE);
                }

            } else {
                SetStartValuePP();
                SetStartValue();
            }
        }

        private void SetStartValuePP() {
            PlayerPrefs.SetInt(PlayerPrefsKey.LANGUAGE_GAME, (int)LocalizationLanguage.English);

            PlayerPrefs.SetString(PlayerPrefsKey.ALL_SOUND_STATE, TRUE_STRING);
            PlayerPrefs.SetFloat(PlayerPrefsKey.SOUND_STATE, TURN_ON_SOUND);
            PlayerPrefs.SetFloat(PlayerPrefsKey.MUSIC_STATE, TURN_ON_SOUND);

            PlayerPrefs.SetString(PlayerPrefsKey.FISRT_SESSION, TRUE_STRING);
            PlayerPrefs.Save();
        }

        private void SetStartValue() {
            _localData.GameLanguage = LocalizationLanguage.English;
            _localData.AllSound = true;
            _localData.VolumeSound = TURN_ON_SOUND;
            _localData.VolumeMusic = TURN_ON_SOUND;
        }

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
    }
}
