using System;
using UnityEngine;

namespace GriftTogether {

    public class MapAgentPresenter : IPresenter {

        private GameObject _root;
        private MapAgentBuyUIView _view;

        private string _indeficator;

        public event Action OnSkipAgent;

        public MapAgentPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapAgentPrefabType.BuyView, _root).GetComponent<MapAgentBuyUIView>();
            _view.Initialize(this);
            _view.CloseUI();
        }

        public void ShowUI(string indeficator, PlaygroundAgentBuyData data) {
            _indeficator = indeficator;
            _view.UpdateData(data);
            ShowUI();
        }

        public void BuyAgent() {

        }

        public void SkipAgent() {
            _view.CloseUI();
            OnSkipAgent?.Invoke();
        }


        public void ShowUI() => _view.ShowUI();

        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
