using ExitGames.Client.Photon.StructWrapping;
using System;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuSettingPresenter : IPresenter {

        private GameObject _root;

        private ResolutionScreenService _resolutionScreenService;
        public ResolutionScreenService GetScreenService => _resolutionScreenService;


        private MainMenuSettingModel _model;
        public int GetScreenType => _model.TypeScreen;
        public string GetSreenResolution => _model.TypeResolution;

        private MainMenuSettingView _view;
        
        public event Action onBack;

        public MainMenuSettingPresenter(GameObject root) {
            _root = root;

            if(GameRoot.ServiceLocator.Resolve(out _resolutionScreenService) == false) {
                _resolutionScreenService = new ResolutionScreenService();
                Debug.LogError("GameRoot: Service locator can't resolve: Resolution Screen server!");
            }
        }

        public void Initialize() {
            _model = new MainMenuSettingModel();
            _model.Init();

            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.SettingView, _root.gameObject).GetComponent<MainMenuSettingView>();
            _view.Initialize(this);

            _view.OnClose += BackButton;
        }
        
        public void ChangeScreenValue(int screenType, string screenResolution) {
            _model.TypeScreen = screenType;
            _model.TypeResolution = screenResolution;

            _resolutionScreenService.SetScreenType((FullScreenMode)_model.TypeScreen);
            _resolutionScreenService.TrySetScreenSize(_model.TypeResolution);
        }

        private void BackButton() {
            _model.SavePlayerSettingToGlobal();
            onBack?.Invoke();
        }




        public void ShowUI() => _view.ShowUI();

        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.OnClose -= BackButton;
            _view.Deinitialize();

            onBack = null;
        }
    }
}
