using System;
using UnityEngine;

namespace GriftTogether {
    
    public class MainMenuSettingView : MonoBehaviour, IView {

        private MainMenuSettingPresenter _presenter;


        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
           _presenter = (MainMenuSettingPresenter)presenter;
        }

        public void ShowUI() {
           gameObject.SetActive(true);
        }


        public void CloseUI() {
            gameObject.SetActive(false);
        }

        public void Deinitialize() {
        }

    }
}
