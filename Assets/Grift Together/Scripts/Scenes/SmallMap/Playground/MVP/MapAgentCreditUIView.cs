using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether
{
    public class MapAgentCreditUIView : MonoBehaviour, IView {

        [Space(0)][Header("UI")]
        [SerializeField] private TextMeshProUGUI _nameAgent;
        [SerializeField] private TextMeshProUGUI _descriptionAgent;
        [SerializeField] private TextMeshProUGUI _percentForRound;

        [Space(10)][Header("Sliders")]
        [SerializeField] private Slider _countCoinSlider;
        [SerializeField] private TextMeshProUGUI _countCoinText;
        [SerializeField] private Slider _countRoundSlider;
        [SerializeField] private TextMeshProUGUI _countRountText;

        [Space(10)][Header("Controll")]
        [SerializeField] private Button _useAgentButton;
        [SerializeField] private TextMeshProUGUI _useButtonText;
        [SerializeField] private Button _skipAgentButton;

        private MapAgentPresenter _presenter;

        PlaygroundAgentCreditData _data;
        private int _debtForRound;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapAgentPresenter)presenter;

            _useAgentButton.onClick.AddListener(UseAgent);
            _skipAgentButton.onClick.AddListener(SkipAgent);

            _countCoinSlider.onValueChanged.AddListener(UpdateCoinText);
            _countRoundSlider.onValueChanged.AddListener(UpdateRoundText);
        }

        public void UpdateData(PlaygroundAgentCreditData data) {
            _data = data;

            _nameAgent.text = GameRoot.LocalizationManager.Get(data.GetNameAgent);
            _descriptionAgent.text = GameRoot.LocalizationManager.Get(data.GetDescription);


            _countCoinSlider.minValue = data.GetCountMinCredit;
            _countCoinSlider.maxValue = data.GetCountMaxCredit;

            _countRoundSlider.minValue = data.GetCountMinRound;
            _countRoundSlider.maxValue = data.GetCountMaxRound;

            string buttonText = data.GetIsHadCredit ? 
                GameRoot.LocalizationManager.Get(PlaygroundConst.RETURN_CREDIT) + $" {data.GetSizeCredit}" 
                : GameRoot.LocalizationManager.Get(PlaygroundConst.TAKE_CREDIT);
            
            _useButtonText.text = buttonText;
        }

        
        private void UseAgent() {
            if (_data.GetIsHadCredit == false) _presenter.TakeCredit((int)_countCoinSlider.value, (int)_countRoundSlider.value, _debtForRound);
            else _presenter.CloseCredit();
        }

        private void SkipAgent() {
            _presenter.SkipAgent();
        }

        private void UpdateCoinText(float value) {
            _countCoinText.text = value.ToString();
            UpdatePercent();
        }
        private void UpdateRoundText(float value) {
            _countRountText.text = value.ToString();
            UpdatePercent();
        }

        private void UpdatePercent() {

            int coinPercent = (int)(_countCoinSlider.value * ((_data.GetPercentMaxCredit * 1.0f) / (_data.GetCountMaxCredit * 1.0f)));
            int roundPercent = (int)(_countRoundSlider.value * ((_data.GetPercentMaxRound * 1.0f) / (_data.GetCountMaxRound * 1.0f)));
            int allPercent = coinPercent + roundPercent;
            _debtForRound = (int)(_countCoinSlider.value * (allPercent / 100f));

            _percentForRound.text = $"{allPercent.ToString()}% ({_debtForRound})";
        }

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            _countCoinSlider.onValueChanged.RemoveListener(UpdateCoinText);
            _countRoundSlider.onValueChanged.RemoveListener(UpdateRoundText);

            _useAgentButton.onClick.RemoveListener(UseAgent);
            _skipAgentButton.onClick.RemoveListener(SkipAgent);

            GameRoot.PrefabManager.DestroyGameObject(this.gameObject);
        }
    }
}
