using System;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class MapAgentPresenter : IPresenter {

        private GameObject _root;
        private PlaygroundTradeService _tradeService;

        private Dictionary<string, IView> _views;

        private string _indeficator;

        public event Action OnSkipAgent;
        public event Action OnLost;

        public MapAgentPresenter(GameObject root, ServiceLocator serviceLocator) {
            _root = root;
            serviceLocator.Resolve(out _tradeService);
        }

        public void Initialize() {

            _views = new Dictionary<string, IView>();

            var temp = GameRoot.PrefabManager.InstantiatePrefab(MapAgentPrefabType.BuyView, _root).GetComponent<MapAgentBuyUIView>();
            temp.Initialize(this);
            _views.Add(typeof(MapAgentBuyUIView).Name, temp);

            var temp_2 = GameRoot.PrefabManager.InstantiatePrefab(MapAgentPrefabType.RentView, _root).GetComponent<MapAgentRentUIView>();
            temp_2.Initialize(this);
            _views.Add(typeof(MapAgentRentUIView).Name, temp_2);

            this.CloseUI();
        }



        public void ShowUI(string indeficator, PlaygroundAgentBuyData data) {

            var temp = (MapAgentBuyUIView)_views[typeof(MapAgentBuyUIView).Name];

            _indeficator = indeficator;

            temp.UpdateData(data);
            temp.ShowUI();
        }

        public void ShowUI(string indeficator, PlaygroundAgentRentData data) {

            var temp = (MapAgentRentUIView)_views[typeof(MapAgentRentUIView).Name];

            _indeficator = indeficator;

            temp.UpdateData(data);
            temp.ShowUI();
        }

        public void ShowUI() { }

        public void BuyAgent() {
            if (_tradeService.Trade(_indeficator)) {
                this.CloseUI();
                OnSkipAgent?.Invoke();
            }
        }

        public void SkipAgent() {
            this.CloseUI();
            OnSkipAgent?.Invoke();
        }

        public void RentButton() {
            this.CloseUI();

            if (_tradeService.Rent(_indeficator)) {
                OnSkipAgent?.Invoke();
                return;
            }

            OnLost?.Invoke();
        }

        public void CloseUI() {
            foreach(var item in _views.Values) { 
                item.CloseUI();
            }
        }

        public void Deinitialize() {
            this.CloseUI();
            foreach (var item in _views.Values) { item.Deinitialize(); }
        }
    }
}
