using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapAgentChangeUIView : MonoBehaviour, IView {

        [Space(0)][Header("UI")]
        [SerializeField] private TextMeshProUGUI _nameAgent;
        [SerializeField] private TextMeshProUGUI _changeDescription;
        [SerializeField] private TextMeshProUGUI _changeGap;


        [Space(10)][Header("Controll")]
        [SerializeField] private Button _subscribeButton;
        [SerializeField] private Button _unSubscribeButton;


        private MapAgentPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapAgentPresenter)presenter;

            _subscribeButton.onClick.AddListener(SubscribeChange);
            _unSubscribeButton.onClick.AddListener(UnSubscribeChange);

            _subscribeButton.gameObject.SetActive(false);
            _unSubscribeButton.gameObject.SetActive(false);
        }

        public void UpdateData(PlaygroundAgentChangeData data) {
            _nameAgent.text = GameRoot.LocalizationManager.Get(data.GetName);
            _changeDescription.text = GameRoot.LocalizationManager.Get(data.GetDescription);

            _changeGap.text = GameRoot.LocalizationManager.Get(MapMessage.GAP_PRICE) + ": " + data.GetPercentGap.ToString() + "%";
            
            _subscribeButton.gameObject.SetActive(data.IsSubscribe);
            _unSubscribeButton.gameObject.SetActive(!data.IsSubscribe);
        }

        private void SubscribeChange() => _presenter.Subscribe();

        private void UnSubscribeChange() => _presenter.UnSubscribe();

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {

            _subscribeButton.onClick.RemoveListener(SubscribeChange);
            _unSubscribeButton.onClick.RemoveListener(UnSubscribeChange);

            GameRoot.PrefabManager.DestroyGameObject(this.gameObject);
        }
    }
}
