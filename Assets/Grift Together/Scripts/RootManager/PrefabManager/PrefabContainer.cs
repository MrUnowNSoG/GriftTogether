using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "Scriptable Objects/PrefabContainer")]
    public class PrefabContainer : ScriptableObject {

        [SerializeField] private List<UniversalPrefabCollection> _universalPrefabCollection;
        [SerializeField] private List<ScenesManagerPrefabCollection> _scenesManagerCollection;
        [SerializeField] private List<LoginPrefabCollection> _loginPrefabCollection;
        [SerializeField] private List<MainMenuPrefabCollection> _mainMenuPrefabCollection;
        [SerializeField] private List<LobbyPrefabCollection> _lobbyPrefabCollection;
        [SerializeField] private List<MapPrefabCollection> _mapPrefabCollection;
        [SerializeField] private List<MapAgentPrefabCollection> _mapAgentPrefabCollection;

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

        public GameObject GetPrefab(LobbyPrefabType typePrefab) {

            foreach (var item in _lobbyPrefabCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_loginPrefabCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }

        public GameObject GetPrefab(MapPrefabType typePrefab) {

            foreach (var item in _mapPrefabCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_mapPrefabCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }

        public GameObject GetPrefab(MapAgentPrefabType typePrefab) {

            foreach (var item in _mapAgentPrefabCollection) {
                if (item.prefabType.Equals(typePrefab)) return item.prefab;
            }

            Debug.LogError($"Can't find GameObject in next collection: {_mapAgentPrefabCollection.ToString()}, with type: {typePrefab}!");
            return null;
        }
    }
}
