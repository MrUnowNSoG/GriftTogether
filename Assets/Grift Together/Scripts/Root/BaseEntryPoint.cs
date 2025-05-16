using UnityEngine;

namespace GriftTogether {

    public abstract class BaseEntryPoint : MonoBehaviour {

        protected ServiceLocator _localServiceLocator { get; private set; }


        public virtual void Initialize(ServiceLocator parent) {
            _localServiceLocator = new ServiceLocator(parent);
        }

        protected abstract void RegisterGameServices();
        protected abstract void RegisterSceneServices();
        protected abstract void InitSceneManager();

        public virtual void ExtendLocalServiceLocator<T>(T service) where T : IService {
            _localServiceLocator.AddService(service);
        }

        public abstract void Deinitialize();

    }
}
