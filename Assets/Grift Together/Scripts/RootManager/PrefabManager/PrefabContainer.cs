using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "Scriptable Objects/PrefabContainer")]
    public class PrefabContainer : ScriptableObject {

        [SerializeField] private List<UniversalPrefabCollection> _universalPrefabCollection;
        [SerializeField] private List<ScenesManagerPrefabCollection> _scenesManagerCollection;
        [SerializeField] private List<LoginPrefabCollection> _loginPrefabCollection;
        [SerializeField] private List<MainMenuPrefabCollection> _mainMenuPrefabCollection;

        public GameObject GetPrefab(UniversalPrefabType typePrefab) {

            foreach (var item in _universalPrefabCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_scenesManagerCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }


        public GameObject GetPrefab(ScenesManagerPrefabType typePrefab) {

            foreach (var item in _scenesManagerCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_scenesManagerCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }

        public GameObject GetPrefab(LoginPrefabType typePrefab) {

            foreach (var item in _loginPrefabCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_loginPrefabCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }

        public GameObject GetPrefab(MainMenuPrefabType typePrefab) {

            foreach (var item in _mainMenuPrefabCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_loginPrefabCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }
    }
}
