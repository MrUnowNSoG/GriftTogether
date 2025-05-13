using UnityEngine;

namespace GriftTogether {

    public abstract class BaseEntryPoint : MonoBehaviour {

        protected ServiceLocator _ServiceLocator { get; private set; }


        public virtual void Initialize(ServiceLocator parent) {
            _ServiceLocator = new ServiceLocator(parent);
        }

        protected abstract void RegisterGameServices();
        protected abstract void RegisterSceneServices();
        protected abstract void InitSceneManager();

        protected abstract void Deinitialize();

    }
}
