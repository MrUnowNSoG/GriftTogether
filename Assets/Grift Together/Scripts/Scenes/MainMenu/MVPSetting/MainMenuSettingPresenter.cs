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
        public LocalizationLanguage GetLanguage => _model.Language;
        public bool GetMasterState => _model.MasterAudoState;
        public float GetSoundVolume => _model.SoundVolume;
        public float GetMusicVolume => _model.MusicVolume;


        private MainMenuSettingView _view;
        private bool _isLanguageChange = false;

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

        public void ChangeLanguage(int language) {
            _model.Language = (LocalizationLanguage)language;
            _isLanguageChange = true;
        }

        public void ChangeAudio(bool masterState, float soundVolume, float musicVolume) {
            _model.MasterAudoState = masterState;
            _model.SoundVolume = soundVolume;
            _model.MusicVolume = musicVolume;

            GameRoot.SoundManager.SetSetting(masterState, soundVolume, musicVolume);
        }

        private void BackButton() {

            if (_isLanguageChange) {
                GameRoot.ScenesManager.ShowLoadingScreen();
                GameRoot.LocalizationManager.SetLanguage(_model.Language);
            
                _isLanguageChange = false;
                _model.SavePlayerSettingToGlobal();
                
                GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.MENU_SCENE, true);
                return;
            }

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
