using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentBaseBuild : PlaygroundAgent {

        [SerializeField] private PlaygroundAgentContainer _container;

        public override void Activate() {
            base.Activate();

            if(_ownerIndex < 0) {

                PlaygroundAgentBuyData data = new PlaygroundAgentBuyData(_container.GetNameAgent,
                                                                         _container.GetDescriptionAgent,
                                                                         _container.GetRentPrice,
                                                                         _container.GetPriceAgent);
                _mapManager.ShowMapAgent(_container.GetIndeficationAgent, data);
            }

        }

    }
}
