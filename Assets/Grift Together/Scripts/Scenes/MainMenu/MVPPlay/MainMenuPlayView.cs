using System;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuPlayView : MonoBehaviour, IView {

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            throw new NotImplementedException();
        }

        public void ShowUI() {
            throw new NotImplementedException();
        }


        public void CloseUI() {
            throw new NotImplementedException();
        }

        public void Deinitialize() {
            throw new NotImplementedException();
        }

    }
}
