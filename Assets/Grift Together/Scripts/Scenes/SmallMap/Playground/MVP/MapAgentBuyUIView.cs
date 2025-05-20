using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapAgentBuyUIView : MonoBehaviour, IView {

        [Space(0)][Header("UI")]
        [SerializeField] private TextMeshProUGUI _nameAgent;
        [SerializeField] private TextMeshProUGUI _rentPrice;
        [SerializeField] private TextMeshProUGUI _descriptionAgent;
        [SerializeField] private TextMeshProUGUI _priceText;

        [Space(10)][Header("Controll")]
        [SerializeField] private Button _buyAgent;
        [SerializeField] private Button _skipAgent;

        private MapAgentPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapAgentPresenter)presenter;

            _buyAgent.onClick.AddListener(BuyAgent);
            _skipAgent.onClick.AddListener(SkipAgent);
        }

        public void UpdateData(PlaygroundAgentBuyData data) {
            _nameAgent.text = GameRoot.LocalizationManager.Get(data.GetName);
            _descriptionAgent.text = GameRoot.LocalizationManager.Get(data.GetDescription);

            _rentPrice.text = data.GetRent.ToString();
            _priceText.text = GameRoot.LocalizationManager.Get(MapMessage.BUY_BUTTON) + " " + data.GetPrice.ToString();
        }

        private void BuyAgent() {
            _presenter.BuyAgent();
        }

        private void SkipAgent() {
            _presenter.SkipAgent();
        }

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            _buyAgent.onClick.RemoveListener(BuyAgent);
            _skipAgent.onClick.RemoveListener(SkipAgent);
            GameRoot.PrefabManager.DestroyGameObject(this.gameObject);
        }
    }
}
