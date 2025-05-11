using System;
using TMPro;
using UnityEngine;

namespace GriftTogether {


    public class MainMenuHeaderUiView : MonoBehaviour, IView {

        [SerializeField] private TextMeshProUGUI _userNameText;
        [SerializeField] private TextMeshProUGUI _countWinText;
        [SerializeField] private TextMeshProUGUI _countCoinText;


        public event Action OnClose;

        public void Initialize(IPresenter presenter) {}

        public void UpdateData(string userName, int countWin, int countCoin) {
            _userNameText.text = userName;
            _countWinText.text = countWin.ToString();
            _countCoinText.text = countCoin.ToString();
        }


        public void ShowUI() {}
        public void CloseUI() {}
        public void Deinitialize() {}

    }
}
