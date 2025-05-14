using UnityEngine;

namespace GriftTogether {

    public class PlayerGlobalManager {

        private PlayerGlobalControllerSetting _settingController;
        private PlayerGlobalControllerServer _serverController;
        private PlayerGlobalControllerSkin _skinController;

        public PlayerGlobalManager() {
            _settingController = new PlayerGlobalControllerSetting();
            _serverController = new PlayerGlobalControllerServer();
            _skinController = new PlayerGlobalControllerSkin();
        }


        public int GetScreenMode => _settingController.GetScreenMode;
        public string GetResolution => _settingController.GetResolution;

        public LocalizationLanguage GetLanguage => _settingController.GetLanguage;

        public bool GetMasterSoundState => _settingController.GetMasterSoundState;
        public float GetVolumeSound => _settingController.GetVolumeSound;
        public float GetVolumeMusic => _settingController.GetVolumeMusic;

        public void LoadPPSetting() => _settingController.LoadPPSetting();
        public void SetSettingData(PlayerSettingData data) => _settingController.SetSettingData(data);
        public void SavePlayerSetting() => _settingController.SavePlayerSetting();



        public string GetUserName => _serverController.GetUserName;
        public int GetCountWin => _serverController.GetCountWin;
        public int GetCountCoin => _serverController.GetCountCoin;

        public void SetServerData(PlayerServerData data, PlayerFireStoreDTO dto) => _serverController.SetServerData(data, dto);
        public void SavePlayerServerData() => _serverController.SavePlayerServerData();



        public PlayerSkinData GetPlayerSettingData => _skinController.GetCurrentPlayerSkinData;

        public PlayerSkinData LoadPlayerSkin() => _skinController.LoadPlayerSkin();
        public void SavePlayerSkin(PlayerSkinData data) => _skinController.SavePlayerSkin(data);
    }
}
