using System.Threading.Tasks;
using UnityEngine;

namespace GriftTogether {

    public class PlayerGlobalControllerServer {

        private PlayerServerData _serverData;
        private PlayerFireStoreDTO _playerFireStoreDTO;

        public string GetUserName => _serverData.NameUser;
        public int GetCountWin => _serverData.CountWin;
        public int GetCountCoin => _serverData.CountCoin;


        public PlayerGlobalControllerServer() {
            _serverData = new PlayerServerData(string.Empty, "TestUser", 0, 0);
        }

        public void SetServerData(PlayerServerData data, PlayerFireStoreDTO dto) {
            _serverData = data;
            _playerFireStoreDTO = dto;
        }

        public async void SavePlayerServerData() {
            _playerFireStoreDTO.Nickname = _serverData.NameUser;
            _playerFireStoreDTO.CountWinSessions = _serverData.CountWin;
            _playerFireStoreDTO.Gold = _serverData.CountCoin;

            await GameRoot.FireStoreManager.SaveToCloud<PlayerFireStoreDTO>(_playerFireStoreDTO, FireStoreConst.PLAYER_COLLECTION, _playerFireStoreDTO.Login, true);
        }
    }
}
