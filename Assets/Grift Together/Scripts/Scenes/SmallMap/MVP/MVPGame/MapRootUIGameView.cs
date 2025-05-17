using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapRootUIGameView : MonoBehaviour {
        
        [Space(0)][Header("Root Object")]
        [SerializeField] private GameObject _headerGO;
        [SerializeField] private GameObject _leftCornerGO;
        public GameObject GetHeaderParent => _headerGO;
        public GameObject GetLeftCornerParent => _leftCornerGO;
    }
}
