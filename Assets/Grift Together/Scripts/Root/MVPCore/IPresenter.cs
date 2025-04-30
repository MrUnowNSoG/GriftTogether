using UnityEngine;

namespace GriftTogether {

    public interface IPresenter {
        void Initialize();

        void CloseUI();

        void Deinitialize();
    }

}
