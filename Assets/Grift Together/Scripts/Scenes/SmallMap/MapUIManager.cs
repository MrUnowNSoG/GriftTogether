using UnityEngine;

namespace GriftTogether {

    public class MapUIManager : BaseSceneManager {

        private GameObject _mainRoot;

        private MapRootUIGameView _gameUIRoot;
        private MapMenuPresenter _menuPresenter;

        private MapSessionControllerPresenter _sessionControllerPresenter;

        public MapUIManager(GameObject main) {
            _mainRoot = main;
        }

        public override void Initialize() {
            _gameUIRoot = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameUIRoot, _mainRoot).GetComponent<MapRootUIGameView>();

            _menuPresenter = new MapMenuPresenter(_gameUIRoot.GetLeftCornerParent);
            _menuPresenter.Initialize();

            _sessionControllerPresenter = new MapSessionControllerPresenter(_mainRoot);
            _sessionControllerPresenter.Initialize();
        }

        public void StartSession() {

        }

        public void GetNextTurn(bool isPlayer) {
            if (isPlayer) {
                string message = GameRoot.LocalizationManager.Get(MapMessage.START_TURN);
                _sessionControllerPresenter.ShowUI(message);
            }
        }

        public override void Deinitialize() {
            _menuPresenter.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_gameUIRoot.gameObject);
        }
    }
}
