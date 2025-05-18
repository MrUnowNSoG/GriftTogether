using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace GriftTogether {

    [RequireComponent (typeof(CinemachineCamera))]
    public class MapSwitchCameraService : MonoBehaviour, IService {

        private CinemachineCamera _camera;
        private Dictionary<int, GameObject> _targetsList;

        public void Initialize() {
            _camera = gameObject.GetComponent<CinemachineCamera>();
            _camera.Target.TrackingTarget = null;

            _targetsList = new Dictionary<int, GameObject>();
        }

        public void AddTarget(int index,  GameObject target) {
            _targetsList.Add(index, target);
        }

        public void SwithcTarget(int index) {
            if(_targetsList.TryGetValue(index, out GameObject temp)) {
                _camera.Target.TrackingTarget = temp.transform;
            } else {
                Debug.LogError($"MapSwitchCameraService: Can't find object with id:{index}!");
            }
        }
    }
}
