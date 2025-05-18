using UnityEngine;

namespace GriftTogether {

    public class MapPlayerObject : MonoBehaviour {

        private int _indexPlayer;
        public int IndexPlayer {

            set {
                if (value < 0) value = 0;
                _indexPlayer = value;
            }

            get { return _indexPlayer; }
        }

        private int _indexPosition;
        public int IndexPosition {
            
            set {
                if(value < 0) value = 0;
                _indexPosition = value;
            }

            get { return _indexPosition; }
        }
    }

}
