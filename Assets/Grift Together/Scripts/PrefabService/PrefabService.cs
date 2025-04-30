using UnityEngine;

namespace GriftTogether {

    public class PrefabService {

        private PrefabContainer _prefabContainer;
        private int _countActiveObject;

        public PrefabService() {

            var temp = Resources.Load("PrefabContainer");

            _prefabContainer = temp as PrefabContainer;

            if(_prefabContainer == null) { 
                Debug.LogError("Can't find prefab container!"); 
                return; 
            }

            _countActiveObject = 0;
        }

        public GameObject InstantiatePrefab(ScenesServicePrefabType typePrefab, GameObject parent = null) {

            _countActiveObject++;
            GameObject go = _prefabContainer.GetPrefab(typePrefab);

           if(go == null) {
                _countActiveObject--;
                Debug.LogError($"Can't find GameObject in next collection: , with type: {typePrefab}!"); 
           }

            return ReturnGameObject(go, parent);
        }


        private GameObject ReturnGameObject(GameObject prefab, GameObject parent) {
            
            GameObject temp;

            if (parent != null) {
                temp = GameObject.Instantiate(prefab, parent.transform);

            } else {
                temp = GameObject.Instantiate(prefab);
            }

            return temp;
        }


        public void DestroyGameObject(GameObject go) {
            GameObject.Destroy(go);
            _countActiveObject --;
        }
    }
}
