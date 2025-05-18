using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

namespace GriftTogether
{
    public class MapHeaderUIView : MonoBehaviour, IView {

        [SerializeField] private TextMeshProUGUI _userNameText;
        [SerializeField] private TextMeshProUGUI _countTurnText;
        [SerializeField] private TextMeshProUGUI _counCoinText;
        
        private MapPlayerObject _mapPlayerObject;
        private MapPhotonTurnService _turnService;

        public event Action OnClose;

        public void Initilize(IPresenter presenter, MapPlayerObject player, MapPhotonTurnService turnService) {
            _mapPlayerObject = player;
            _turnService = turnService;
            Initialize(presenter);
        }
        public void Initialize(IPresenter presenter) {
            _userNameText.text = PhotonNetwork.LocalPlayer.NickName;
            _mapPlayerObject.OnCoinChange += UpdateCoin;
            _turnService.OnChangeTurn += UpdateTurn;

            UpdateCoin();
            UpdateTurn();
        }

        private void UpdateCoin() => _counCoinText.text = _mapPlayerObject.GetCountCoin.ToString();
        private void UpdateTurn() => _countTurnText.text = _turnService.GetCounrTurn.ToString();


        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            _turnService.OnChangeTurn -= UpdateTurn;
            _mapPlayerObject.OnCoinChange -= UpdateCoin;
        }

    }
}

