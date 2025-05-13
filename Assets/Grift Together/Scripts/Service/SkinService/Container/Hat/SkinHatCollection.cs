using System;
using UnityEngine;

namespace GriftTogether {

    [Serializable]
    public class SkinHatCollection : ISkinCollection {

        public SkinHatType Type;
        public string SkinName;
        public GameObject Prefab;

    }
}
