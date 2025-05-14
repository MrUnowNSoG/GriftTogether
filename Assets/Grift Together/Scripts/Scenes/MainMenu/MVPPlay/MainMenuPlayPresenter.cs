using System;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuPlayPresenter : IPresenter {

        private GameObject _root;
        private TextValidatorService _textValidatorService;

        private MainMenuPlayView _view;

        public event Action OnBack;

        public MainMenuPlayPresenter(GameObject root) {
            _root = root;
            GameRoot.ServiceLocator.Resolve(out _textValidatorService);
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.PlayView, _root).GetComponent<MainMenuPlayView>();
            _view.Initialize(this);

            _view.OnClose += Back;
        }

        public void CreateLobby() {
            GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.LOBBY_SCENE, true);
        }

        public string ConnectToLobby(string code) {
            if(_textValidatorService.ValidationText(code, TextValidatorType.LobbyCode)) {
                //TODO : add connect to server
                return string.Empty;
            }

            return _textValidatorService.RuleValidationText(TextValidatorType.LobbyCode);
        }

        private void Back() {
            OnBack?.Invoke();
        }

        public void ShowUI() => _view.ShowUI();

        public void CloseUI() {
            _view.gameObject.SetActive(false);
        }

        public void Deinitialize() {
            
            OnBack = null;

            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }

    }
}
