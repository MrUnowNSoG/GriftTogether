using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgent : MonoBehaviour {

        [SerializeField] private PlaygroundSpawnCollection _spawns;
        public Vector3 GetPos(int indexPlayer) =>_spawns.GetPos(indexPlayer);

        //Stay in the agent
        public void ActiveAgent(bool owner) {

        }

        //Run across
        public void PassiveAgent(bool owner) {

        }

        //All move
        public void SubscribeAgent(bool owner) {

        }
    }
}
