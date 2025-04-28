using UnityEngine;

namespace GriftTogether {

    [SerializeField]
    public class ScenesServicePrefabCollectio {

        public ScenesServicePrefabType prefabType;
        public GameObject prefab;

        public override string ToString() {
            return "ScenesService Prefab Collection";
        }
    }
}
