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
        public event Action OnSellAgent;

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

            var temp_3 = GameRoot.PrefabManager.InstantiatePrefab(MapAgentPrefabType.ChangeView, _root).GetComponent<MapAgentChangeUIView>();
            temp_3.Initialize(this);
            _views.Add(typeof(MapAgentChangeUIView).Name, temp_3);

            var temp_4 = GameRoot.PrefabManager.InstantiatePrefab(MapAgentPrefabType.CreditView, _root).GetComponent<MapAgentCreditUIView>();
            temp_4.Initialize(this);
            _views.Add(typeof(MapAgentCreditUIView).Name, temp_4);

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

        public void ShowUI(string indeficator, PlaygroundAgentChangeData data) {

            var temp = (MapAgentChangeUIView)_views[typeof(MapAgentChangeUIView).Name];

            _indeficator = indeficator;

            temp.UpdateData(data);
            temp.ShowUI();
        }

        public void ShowUI(string indeficator, PlaygroundAgentCreditData data) {

            var temp = (MapAgentCreditUIView)_views[typeof(MapAgentCreditUIView).Name];

            _indeficator = indeficator;

            temp.UpdateData(data);
            temp.ShowUI();
        }


        public void BuyAgent() {
            if (_tradeService.Trade(_indeficator)) {
                this.CloseUI();
                OnSkipAgent?.Invoke();
            }
        }

        public void RentButton() {
            this.CloseUI();

            if (_tradeService.Rent(_indeficator)) {
                OnSkipAgent?.Invoke();
                return;
            }

            OnLost?.Invoke();
        }

        public void Subscribe() {
            _tradeService.Subscribe(_indeficator);
            this.CloseUI();
            OnSkipAgent?.Invoke();
        }

        public void UnSubscribe(int percent) {
            _tradeService.UnSubscribe(_indeficator, percent);
            this.CloseUI();
            OnSkipAgent?.Invoke();
        }

        public void TakeCredit(int sizeCredit, int countRound, int debtForRound) {
            CloseUI();
            _tradeService.TakeCredit(_indeficator, sizeCredit, countRound, debtForRound);
            OnSkipAgent?.Invoke();
        }

        public void CloseCredit() {
            if(_tradeService.CloseCredit(_indeficator)) {
                this.CloseUI();
                OnSkipAgent?.Invoke();
                return;
            }

            OnLost?.Invoke();
        }

        public void SkipAgent() {
            this.CloseUI();
            OnSkipAgent?.Invoke();
        }


        public void ShowUI() { }

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
