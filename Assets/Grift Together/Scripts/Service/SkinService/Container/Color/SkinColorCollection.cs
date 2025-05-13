using System;
using UnityEngine;

namespace GriftTogether {

    [Serializable]
    public class SkinColorCollection : ISkinCollection {

        public SkinColorType Type;
        public string SkinName;
        public GameObject Prefab;

    }
}
