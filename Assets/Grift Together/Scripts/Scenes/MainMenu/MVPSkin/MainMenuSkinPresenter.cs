using System;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuSkinPresenter : IPresenter {

        private GameObject _root;
        private SkinService _skinService;

        private MainMenuSkinView _view;

        public event Action OnBack;

        public MainMenuSkinPresenter(GameObject root, SkinService skinService) {
            _root = root;
            _skinService = skinService;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.SkinView, _root).GetComponent<MainMenuSkinView>();
            _view.Initialize(this);
            _view.InitUi(_skinService.GetHatName, _skinService.GetColorName, _skinService.GetFaceName);
            _view.OnClose += Back;
        }

        public string HatChange(int direction) {
            return _skinService.ChangeSkin(new SkinHatCollection(), direction);
        }
        public string ColorChange(int direction) {
            return _skinService.ChangeSkin(new SkinColorCollection(), direction);
        }

        public string FaceChange(int direction) {
            return _skinService.ChangeSkin(new SkinFaceCollection(), direction);
        }

        private void Back() {
            _skinService.SaveCurrentSkin();
            OnBack?.Invoke();
        }

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.gameObject.SetActive(false);

        public void Deinitialize() {
            _view.OnClose -= Back;
            _view.Deinitialize();

            OnBack = null;

            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }

    }
}
