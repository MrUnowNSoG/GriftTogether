using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {
    public class MapPhotonManager : MonoBehaviourPunCallbacks {

        [SerializeField] private Button _leaveButton;

        private void Awake() {
            GameRoot.ServiceLocator.Resolve(out SkinPhotonService servic);
            servic.CreatePhotonPlayer(PhotonNetwork.LocalPlayer);

            _leaveButton.onClick.AddListener(LeaveButton);
            GameRoot.ScenesManager.HideLoadingScreen();
        }

        private void LeaveButton() {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom() {
            GameRoot.ScenesManager.ShowLoadingScreen();
            GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.MENU_SCENE, true);
        }

    }
}
