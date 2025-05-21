using Photon.Pun;
using System;
using UnityEngine;

namespace GriftTogether {

    public class MapPlayerObject : MonoBehaviour {

        private const int START_GOLD = 10;

        private int _indexPlayer;
        public int GetIndexPlayer => _indexPlayer;

        private int _indexPosition;
        public int IndexPosition {  
            set {
                if (value < 0) value = 0;
                _indexPosition = value; 
            }

            get { return _indexPosition; } 
        }

        private int _countCoin;
        public int GetCountCoin => _countCoin;

        public event Action OnCoinChange;

        private void Start() {

            Initizlize();

            MapPhotonRPCContext context = GameRoot.PhotonManager.CurrentPhotonContext as MapPhotonRPCContext;

            if (context != null) {
                context.GetCameraSwitcher.AddTarget(_indexPlayer, this.gameObject);
            } else {
                Debug.LogError("MapPlayerObject: Can't research MapPhotonRPCContext!");
                return;
            }
        }

        public void Initizlize() {

            int actorNumber = gameObject.GetComponent<PhotonView>().OwnerActorNr;
            _indexPlayer = Array.FindIndex(PhotonNetwork.PlayerList, p => p.ActorNumber == actorNumber);
            _indexPosition = 0;
            AddGold(START_GOLD);

        }

        public void AddGold(int coin) {
            _countCoin += coin;
            OnCoinChange?.Invoke();
        }

        public void Trade(int price) {
            _countCoin -= price;
            OnCoinChange?.Invoke();
        }
    }

}
