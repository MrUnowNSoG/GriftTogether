using Photon.Pun;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundTradeService : IService {

        private MapPhotonRPCService _rpcService;
        private AnalyticsService _analyticsService;

        private MapPlayerObject _player;
        private List<PlaygroundAgent> _agents;

        public PlaygroundTradeService(ServiceLocator serviceLocator, MapPlayerObject player, List<PlaygroundAgent> agent) {
            serviceLocator.Resolve(out _rpcService);
            serviceLocator.Resolve(out _analyticsService);

            _player = player;
            _agents = agent;
        }

        public bool Trade(string indeficator) {

            PlaygroundAgent playgroundAgent = null;

            foreach(var agent in _agents) {
                if(agent.Equals(indeficator)) playgroundAgent = agent;
            }

            if (playgroundAgent == null) return false;
            if (playgroundAgent.GetOwner == PlaygroundConst.NOT_OWNER) return Buy(playgroundAgent, indeficator);
            if (playgroundAgent.GetOwner == _player.GetIndexPlayer) return Upgrade(playgroundAgent);  

            return false;
        }

        private bool Buy(PlaygroundAgent agent, string indeficator) {

            int price = agent.GetPrice(0);
            int coint = _player.GetCountCoin;

            if(coint - price >= 0) {

                _player.Trade(price);

                string message = PhotonNetwork.LocalPlayer.NickName + " "
                    + GameRoot.LocalizationManager.Get(MapMessage.PURCHASED_BUILD) + " "
                    + GameRoot.LocalizationManager.Get(agent.GetName()) + "!";

                _rpcService.RPC_BuyBuild(message, indeficator, _player.GetIndexPlayer);

                _analyticsService.SendBuildStat(indeficator);
                return true;
            }

            return false;
        }

        private bool Upgrade(PlaygroundAgent agent) {
            return false;
        }


        public bool Rent(string indeficator) {
            PlaygroundAgent playgroundAgent = null;

            foreach (var agent in _agents) {
                if (agent.Equals(indeficator)) playgroundAgent = agent;
            }

            int rent = playgroundAgent.GetRent();
            int coin = _player.GetCountCoin;

            if (coin - rent >= 0) {

                _player.Trade(rent);

                string message = PhotonNetwork.LocalPlayer.NickName + " "
                    + GameRoot.LocalizationManager.Get(MapMessage.PAID_RENT) + " "
                    + GameRoot.LocalizationManager.Get(playgroundAgent.GetRentReason()) + "!";

                _rpcService.RPC_Rent(message, indeficator);

                return true;  
            }

            return false;
        }


        public void Subscribe(string indeficator) {

            PlaygroundAgent playgroundAgent = null;

            foreach (var agent in _agents) {
                if (agent.Equals(indeficator)) playgroundAgent = agent;
            }

            if(playgroundAgent == null) {
                Debug.LogError($"Trade service: Can't find playground agent with indeficator: {indeficator}!");
                return;
            }

            string message = $"{PhotonNetwork.LocalPlayer.NickName} {GameRoot.LocalizationManager.Get(MapMessage.SUBSCRIBE_FOR)} {GameRoot.LocalizationManager.Get(playgroundAgent.GetName())}!";

            _rpcService.RPC_BuyBuild(message, indeficator, _player.GetIndexPlayer);

            _analyticsService.SendBuildStat(indeficator);
        }

        public void UnSubscribe(string indeficator, int percent) {

            PlaygroundAgent playgroundAgent = null;

            foreach (var agent in _agents) {
                if (agent.Equals(indeficator)) playgroundAgent = agent;
            }

            if (playgroundAgent == null) {
                Debug.LogError($"Trade service: Can't find playground agent with indeficator: {indeficator}!");
                return;
            }

            int plauerCoin = _player.GetCountCoin;
            int newCoin = (int)(plauerCoin * (percent / 100f));
            _player.Trade(newCoin);

            string message = PhotonNetwork.LocalPlayer.NickName + " "
                + GameRoot.LocalizationManager.Get(MapMessage.UN_SUBSCRIBE_FOR) + " "
                + GameRoot.LocalizationManager.Get(playgroundAgent.GetName()) + "!";

            _rpcService.RPC_RemoveBuild(message, indeficator, _player.GetIndexPlayer);

            _analyticsService.SendBuildStat(indeficator);
        }
    }
}
