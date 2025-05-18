using Photon.Pun;

namespace GriftTogether {

    public class MapGameManager : BaseSceneManager {

        private MapManager _mapManager;
        private PlaygroundManager _playgroundManager;

        private ServiceLocator _serviceLocator;
        private DustService _dustService;
        private MapPhotonRPCService _rpcService;

        private MapPlayerTurnStage _currentStage;

        public MapGameManager(MapManager mapManager, PlaygroundManager playgroundManager, ServiceLocator serviceLocator) {
            _mapManager = mapManager;
            _playgroundManager = playgroundManager;
            _serviceLocator = serviceLocator;
        }

        public override void Initialize() {
            _serviceLocator.Resolve(out _dustService);
            _serviceLocator.Resolve(out _rpcService);
            _currentStage = MapPlayerTurnStage.WaitTurn;
        }

        public void GetNextTurn(bool isPlayer) {

            _currentStage = MapPlayerTurnStage.WaitTurn;
            if (isPlayer) _currentStage = MapPlayerTurnStage.StartTurn;

            _mapManager.StopTurnProcess(StageMessage());
        }

        public void StartTurnProcess() {

            if (_currentStage == MapPlayerTurnStage.StartTurn) {
                _currentStage = MapPlayerTurnStage.ProcessingTurn;

                int countMove = _dustService.GenerateDustStep();
                string message = PhotonNetwork.LocalPlayer.NickName + GameRoot.LocalizationManager.Get(MapMessage.SPAWN_DUST) + countMove.ToString() + "!";
                _rpcService.RPC_GetLog(message);

                _mapManager.StopTurnProcess(StageMessage());
                _playgroundManager.MovePlayer(countMove);
                return;
            }

            if (_currentStage == MapPlayerTurnStage.ProcessingTurn) {
                _currentStage = MapPlayerTurnStage.EndTurn;
                _mapManager.StopTurnProcess(StageMessage());
                return;
            }

            if(_currentStage == MapPlayerTurnStage.EndTurn) {
                _rpcService.RPC_SendNextTurn();
            }

            _mapManager.StopTurnProcess(StageMessage());
        }

        private string StageMessage() {
            
            string message = string.Empty;

            switch(_currentStage) {
                case MapPlayerTurnStage.StartTurn:
                    message = MapMessage.START_TURN;
                    break;

                case MapPlayerTurnStage.ProcessingTurn:
                    message = MapMessage.PROCESS_TURN;
                    break;

                case MapPlayerTurnStage.EndTurn:
                    message = MapMessage.END_TURN;
                    break;

                case MapPlayerTurnStage.WaitTurn:
                    message = MapMessage.WAIT_TURN;
                    break;

                default: 
                    return message;
            }

            return GameRoot.LocalizationManager.Get(message);
        }

        public override void Deinitialize() {
        
        }
    }
}
