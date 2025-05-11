using UnityEngine;

namespace GriftTogether {

    public class PrefabManager {

        private const string NAME_PREFAB_CONTAINER = "PrefabContainer";

        private PrefabContainer _prefabContainer;
        private int _countActiveObject;

        public PrefabManager() {

            var temp = Resources.Load(NAME_PREFAB_CONTAINER);

            _prefabContainer = temp as PrefabContainer;

            if(_prefabContainer == null) { 
                Debug.LogError("Can't find prefab container!"); 
                return; 
            }

            _countActiveObject = 0;
        }



        public GameObject InstantiatePrefab(UniversalPrefabType typePrefab, GameObject parent = null) {

            GameObject go = _prefabContainer.GetPrefab(typePrefab);

            if (IsNullPrefab(go, typePrefab.ToString())) return null;

            return ReturnGameObject(go, parent);
        }


        public GameObject InstantiatePrefab(ScenesManagerPrefabType typePrefab, GameObject parent = null) {

            GameObject go = _prefabContainer.GetPrefab(typePrefab);

            if (IsNullPrefab(go, typePrefab.ToString())) return null;

            return ReturnGameObject(go, parent);
        }

        public GameObject InstantiatePrefab(LoginPrefabType typePrefab, GameObject parent = null) {

            GameObject go = _prefabContainer.GetPrefab(typePrefab);

            if (IsNullPrefab(go, typePrefab.ToString())) return null;

            return ReturnGameObject(go, parent);
        }
 
        public GameObject InstantiatePrefab(MainMenuPrefabType typePrefab, GameObject parent = null) {

            GameObject go = _prefabContainer.GetPrefab(typePrefab);

            if(IsNullPrefab(go, typePrefab.ToString())) return null;

            return ReturnGameObject(go, parent);
        }



        private bool IsNullPrefab(GameObject go, string typePrefab) {

            if (go == null) {
                Debug.LogError($"Can't find prefab with type: {typePrefab}!");
                return true;
            }

            return false;
        }

        private GameObject ReturnGameObject(GameObject prefab, GameObject parent) {

            _countActiveObject++;

            return  parent != null 
                    ? GameObject.Instantiate(prefab, parent.transform)
                    : GameObject.Instantiate(prefab);
        }


        public void DestroyGameObject(GameObject go) {
            GameObject.Destroy(go);
            _countActiveObject --;
        }
    }
}
