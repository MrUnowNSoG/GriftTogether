using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LobbyMenuUiView : MonoBehaviour, IView {

        [SerializeField] private Button _startLobbyButton;
        [SerializeField] private Button _changeDust;
        [SerializeField] private Button _leaveLobby;
        
        private LobbyMenuPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (LobbyMenuPresenter)presenter;
        }


        public void ShowUI() { }
        public void CloseUI() { }
        public void Deinitialize() { }

    }
}
