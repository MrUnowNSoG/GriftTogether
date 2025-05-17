using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GriftTogether {

    public class AuthService {

        private readonly FireStoreManager _fs;
        private readonly SecureHasherService _hasher;

        private const int MIN_COUNT_ITERATION = 100_000;
        private const int MAX_COUNT_ITERATION = 500_000;

        public Action<string> OnError;

        public AuthService(FireStoreManager fs) {
            _fs = fs;
            _hasher = new SecureHasherService();
        }

        public async Task<bool> RegisterAsync(string login, string userName, string password) {
            
            var existing = await _fs.ExistFile(FireStoreConst.PLAYER_COLLECTION, login, true);
            if (existing) {
                OnError?.Invoke(GameErrorConst.BD_USER_FIND);
                return false;
            }

            PlayerFireStoreDTO dto = CreateBasePlayer(login, userName, password);
            
            SavePlayrData(dto);
            await SavePlayerToCloud(dto);

            return true;
        }

        private PlayerFireStoreDTO CreateBasePlayer(string login, string userName, string password) {

            int iterations = UnityEngine.Random.Range(MIN_COUNT_ITERATION, MAX_COUNT_ITERATION);
            _hasher.HashPassword(password, iterations, out var hash, out var salt);

            PlayerFireStoreDTO player = new PlayerFireStoreDTO {
                Login = login,
                Nickname = userName,
                Hash = Convert.ToBase64String(hash),
                Salt = Convert.ToBase64String(salt),
                Iterations = iterations,
                Gold = 0,
                CountWinSessions = 0,
            };

            return player;
        }

        private Task SavePlayerToCloud(PlayerFireStoreDTO dto) {
            return _fs.SaveToCloud<PlayerFireStoreDTO>(dto, FireStoreConst.PLAYER_COLLECTION, dto.Login, true);
        }

        public async Task<bool> LoginAsync(string login, string password) {
            
            PlayerFireStoreDTO player = await _fs.TryGetFile<PlayerFireStoreDTO>(FireStoreConst.PLAYER_COLLECTION, login, true);
            if (player == null) {
                OnError?.Invoke(GameErrorConst.BD_USER_CANT_FIND);
                return false;
            }

            if (string.IsNullOrEmpty(password) == false) {
                var storedHash = Convert.FromBase64String(player.Hash);
                var storedSalt = Convert.FromBase64String(player.Salt);

                if (_hasher.VerifyPassword(password, storedHash, storedSalt, player.Iterations) == false) {
                    OnError?.Invoke(GameErrorConst.INCORRECT_PASSWORD);
                    return false;
                }

            } else {
                OnError?.Invoke(GameErrorConst.BD_ERROR_DATE);
                return false;
            }

            SavePlayrData(player);
            return true;
        }

        private void SavePlayrData(PlayerFireStoreDTO data) {
            PlayerServerData playerData = new PlayerServerData(data.Login, data.Nickname, data.CountWinSessions, (int)data.Gold);

            GameRoot.PlayerGlobalManager.SetServerData(playerData, data);
            GameRoot.PhotonManager.Init(data.Nickname);
        }
    }
}
