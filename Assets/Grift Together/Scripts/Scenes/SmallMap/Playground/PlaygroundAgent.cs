using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgent : MonoBehaviour {

        [SerializeField] private PlaygroundSpawnCollection _spawns;
        public Vector3 GetPos(int indexPlayer) =>_spawns.GetPos(indexPlayer);


    }
}
