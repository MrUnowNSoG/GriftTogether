using System;
using UnityEngine;

namespace GriftTogether {

    public class MapPlayerObject : MonoBehaviour {

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


        public void Initizlize(int indexPlayer) {
            _indexPlayer = indexPlayer;
            _indexPosition = 0;
            _countCoin = 1500;
        }

        public void Trade() {
            OnCoinChange?.Invoke();
        }
    }

}
