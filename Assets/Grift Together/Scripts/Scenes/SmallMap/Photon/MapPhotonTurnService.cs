using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class MapPhotonTurnService : MonoBehaviourPunCallbacks, IService {

        private ServiceLocator _serviceLocator;

        private List<Player> _turnOrder = new List<Player>();
        private int _currentPlayerIndex = -1;
        private Player _currentPlayer;

        private int _countTurn = 0;
        public int GetCounrTurn => _countTurn;

        public event Action OnChangeTurn;

        public void Init(ServiceLocator serviceLocator) {
            _serviceLocator = serviceLocator;
            _countTurn = 0;
            BuildTurnOrder();
        }

        private void BuildTurnOrder() {
            _turnOrder = new List<Player>(PhotonNetwork.PlayerList);
            _turnOrder.Sort((a, b) => a.ActorNumber.CompareTo(b.ActorNumber));
        }

        public void NextTurn() {
            _countTurn++;

            _currentPlayerIndex++;
            if(_currentPlayerIndex >= _turnOrder.Count) _currentPlayerIndex = 0;
            if (_currentPlayerIndex < 0) _currentPlayerIndex = 0;

            _currentPlayer = _turnOrder[_currentPlayerIndex];

            OnChangeTurn?.Invoke();
        }

        public bool IsTurnPlayer() {
            return PhotonNetwork.LocalPlayer == _currentPlayer;
        }

        public string GetCurrentName() {
            return _currentPlayer.NickName;
        }

        public override void OnPlayerLeftRoom(Player other) {
            Player current = _turnOrder[_currentPlayerIndex];
            BuildTurnOrder();

            if (current == other) {
                _currentPlayerIndex--;
                _serviceLocator.Resolve(out MapPhotonRPCService service);
                service.RPC_SendNextTurn();
            }
        }
    }
}
