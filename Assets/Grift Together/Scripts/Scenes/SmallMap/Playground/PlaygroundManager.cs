using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class PlaygroundManager : MonoBehaviour {

        [SerializeField] private List<PlaygroundAgent> _agents;

        public void Init() {

        }

        public Vector3 GetStartRoundPos(int indexPlayer) {
            return _agents[0].GetPos(indexPlayer);
        }
    }
}
