using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class MapPopUpPresenter : IPresenter {

        private GameObject _root;
        private ServiceLocator _serviceLocator;

        private MapPopUpView _view;

        public MapPopUpPresenter(GameObject root, ServiceLocator locator) {
            _root = root;
            _serviceLocator = locator;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.PopUp, _root).GetComponent<MapPopUpView>();
            _view.Initialize(this);
        }

        public void StartGame(float timer) {
            _view.OnEndTimer += StartFirstTurn;
            ShowTimer(timer);
        }

        public void ShowTimer(float time) {
            _view.StartTimer(time);
        }

        private void StartFirstTurn() {
            _view.OnEndTimer -= StartFirstTurn;

            if (PhotonNetwork.IsMasterClient) {
                _serviceLocator.Resolve(out MapPhotonRPCService service);
                service.RPC_SendNextTurn();
            }
        }

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
