using UnityEngine;

namespace GriftTogether {

    public class MapEntryPoint : BaseEntryPoint {

        private void Awake() {
            Initialize(GameRoot.ServiceLocator);
        }

        public override void Initialize(ServiceLocator parent) {
            base.Initialize(parent);
        }

        protected override void InitSceneManager() {
            throw new System.NotImplementedException();
        }
        protected override void RegisterGameServices() {
            throw new System.NotImplementedException();
        }

        protected override void RegisterSceneServices() {
            throw new System.NotImplementedException();
        }

        public override void Deinitialize() {
            throw new System.NotImplementedException();
        }

    }
}
