using UnityEngine;

namespace GriftTogether
{
    public class PlaygroundAgenMultiplayRent : PlaygroundAgent {

        [Space(0)][Header("Setting")]
        [SerializeField] private PlaygroundAgentContainer _container;
        [SerializeField] private int _frequencyMultiRent;
        [SerializeField] private int _multiRent;

        [Space(0)][Header("Visual")]
        [SerializeField] private GameObject _buyGO;
        [SerializeField] private GameObject _multiRentGO;

        private int _countRent = 0;

        public override void Initialize() {
            base.Initialize();
            _buyGO.gameObject.SetActive(false);
            _multiRentGO.gameObject.SetActive(false);
        }

        public override void Activate() {
            base.Activate();

            if (_ownerIndex == PlaygroundConst.NOT_OWNER) {

                PlaygroundAgentBuyData data = new PlaygroundAgentBuyData(_container.GetNameAgent,
                                                                         _container.GetBuyDescription,
                                                                         _container.GetRentPrice,
                                                                         _container.GetPriceAgent,
                                                                         false);
                _mapManager.ShowBuyAgent(_container.GetIndeficationAgent, data);

            } else if (_ownerIndex == _currentPlayer.GetIndexPlayer) {

                PlaygroundAgentBuyData data = new PlaygroundAgentBuyData(_container.GetNameAgent,
                                                                         _container.GetBuyDescription,
                                                                         _container.GetRentPrice,
                                                                         _container.GetPriceAgent,
                                                                         true);
                _mapManager.ShowBuyAgent(_container.GetIndeficationAgent, data);

            } else {

                int multiRent = 1;
                if(_countRent != 0 && _countRent % _frequencyMultiRent == 0) multiRent = _multiRent;

                PlaygroundAgentRentData data = new PlaygroundAgentRentData(_container.GetNameAgent,
                                                                           _container.GetRentDescription,
                                                                           _container.GetRentPrice * multiRent);

                _countRent++;
                if(_countRent % _frequencyMultiRent == 0) _multiRentGO.gameObject.SetActive(true);
                else _multiRentGO.gameObject.SetActive(false);

                _mapManager.ShowRentAgent(_container.GetIndeficationAgent, data);
            }

        }

        public override void SetOwner(int indexPlayer) {
            _ownerIndex = indexPlayer;
            _buyGO.gameObject.SetActive(true);
        }

        public override bool Equals(string indeficator) {
            return indeficator.Equals(_container.GetIndeficationAgent);
        }

        public override int GetPrice(int lvlUpgrade) {
            return _container.GetPriceAgent;
        }

        public override int GetRent() {
            return _container.GetRentPrice;
        }

        public override string GetName() {
            return _container.name;
        }
    }
}
