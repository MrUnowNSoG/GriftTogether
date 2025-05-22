using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapAgentChangeUIView : MonoBehaviour, IView {

        [Space(0)][Header("UI")]
        [SerializeField] private TextMeshProUGUI _nameAgentText;
        [SerializeField] private TextMeshProUGUI _changeDescriptionText;
        [SerializeField] private TextMeshProUGUI _percentGapText;


        [Space(10)][Header("Controll")]
        [SerializeField] private Button _subscribeButton;
        [SerializeField] private Button _unSubscribeButton;
        [SerializeField] private Button _skipButton;

        private MapAgentPresenter _presenter;

        private int _percent = 0;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapAgentPresenter)presenter;

            _subscribeButton.onClick.AddListener(SubscribeChange);
            _unSubscribeButton.onClick.AddListener(UnSubscribeChange);
            _skipButton.onClick.AddListener(SkipButton);

            _subscribeButton.gameObject.SetActive(false);
            _unSubscribeButton.gameObject.SetActive(false);
        }

        public void UpdateData(PlaygroundAgentChangeData data) {
            _nameAgentText.text = GameRoot.LocalizationManager.Get(data.GetName);
            _changeDescriptionText.text = GameRoot.LocalizationManager.Get(data.GetDescription);

            _percentGapText.text = GameRoot.LocalizationManager.Get(MapMessage.GAP_PRICE) + ": " + data.GetPercentGap.ToString() + "%";
            _percent = data.GetPercentGap;

            _subscribeButton.gameObject.SetActive(!data.IsSubscribe);
            _unSubscribeButton.gameObject.SetActive(data.IsSubscribe);
        }

        private void SubscribeChange() => _presenter.Subscribe();

        private void UnSubscribeChange() => _presenter.UnSubscribe(_percent);

        private void SkipButton() => _presenter.SkipAgent();

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {

            _skipButton.onClick.RemoveListener(SkipButton);
            _subscribeButton.onClick.RemoveListener(SubscribeChange);
            _unSubscribeButton.onClick.RemoveListener(UnSubscribeChange);

            GameRoot.PrefabManager.DestroyGameObject(this.gameObject);
        }
    }
}
