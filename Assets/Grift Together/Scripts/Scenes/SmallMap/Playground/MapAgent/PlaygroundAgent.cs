using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgent : MonoBehaviour {

        [SerializeField] protected PlaygroundSpawnCollection _spawns;
        public Vector3 GetPos(int indexPlayer) =>_spawns.GetPos(indexPlayer);

        protected MapManager _mapManager;
        protected MapPlayerObject _currentPlayer;
        protected ServiceLocator _serviceLocator;

        protected int _ownerIndex;
        public int GetOwner => _ownerIndex;

        public void Initialize(MapManager manager, MapPlayerObject player, ServiceLocator serviceLocator) {
            _mapManager = manager;
            _currentPlayer = player;
            _serviceLocator = serviceLocator;
            
            _ownerIndex = PlaygroundConst.NOT_OWNER;

            Initialize();
        }

        public virtual void Initialize() {}

        public virtual void Activate() {}

        public virtual void Across() {}

        public virtual void SetOwner(int indexPlayer) {
            _ownerIndex = indexPlayer;
        }

        public virtual bool Equals(string indeficator) {
            return false;
        }

        public virtual int GetPrice(int lvlUpgrade) {
            return 0;
        }

        public virtual int GetRent() {
            return 0;
        }

        public virtual string GetRentReason() {
            return string.Empty;
        }

        public virtual string GetName() {
            return string.Empty;
        }
    }
}
