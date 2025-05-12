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

        public void SavePlayerSettingToGlobal() {

            PlayerSettingData data = new PlayerSettingData();
            data.TypeScreenMode = TypeScreen;
            data.TypeResolutionScreen = TypeResolution;

            data.GameLanguage = Language;

            data.MasterSoundState = MasterAudoState;
            data.VolumeSound = SoundVolume;
            data.VolumeMusic = MusicVolume;

            GameRoot.PlayerGlobalManager.SetSettingData(data);
        }
    }
}
