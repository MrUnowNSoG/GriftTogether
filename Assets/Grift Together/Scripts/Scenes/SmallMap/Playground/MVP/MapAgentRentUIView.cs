using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapAgentRentUIView : MonoBehaviour, IView {

        [Space(0)][Header("UI")]
        [SerializeField] private TextMeshProUGUI _nameAgent;
        [SerializeField] private TextMeshProUGUI _rentDescription;
        [SerializeField] private TextMeshProUGUI _rentPrice;

        [Space(10)][Header("Controll")]
        [SerializeField] private Button _rentButton;

        private MapAgentPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapAgentPresenter)presenter;

            _rentButton.onClick.AddListener(RentButton);
        }

        public void UpdateData(PlaygroundAgentRentData data) {
            _nameAgent.text = GameRoot.LocalizationManager.Get(data.GetName);
            _rentDescription.text = GameRoot.LocalizationManager.Get(data.GetRentDescription);

            _rentPrice.text = GameRoot.LocalizationManager.Get(MapMessage.RENT_BUTTON) + ": " + data.GetRent.ToString();
        }

        private void RentButton() {
            _presenter.RentButton();
        }

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            _rentButton.onClick.RemoveListener(RentButton);
            GameRoot.PrefabManager.DestroyGameObject(this.gameObject);
        }
    }
}
