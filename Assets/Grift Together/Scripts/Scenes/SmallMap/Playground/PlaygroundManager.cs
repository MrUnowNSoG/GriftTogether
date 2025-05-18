using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundManager : MonoBehaviour {

        [Space(0)][Header("Parts")]
        [SerializeField] private List<PlaygroundAgent> _agents;

        
        private MapPlayerObject _currentPlayer;
        public void SetCurrentPlayer(MapPlayerObject player) => _currentPlayer = player;

        private float _stepDuration = 1f;

        private MapManager _mapManager;

        public void Initialize(MapManager manager) {
            _mapManager = manager;
            Initialize();
        }

        public void Initialize() {}


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
                Vector3 endPos = _agents[nextIndex].GetPos(_currentPlayer.IndexPlayer);

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
            }

            _mapManager.StartTurnProcess();
        }

        public  void Deinitialize() {
           
        }
    }
}
