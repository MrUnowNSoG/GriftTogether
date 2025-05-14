using Photon.Pun;
using Photon.Realtime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class PhotonManager : MonoBehaviourPunCallbacks {

        [Header("Photon Settings")]
        [Tooltip("Версія мережевого протоколу")]
        [SerializeField] private string _gameVersion = "1.1";
        [Tooltip("Максимальна кількість гравців у кімнаті")]
        [SerializeField] private byte _maxPlayers = 8;
        [Tooltip("Чи синхронізувати сцену автоматично в усіх клієнтів")]
        [SerializeField] private bool _autoSyncScene = true;
        [Tooltip("Частота відправки подій (Update) у разах на секунду")]
        [SerializeField] private int _sendRate = 20;
        [Tooltip("Частота серіалізації стану (трансформацій)")]
        [SerializeField] private int _serializationRate = 10;

        public void Init(string userName) {

            // 1. Версія протоколу
            PhotonNetwork.GameVersion = _gameVersion;

            // 2. Нікнейм гравця
            PhotonNetwork.NickName = userName;

            // 3. Автоматична синхронізація сцен
            PhotonNetwork.AutomaticallySyncScene = _autoSyncScene;

            // 4. Оптимізація мережевих кадрів
            PhotonNetwork.SendRate = _sendRate;
            PhotonNetwork.SerializationRate = _serializationRate;

            PhotonNetwork.ConnectUsingSettings();

        }

        public override void OnConnectedToMaster() {
            Debug.Log($"Connected to Photon Master Server. Region: {PhotonNetwork.CloudRegion}, Ping: {PhotonNetwork.GetPing()} ms");
        }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogError($"Disconnected from Photon: {cause}");
        }


        public void CreateRoom() {
            GameRoot.ScenesManager.ShowLoadingScreen();

            if(GameRoot.ServiceLocator.Resolve(out TextValidatorService textValidation) == false) {
                Debug.LogError("Can't resolve Text Validation Service!");
                GameRoot.ScenesManager.HideLoadingScreen();
                return;
            }

            string code;
            do {
                code = Random.Range(0, 1_000_000).ToString("D6");
            } while (textValidation.ValidationText(code, TextValidatorType.LobbyCode) == false);

            Debug.Log($"Room generate next code: {code}");

            var props = new ExitGames.Client.Photon.Hashtable {
                {"s1", GameRoot.PlayerGlobalManager.GetPlayerSkinData.HatName},
                {"s2", GameRoot.PlayerGlobalManager.GetPlayerSkinData.ColorName},
                {"s3", GameRoot.PlayerGlobalManager.GetPlayerSkinData.FaceName}
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);


            RoomOptions options = new RoomOptions() { MaxPlayers = _maxPlayers, IsVisible = true, IsOpen = true };
            PhotonNetwork.CreateRoom(code, options, TypedLobby.Default);
        }

        public override void OnCreatedRoom() {
            Debug.Log($"Room created: {PhotonNetwork.CurrentRoom.Name}");
            GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.LOBBY_SCENE, true);
        }

        public override void OnCreateRoomFailed(short returnCode, string message) {
            Debug.LogWarning($"CreateRoomFailed ({returnCode}): {message}");
            CreateRoom();
        }

        public void JoinRoom(string roomCode) {
            PhotonNetwork.JoinRoom(roomCode);
        }

        public void OnJoinedRoom() {
            //OnJoinedRoom викликається для нового гравця — саме тоді він бачить, хто вже в кімнаті.
            Debug.Log("Маємо старе OnJoinedRoom: " + SceneManager.GetActiveScene().name);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            //OnPlayerEnteredRoom викликається у всіх клієнтів(включно з тобою) коли хтось новий підключається.
            Debug.Log("Маємо старе OnPlayerEnteredRoom: " + SceneManager.GetActiveScene().name);
        }

    }
}
