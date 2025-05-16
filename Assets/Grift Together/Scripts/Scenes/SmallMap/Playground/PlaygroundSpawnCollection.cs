using UnityEngine;

namespace GriftTogether {

    public class PlaygroundSpawnCollection : MonoBehaviour {

        [SerializeField] private Transform[] _positions;

        public Vector3 GetPos(int index) {

            if (index < 0) {
                index = 0;
                Debug.LogError("Have minus index!");
            }

            if (index >= _positions.Length) {
                index = _positions.Length - 1;
                Debug.LogError("Have overhad index!");
            }

            return _positions[index].position;
        }
    }
}