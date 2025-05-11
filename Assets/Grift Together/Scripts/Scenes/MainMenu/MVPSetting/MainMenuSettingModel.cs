using UnityEngine;

namespace GriftTogether {

    public class MainMenuSettingModel {

        public int TypeScreen;
        public string TypeResolution;
        
        public LocalizationLanguage Language;

        public bool MasterAudoState;
        public float SoundVolume;
        public float MusicVolume;


        public void Init() {
            TypeScreen = GameRoot.PlayerGlobalManager.GetScreenMode;
            TypeResolution = GameRoot.PlayerGlobalManager.GetResolution;

            Language = GameRoot.PlayerGlobalManager.GetLanguage;

            MasterAudoState = GameRoot.PlayerGlobalManager.GetMasterSoundState;
            SoundVolume = GameRoot.PlayerGlobalManager.GetVolumeSound;
            MusicVolume = GameRoot.PlayerGlobalManager.GetVolumeMusic;
        }
    }
}
