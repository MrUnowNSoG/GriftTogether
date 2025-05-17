using System;
using UnityEditor.VersionControl;
using UnityEngine;

namespace GriftTogether {

    public class MapSessionControllerPresenter : IPresenter {

        private GameObject _root;
        private MapSessionControllerUIView _view;

        private bool _isTurnStage = false;

        public event Action OnStartTurn;
        public event Action OnEntTurn;

        public MapSessionControllerPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.SessionTurnView, _root).GetComponent<MapSessionControllerUIView>();
            _view.Initialize(this);
        }

        public void TurnButton() {

            if(_isTurnStage) {
                string message = GameRoot.LocalizationManager.Get(MapMessage.END_TURN);
                _view.UpdateTurnMessage(message);

                _isTurnStage = false;

                OnStartTurn?.Invoke();
                return;
            }

            _view.UpdateTurnMessage(string.Empty);
        }

        public void ShowUI(string message, bool startTurn = false) {
            _view.UpdateTurnMessage(message);
            _view.ShowUI();
            _isTurnStage = startTurn;
        }

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
