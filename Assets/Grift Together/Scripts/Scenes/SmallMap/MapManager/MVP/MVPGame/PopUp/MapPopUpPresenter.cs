using Photon.Pun;
using System;
using UnityEngine;

namespace GriftTogether {

    public class MapPopUpPresenter : IPresenter {

        private GameObject _root;
        private ServiceLocator _serviceLocator;

        private MapPopUpView _view;
        private MapPopUpLostView _lostView;

        public event Action OnLeaveGame;

        public MapPopUpPresenter(GameObject root, ServiceLocator locator) {
            _root = root;
            _serviceLocator = locator;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.PopUp, _root).GetComponent<MapPopUpView>();
            _view.Initialize(this);

            _lostView = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.PopUpLost, _root).GetComponent<MapPopUpLostView>();
            _lostView.Initialize(this);
        }


        public void ShowTimer(float time) {
            _view.StartTimer(time);
        }


        public void StartGame(float timer) {
            _view.OnEndTimer += StartFirstTurn;
            ShowTimer(timer);
        }

        private void StartFirstTurn() {
            _view.OnEndTimer -= StartFirstTurn;

            if (PhotonNetwork.IsMasterClient) {
                _serviceLocator.Resolve(out MapPhotonRPCService service);
                service.RPC_SendNextTurn();
            }
        }


        public void LostGame() => _lostView.ShowUI();

        public void LeaveGame() {
            CloseUI();
            OnLeaveGame?.Invoke();
        }

        public void StayGame() {
            CloseUI();
            PhotonNetwork.LeaveRoom();
        }

        public void ShowUI() {
            _view.ShowUI();
        }

        public void CloseUI() {
            _view.CloseUI();
            _lostView.CloseUI();
        }

        public void Deinitialize() {

            _lostView.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_lostView.gameObject);

            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
