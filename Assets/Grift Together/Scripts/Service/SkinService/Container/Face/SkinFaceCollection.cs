using System;
using UnityEngine;

namespace GriftTogether {

    [Serializable]
    public class SkinFaceCollection : ISkinCollection {

        public SkinFaceType Type;
        public string SkinName;
        public GameObject Prefab;

    }
}
