using Photon.Pun;
using UnityEngine;

namespace GriftTogether {
    public class MapManager : BaseSceneManager {

        private const float TIME_BEFORE_START = 5f;

        private GameObject _mainCanvas;
        private GameObject _overlayCanvas;
        
        private PlaygroundManager _playgroundManager;

        private ServiceLocator _serviceLocator;

        private MapPlayerObject _mapPlayerObject;

        private MapUIManager _mapUIManager;
        private MapGameManager _mapGameManager;


        public MapManager(GameObject main, GameObject overlay, PlaygroundManager playgroundManager, ServiceLocator serviceLocator) {
            _mainCanvas = main;
            _overlayCanvas = overlay;
            _playgroundManager = playgroundManager;

            _serviceLocator = serviceLocator;
        }

        public void Initialize(MapPlayerObject mapPlayer) {
            _mapPlayerObject = mapPlayer;
            Initialize();
        }
        public override void Initialize() {
            _mapUIManager = new MapUIManager(this, _mainCanvas, _overlayCanvas, _mapPlayerObject, _serviceLocator);
            _mapGameManager = new MapGameManager(this, _playgroundManager, _serviceLocator);

            _mapUIManager.Initialize();
            _mapGameManager.Initialize();
        }

        
        //RPC
        public void StartGame() {
            _mapUIManager.StartGame(TIME_BEFORE_START);
        }

        public void GetNextTurn(bool isPlayer) => _mapGameManager.GetNextTurn(isPlayer);

        //
        public void StartTurnProcess() => _mapGameManager.StartTurnProcess();
        public void StopTurnProcess(string message) => _mapUIManager.StopTurnProcess(message);

        public void ShowMapAgent(string indefictor, PlaygroundAgentBuyData data) => _mapUIManager.ShowMapAgent(indefictor, data);
        public void SkipMapAgent() => _mapGameManager.SkipMapAgent();


        public override void Deinitialize() {
            _mapUIManager.Deinitialize();
            _mapGameManager.Deinitialize();
        }
    
    }
}
