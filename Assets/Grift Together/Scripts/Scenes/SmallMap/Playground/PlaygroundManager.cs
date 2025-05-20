using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundManager : MonoBehaviour {

        [Space(0)][Header("Parts")]
        [SerializeField] private List<PlaygroundAgent> _agents;

        private MapManager _mapManager;
        private MapPlayerObject _currentPlayer;
        private ServiceLocator _serviceLocator;

        private float _stepDuration = 1f;

        public void Initialize(MapManager manager, MapPlayerObject player, ServiceLocator serviceLocator) {
            _mapManager = manager;
            _currentPlayer = player;
            _serviceLocator = serviceLocator;

            PlaygroundTradeService playgroundTradeService = new PlaygroundTradeService(serviceLocator, player, _agents);
            serviceLocator.AddService(playgroundTradeService);

            Initialize();
        }
        public void Initialize() {
            foreach (var agent in _agents) {
                agent.Initialize(_mapManager, _currentPlayer, _serviceLocator);
            }
        }


        public Vector3 GetStartRoundPos(int indexPlayer) {
            return _agents[0].GetPos(indexPlayer);
        }

        public void MovePlayer(int count) {
            StopAllCoroutines();
            StartCoroutine(MovePlayerAnimation(count));
        }

        private IEnumerator MovePlayerAnimation(int count) {
            
            int totalAgents = _agents.Count;

            for (int i = 0; i < count; i++) {

                int nextIndex = (_currentPlayer.IndexPosition + 1) % totalAgents;

                Vector3 startPos = _currentPlayer.transform.position;
                Vector3 endPos = _agents[nextIndex].GetPos(_currentPlayer.GetIndexPlayer);

                Vector3 dir = (endPos - startPos).normalized;
                _currentPlayer.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

                float time = 0f;
                while (time <= _stepDuration) {
                    time += Time.fixedDeltaTime;

                    float t = time / _stepDuration;
                    _currentPlayer.transform.position = Vector3.Lerp(startPos, endPos, t);
                    
                    yield return new WaitForFixedUpdate();
                }

                _currentPlayer.transform.position = endPos;
                _currentPlayer.IndexPosition = nextIndex;

                _agents[nextIndex].Across();
            }

            _agents[_currentPlayer.IndexPosition].Activate();
        }

        public void BuyBuild(string indeficator, int indexPlayer) {
            foreach (var agent in _agents) {
                if (agent.Equals(indeficator)) agent.SetOwner(indexPlayer);
            }
        }

        public void PayRent(string indeficator) {
            PlaygroundAgent current = null;

            foreach (var agent in _agents) {
                if (agent.Equals(indeficator)) {
                    current = agent;
                    return;
                }
            }

            if(current != null && current.GetOwner == _currentPlayer.GetIndexPlayer) {
                _currentPlayer.AddGold(current.GetRent());
            }
        }

        public  void Deinitialize() {
           
        }
    }
}
