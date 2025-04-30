using System;
using UnityEngine;

namespace GriftTogether {

    public interface IView {

        event Action OnClose;

        void Initialize(IPresenter presenter);
        void Deinitialize();


        void ShowUI();
        void HideUI();
    }

}
