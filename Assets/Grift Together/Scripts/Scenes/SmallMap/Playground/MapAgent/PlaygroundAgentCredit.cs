using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentCredit : PlaygroundAgent {


        [SerializeField] private PlaygroundAgentCreditContainer _container;

        private PlaygroundTradeService _tradeService;

        private int _sizeCredit = 0;
        public int GetSizeCredit => _sizeCredit;

        private int _percentForRound;
        private int _debtForRound;

        public override void Initialize() {
            base.Initialize();
            _serviceLocator.Resolve(out _tradeService);

            _sizeCredit = 0;
            _percentForRound = 0;
            _debtForRound = 0;
        }

        public override void Activate() {

            if (_ownerIndex == PlaygroundConst.NOT_OWNER) {

                PlaygroundAgentCreditData data = new PlaygroundAgentCreditData(_container.GetNameAgent, _container.GetDescription, false, 0,
                                                                                _container.GetCountMaxCredit, _container.GetCountMinCredit, _container.GetPercentMaxCredit,
                                                                                _container.GetCountMaxRound, _container.GetCountMinRound, _container.GetPercentMaxRound);
                
                _mapManager.ShowCreditAgent(_container.GetIndeficationAgent, data);

            } else {
                PlaygroundAgentCreditData data = new PlaygroundAgentCreditData(_container.GetNameAgent, _container.GetDescription, true, _sizeCredit,
                                                                _container.GetCountMaxCredit, _container.GetCountMinCredit, _container.GetPercentMaxCredit,
                                                                _container.GetCountMaxRound, _container.GetCountMinRound, _container.GetPercentMaxRound);

                _mapManager.ShowCreditAgent(_container.GetIndeficationAgent, data);
            }
        }

        public void SetCredit(int sizeCredit, int countRound, int percentForRound) {
            _sizeCredit = sizeCredit;
            _percentForRound = percentForRound;

            _debtForRound = (int)Mathf.Round(sizeCredit / (countRound * 1.0f));
        }

        public override void Across() {
            if(_sizeCredit > 0) {
                int pay = _debtForRound + _percentForRound;
                _sizeCredit -= _debtForRound;
                _tradeService.PayForCredit(this, pay);
            }
        }

        public override void SetOwner(int indexPlayer) {
            _ownerIndex = indexPlayer;
        }

        public override void RemoveOwner(int indexPlayer) {
            if(_sizeCredit <= 0) _ownerIndex = PlaygroundConst.NOT_OWNER;
        }

        public override bool Equals(string indeficator) {
            return indeficator.Equals(_container.GetIndeficationAgent);
        }

        public override string GetName() {
            return _container.GetNameAgent;
        }
    }
}
