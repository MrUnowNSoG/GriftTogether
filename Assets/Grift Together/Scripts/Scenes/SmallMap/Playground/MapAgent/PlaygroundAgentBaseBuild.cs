using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentBaseBuild : PlaygroundAgent {

        [SerializeField] private PlaygroundAgentContainer _container;
        [SerializeField] private GameObject _buyGO;

        public override void Initialize() {
            base.Initialize();
            _buyGO.gameObject.SetActive(false);
        }

        public override void Activate() {
            base.Activate();

            if(_ownerIndex == PlaygroundConst.NOT_OWNER) {

                PlaygroundAgentBuyData data = new PlaygroundAgentBuyData(_container.GetNameAgent,
                                                                         _container.GetBuyDescription,
                                                                         _container.GetRentPrice,
                                                                         _container.GetPriceAgent);
                _mapManager.ShowBuyAgent(_container.GetIndeficationAgent, data);

            } else if(_ownerIndex == _currentPlayer.GetIndexPlayer) {

                Debug.LogWarning("Upgrade window!");

                PlaygroundAgentRentData data = new PlaygroundAgentRentData(_container.GetNameAgent,
                                                                           _container.GetRentDescription,
                                                                           _container.GetRentPrice);

                _mapManager.ShowRentAgent(_container.GetIndeficationAgent, data);

                // _mapManager.SkipMapAgent();

            } else {

                PlaygroundAgentRentData data = new PlaygroundAgentRentData(_container.GetNameAgent,
                                                                           _container.GetRentDescription,
                                                                           _container.GetRentPrice);

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
