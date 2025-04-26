using System;
using UnityEngine;

namespace GriftTogether {

    public interface IView {

        event Action OnDeinitialize;
        event Action OnClose;

        void Initialize();
        void Deinitialize();


        void ShowUI();
        void CloseUI();
    }

}
