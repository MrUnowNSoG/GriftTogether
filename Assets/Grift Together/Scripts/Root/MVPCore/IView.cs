using System;
using UnityEngine;

namespace GriftTogether {

    public interface IView {

        event Action OnClose;

        void Initialize(IPresenter presenter);

        void ShowUI();
        void HideUI();

        void Deinitialize();
    }

}
