using System;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuExitPresenter : IPresenter {

        private GameObject _root;
        private ConfirmUIView _view;

        public event Action OnBack;

        public MainMenuExitPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(UniversalPrefabType.ConfirmView, _root).GetComponent<ConfirmUIView>();
            _view.Initialize(this);
            _view.OnConfirm += ExitGame;
            _view.OnDecline += Decline;
        }


        private void Decline() {
            OnBack?.Invoke();
        }

        private void ExitGame() {
            GameRoot.ScenesManager.ShowLoadingScreen();
            GameRoot.PlayerGlobalManager.SavePlayerServerData();
            GameRoot.PlayerGlobalManager.SavePlayerSetting();

            Deinitialize();
            Application.Quit();
        }
            
        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.OnConfirm -= ExitGame;
            _view.OnDecline -= Decline;

            OnBack = null;

            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
