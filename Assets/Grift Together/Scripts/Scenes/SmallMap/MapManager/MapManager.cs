using UnityEngine;

namespace GriftTogether {
    public class MapManager : BaseSceneManager {

        public GameObject _mainCanvas;
        private PlaygroundManager _playgroundManager;

        private ServiceLocator _serviceLocator;

        private MapUIManager _mapUIManager;
        private MapGameManager _mapGameManager;

        public MapManager(GameObject main, PlaygroundManager playgroundManager, ServiceLocator serviceLocator) {
            _mainCanvas = main;
            _playgroundManager = playgroundManager;

            _serviceLocator = serviceLocator;
        }

        public override void Initialize() {
            _mapUIManager = new MapUIManager(this, _mainCanvas);
            _mapGameManager = new MapGameManager(this, _playgroundManager, _serviceLocator);

            _mapUIManager.Initialize();
            _mapGameManager.Initialize();
        }


        //RPC
        public void GetNextTurn(bool isPlayer) => _mapGameManager.GetNextTurn(isPlayer);

        //
        public void StartTurnProcess() => _mapGameManager.StartTurnProcess();
        public void StopTurnProcess(string message) => _mapUIManager.StopTurnProcess(message);


        public override void Deinitialize() {
            _mapUIManager.Deinitialize();
            _mapGameManager.Deinitialize();
        }
    
    }
}
