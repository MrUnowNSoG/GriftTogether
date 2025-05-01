using System;
using UnityEngine;

namespace GriftTogether {

    [Serializable]
    public class ScenesServicePrefabCollection {

        public ScenesServicePrefabType prefabType;
        public GameObject prefab;

        public override string ToString() {
            return "ScenesService Prefab Collection";
        }
    }
}
