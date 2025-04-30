using System;
using UnityEngine;

namespace GriftTogether {

    [Serializable]
    public class ScenesServicePrefabCollectio {

        public ScenesServicePrefabType prefabType;
        public GameObject prefab;

        public override string ToString() {
            return "ScenesService Prefab Collection";
        }
    }
}
