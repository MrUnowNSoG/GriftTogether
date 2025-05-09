using UnityEngine;

namespace GriftTogether {

    public interface IPresenter {
        void Initialize();

        void ShowUI();
        void CloseUI();

        void Deinitialize();
    }

}
