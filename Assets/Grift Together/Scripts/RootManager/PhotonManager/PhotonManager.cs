using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {

    public class PhotonManager : MonoBehaviourPunCallbacks {

        [Header("Photon Settings")]
        [Tooltip("Версія мережевого протоколу")]
        [SerializeField] private string _gameVersion = "1.1";
        [Tooltip("Максимальна кількість гравців у кімнаті")]
        
        [SerializeField] private int _maxPlayers = 8;
        public int GetMaxPlayer => _maxPlayers;

        [Tooltip("Чи синхронізувати сцену автоматично в усіх клієнтів")]
        [SerializeField] private bool _autoSyncScene = true;
        [Tooltip("Частота відправки подій (Update) у разах на секунду")]
        [SerializeField] private int _sendRate = 20;
        [Tooltip("Частота серіалізації стану (трансформацій)")]
        [SerializeField] private int _serializationRate = 10;


        private string _codeRoom;
        public string GetCodeRoom => _codeRoom;

        public PhotonRPCContext CurrentPhotonContext;

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
            Debug.Log($"Disconnected from Photon: {cause}");
        }

        public void CreateRoom() {

            GameRoot.ScenesManager.ShowLoadingScreen();

            _codeRoom = GenerateRoomCode();
            if(_codeRoom == null) {
                GameRoot.ScenesManager.HideLoadingScreen();
                return;
            }

            SetPhotonPlayerSkin();

            RoomOptions options = new RoomOptions() { MaxPlayers = _maxPlayers, IsVisible = true, IsOpen = true };
            PhotonNetwork.CreateRoom(_codeRoom, options, TypedLobby.Default);
        }

        private string GenerateRoomCode() {

            if (GameRoot.ServiceLocator.Resolve(out TextValidatorService textValidation) == false) {
                Debug.LogError("Can't resolve Text Validation Service!");
                return null;
            }

            string code;

            do {
                code = Random.Range(0, 1_000_000).ToString("D6");
            } while (textValidation.ValidationText(code, TextValidatorType.LobbyCode) == false);

            Debug.Log($"Room generate next code: {code}");

            return code;
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

            _codeRoom = roomCode;
            SetPhotonPlayerSkin();
            PhotonNetwork.JoinRoom(_codeRoom);
        }

        private void SetPhotonPlayerSkin() {

            var props = new ExitGames.Client.Photon.Hashtable {
                {PhotonManagerConst.HAT_SKIN_KEY, GameRoot.PlayerGlobalManager.GetPlayerSkinData.HatName},
                {PhotonManagerConst.COLOR_SKIN_KEY, GameRoot.PlayerGlobalManager.GetPlayerSkinData.ColorName},
                {PhotonManagerConst.FACE_SKIN_KEY, GameRoot.PlayerGlobalManager.GetPlayerSkinData.FaceName}
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }
    }
}
