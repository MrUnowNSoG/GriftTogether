using System.Collections.Generic;
using UnityEngine;


namespace GriftTogether {

    public class PlaygroundAgentChange : PlaygroundAgent {

        [SerializeField] private PlaygroundAgentSubscibeContainer _container;
        [SerializeField] private AnimationCurve _maxMultiplier;
        [SerializeField] private AnimationCurve _minMultiplier;

        private List<int> _subscribePlayer;
        [SerializeField] private float _oldMultiplier;

        public override void Initialize() {
            base.Initialize();
            _subscribePlayer = new List<int>();
            _oldMultiplier = 0;
        }

        public override void Activate() {
            base.Activate();

            int playerIndex = _currentPlayer.GetIndexPlayer;
            bool subscibe = _subscribePlayer.Contains(playerIndex);

            PlaygroundAgentChangeData data = new PlaygroundAgentChangeData(_container.GetNameAgent, _container.GetDescription, 
                                                                           subscibe, _container.GetPriceGap);

            _mapManager.ShowChangeAgent(_container.GetIndeficationAgent, data);
        }

        public override void Across() {

            if (_subscribePlayer.Contains(_currentPlayer.GetIndexPlayer)) {

                float maxValue = _maxMultiplier.Evaluate(_subscribePlayer.Count);
                float minValue = _minMultiplier.Evaluate(_subscribePlayer.Count);

                float newMultiplay = Random.Range(minValue, maxValue);

                int newCoin = 0;

                if (_oldMultiplier > 0) {
                    float coinPlayer = _currentPlayer.GetCountCoin / _oldMultiplier;
                    newCoin = (int)(coinPlayer * newMultiplay);
                    
                } else {
                    newCoin = (int)(_currentPlayer.GetCountCoin * newMultiplay);
                }

                _currentPlayer.SetGold(newCoin);
                _oldMultiplier = newMultiplay;
            }
        }

        public override void SetOwner(int indexPlayer) {

            if(_subscribePlayer.Contains(indexPlayer)) {
                Debug.LogError("PlaygroundAgentChange: Second suscribe!");
                return;
            }

            _subscribePlayer.Add(indexPlayer);
            Across();
        }

        public override void RemoveOwner(int indexPlayer) {
            if (_subscribePlayer.Contains(indexPlayer)) {
                
                _subscribePlayer.RemoveAt(indexPlayer);
                Across();
            }
        }

        public override bool Equals(string indeficator) {
            return indeficator.Equals(_container.GetIndeficationAgent);
        }

        public override string GetName() {
            return _container.GetNameAgent;
        }
    }
}
