using UnityEngine;

namespace GriftTogether {

    public class ScenesPresenter : IPresenter {


        private ScenesModel _scenesModel;
        private ScenesView _scenesView;


        public void Initialize() {
            _scenesModel = new ScenesModel();
            _scenesView = GameRoot.PrefabManager.InstantiatePrefab(ScenesManagerPrefabType.ScenesView).GetComponent<ScenesView>();

            _scenesView.Initialize(this);
            GameObject.DontDestroyOnLoad(_scenesView.gameObject);
        }

        
        public void ShowLoadingScreen() {
            _scenesView.ShowUI();
        }

        public void HideLoadingScreen() {
            _scenesView.CloseUI();
        }

        public void ShowUI() { }

        public void CloseUI() {}

        public void Deinitialize() {
            _scenesView.Deinitialize();
        }

    }
}
