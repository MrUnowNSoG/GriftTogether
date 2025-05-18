using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgent : MonoBehaviour {

        [SerializeField] protected PlaygroundSpawnCollection _spawns;
        public Vector3 GetPos(int indexPlayer) =>_spawns.GetPos(indexPlayer);

        protected MapManager _mapManager;
        protected MapPlayerObject _currentPlayer;
        protected ServiceLocator _serviceLocator;

        public void Initialize(MapManager manager, MapPlayerObject player, ServiceLocator serviceLocator) {
            _mapManager = manager;
            _currentPlayer = player;
            _serviceLocator = serviceLocator;
            
            InitializeService();
        }

        public virtual void InitializeService() {

        }

        public virtual void Across() {

        }

    }
}
