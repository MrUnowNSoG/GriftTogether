using UnityEngine;

namespace GriftTogether {

    public class MainMenuRootUIView : MonoBehaviour {

        [SerializeField] private GameObject _headerGO;
        [SerializeField] private GameObject _leftCornerGO;
        

        public GameObject GetHeaderParent => _headerGO;
        public GameObject GetLeftCornerParent => _leftCornerGO;
    }
}
