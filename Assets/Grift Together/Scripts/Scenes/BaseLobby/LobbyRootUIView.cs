using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {
    public class LobbyRootUIView : MonoBehaviour {

        [Space(0)][Header("Root Object")]
        [SerializeField] private GameObject _headerGO;
        [SerializeField] private GameObject _leftCornerGO;
        public GameObject GetHeaderParent => _headerGO;
        public GameObject GetLeftCornerParent => _leftCornerGO;


        [Space(10)][Header("Button Menu")]
        [SerializeField] private Button _openMenuButton;
        [SerializeField] private Button _closeMenuButton;   
        public Button GetOpenMenuButton => _openMenuButton;
        public Button GetCloseMenuButton => _closeMenuButton;
    }
}
