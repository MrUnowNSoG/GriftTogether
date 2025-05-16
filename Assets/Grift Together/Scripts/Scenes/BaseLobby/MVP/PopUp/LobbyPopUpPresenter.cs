using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPopUpPresenter : IPresenter {

        private GameObject _root;

        private LobbyPopUpView _view;

        public LobbyPopUpPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(LobbyPrefabType.PopUp, _root).GetComponent<LobbyPopUpView>();
            _view.Initialize(this);
            _view.OnEndTimer += SwitchScene;
        }

        public void ShowTimerPopUp(float timer) => _view.StartTimerForStart(timer);

        public void ChangeDust(string message, string dust) => _view.ChangeDust(message, dust); 

        private void SwitchScene() {
            if (PhotonNetwork.IsMasterClient == false) return;
            PhotonNetwork.LoadLevel(ScenesManagerConst.SMALL_MAP_SCENE);
        }

        public void ShowUI() {}
        public void CloseUI() {}

        public void Deinitialize() {
            _view.OnEndTimer -= SwitchScene;
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
