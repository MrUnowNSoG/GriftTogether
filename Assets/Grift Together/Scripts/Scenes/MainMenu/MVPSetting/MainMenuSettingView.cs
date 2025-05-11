using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {
    
    public class MainMenuSettingView : MonoBehaviour, IView {

        [Space(0)][Header("Sound Block")]
        [SerializeField] private Toggle _masterSoundToggle;
        [SerializeField] private Slider _soundVolumeSlider;
        [SerializeField] private Slider _musicVoluimeSlider;

        [Space(10)][Header("Language")]


        private MainMenuSettingPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
           _presenter = (MainMenuSettingPresenter)presenter;
        }


        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
        }

    }
}
