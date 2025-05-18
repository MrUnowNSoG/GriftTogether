using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentStart : PlaygroundAgent {

        private const int COUNT_GOLD_FOR_ROUND = 400;

        private MapPhotonRPCService _rpcServic;

        public override void InitializeService() {
            base.InitializeService();
            _serviceLocator.Resolve(out  _rpcServic);
        }

        public override void Across() {
            base.Across();
            _currentPlayer.AddGold(COUNT_GOLD_FOR_ROUND);

            string message = PhotonNetwork.LocalPlayer.NickName + " "
                + GameRoot.LocalizationManager.Get(MapMessage.COMPLETE_ROUND)
                + $"\n +{COUNT_GOLD_FOR_ROUND}";

            _rpcServic.RPC_SendLog(message);
        }

    }
}
