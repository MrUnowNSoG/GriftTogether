using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuPresenter : IPresenter {

        private GameObject _root;
        private GameObject _overlayRoot;

        private MainMenuView _view;

        private Dictionary<string, IPresenter> _cashPresenters;

        public MainMenuPresenter(GameObject root, GameObject overlayRoot) {
            _root = root;
            _overlayRoot = overlayRoot;
        }
        
        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.MainView, _root).GetComponent<MainMenuView>();
            _cashPresenters = new Dictionary<string, IPresenter>();
            _view.Initialize(this);
        }

        public void PlayUI() {
            if (TryShowUI<MainMenuPlayPresenter>()) return;

            MainMenuPlayPresenter presenter = new MainMenuPlayPresenter();
            presenter.Initialize();
            _cashPresenters.Add(typeof(MainMenuPlayPresenter).Name, presenter);

            TryShowUI<MainMenuPlayPresenter>();
        }

        public void SkinUI() {
            if (TryShowUI<MainMenuSkinPresenter>()) return;

            MainMenuSkinPresenter presenter = new MainMenuSkinPresenter();
            presenter.Initialize();
            _cashPresenters.Add(typeof(MainMenuSkinPresenter).Name, presenter);

            if (TryShowUI<MainMenuSkinPresenter>()) return;

        }

        public void SettingUI() {
            if (TryShowUI<MainMenuSettingPresenter>()) return;

            MainMenuSettingPresenter presenter = new MainMenuSettingPresenter(_root);
            presenter.Initialize();
            presenter.onBack += BackMainMenu;
            _cashPresenters.Add(typeof(MainMenuSettingPresenter).Name, presenter);

            TryShowUI<MainMenuSettingPresenter>();
        }

        public void ExitUI() {

            if (TryShowUI<MainMenuExitPresenter>()) {
                this.ShowUI();
                return;
            }

            MainMenuExitPresenter presenter = new MainMenuExitPresenter(_overlayRoot);
            presenter.Initialize();
            presenter.OnBack += BackMainMenu;
            _cashPresenters.Add(typeof(MainMenuExitPresenter).Name, presenter);

            TryShowUI<MainMenuExitPresenter>();
            this.ShowUI();
        }


        private bool TryShowUI<T>() where T : IPresenter {
            
            string key = typeof(T).Name;

            if(_cashPresenters.ContainsKey(key)) {
                if (_cashPresenters.TryGetValue(key, out IPresenter presenter)) {
                    
                    foreach(var item in _cashPresenters) {
                        item.Value.CloseUI();
                    }

                    presenter.ShowUI();
                    this.CloseUI();
                    return true;
                }
            }

            return false;
        }

        private void BackMainMenu() {
            foreach (var item in _cashPresenters) {
                item.Value.CloseUI();
            }

            this.ShowUI();
        }

        public void ShowUI() => _view.ShowUI();

        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            foreach (var item in _cashPresenters) {
                item.Value.Deinitialize();
            }
        }
    }

}
