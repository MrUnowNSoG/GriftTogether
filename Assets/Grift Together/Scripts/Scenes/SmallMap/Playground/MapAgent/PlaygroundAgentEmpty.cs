using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentEmpty : PlaygroundAgent {

        public override void Initialize() {
            base.Initialize();
        }

        public override void Activate() {
            base.Activate();
            _mapManager.SkipMapAgent();
        }

    }
}
