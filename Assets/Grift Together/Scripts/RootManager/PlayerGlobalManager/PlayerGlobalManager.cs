using UnityEngine;

namespace GriftTogether {

    public class PlayerGlobalManager {

        private PlayerSettingData _settingData;
        private PlayerSkinData _skinData;
        private PlayerServerData _serverData;

        private PlayerFireStoreDTO _playerFireStoreDTO;

        public PlayerGlobalManager() {
            _serverData = new PlayerServerData();
            _serverData.NameUser = "Load";
            _serverData.CountWin = 0;
            _serverData.CountCoin = 0;
        }

        public void DownloadPPSetting() {
            _settingData = GameRoot.PlayerPrefsManager.LoadSettingData();
            
            if(_settingData == null ) { 
                _settingData = new PlayerSettingData();

                _settingData.GameLanguage = LocalizationLanguage.English;

                _settingData.TypeScreenMode = (int)FullScreenMode.FullScreenWindow;
                _settingData.TypeResolutionScreen = ResolutionScreen.NATIVE;

                _settingData.MasterSoundState = true;
                _settingData.VolumeSound = SoundManagerConst.GAME_VOLUME_ON;
                _settingData.VolumeMusic = SoundManagerConst.GAME_VOLUME_ON;
            }
        }

        public int GetScreenMode => _settingData.TypeScreenMode;
        public string GetResolution => _settingData.TypeResolutionScreen;

        public LocalizationLanguage GetLanguage => _settingData.GameLanguage;

        public bool GetMasterSoundState => _settingData.MasterSoundState;
        public float GetVolumeSound => _settingData.VolumeSound;
        public float GetVolumeMusic => _settingData.VolumeMusic;

        public void SetSettingData(PlayerSettingData settingData) {
            _settingData = settingData;
            SavePlayerSetting();
        }

        public void SavePlayerSetting() {
            GameRoot.PlayerPrefsManager.SavePlayerSetting(_settingData);
        }


        public void LoadServerData(PlayerServerData data, PlayerFireStoreDTO dto) {
            _serverData = data;
            _playerFireStoreDTO = dto;
        }

        public string UserName => _serverData.NameUser;
        public int CountWin => _serverData.CountWin;
        public int CountCoin => _serverData.CountCoin;
        
        public async void SavePlayerServerData() {
            _playerFireStoreDTO.Nickname = _serverData.NameUser;
            _playerFireStoreDTO.CountWinSessions = _serverData.CountWin;
            _playerFireStoreDTO.Gold = _serverData.CountCoin;

            await GameRoot.FireStoreManager.SaveToCloud<PlayerFireStoreDTO>(_playerFireStoreDTO, FireStoreConst.PLAYER_COLLECTION, _playerFireStoreDTO.Login, true);
        }
    }
}
