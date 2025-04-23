using UnityEngine;

namespace GriftTogether {

    public abstract class SceneEntryPointBase : MonoBehaviour  {

        protected ServiceLocator _ParentServiceLocator { get; private set; }

        protected ServiceLocator _ServiceLocator { get; private set; }


        public virtual void Initialize(ServiceLocator parent) {
            parent = _ParentServiceLocator;
        }


        protected abstract void RegisterSceneServices();
    }
}
