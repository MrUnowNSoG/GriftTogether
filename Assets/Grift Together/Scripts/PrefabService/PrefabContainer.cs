using System;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "Scriptable Objects/PrefabContainer")]
    public class PrefabContainer : ScriptableObject {

        [SerializeField] private List<ScenesServicePrefabCollection> _scenesServiceCollection;

        public GameObject GetPrefab(ScenesServicePrefabType typePrefab) {

            foreach (var item in _scenesServiceCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_scenesServiceCollection[0]}, with type: {typePrefab}!");
            return null;
        }
    }
}
