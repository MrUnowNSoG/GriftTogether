using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundBaseAgent : PlaygroundAgent {


        public override void Initialize() {
            base.Initialize();
        }

        public override void Activate() {
            base.Activate();
            _mapManager.SkipMapAgent();
        }

        public override void Across() {
        }

    }
}
