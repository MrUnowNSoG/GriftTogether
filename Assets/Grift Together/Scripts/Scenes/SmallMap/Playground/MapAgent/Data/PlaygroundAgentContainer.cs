using UnityEngine;

namespace GriftTogether {

    [CreateAssetMenu(fileName = "PlaygroundAgentContainer", menuName = "Scriptable Objects/PlaygroundAgentContainer")]
    public class PlaygroundAgentContainer : ScriptableObject {

        [Space(0)][Header("Main")]
        [SerializeField] private string _indeficationAgent;
        public string GetIndeficationAgent => _indeficationAgent;

        [Space(10)][Header("Description")]
        [SerializeField] private string _nameAgent;
        public string GetNameAgent => _nameAgent;


        [SerializeField] private string _descriptionAgent;
        public string GetDescriptionAgent => _descriptionAgent;


        [SerializeField] private int _priceAgent;
        public int GetPriceAgent => _priceAgent;


        [SerializeField] private int _rentPrice;
        public int GetRentPrice => _rentPrice;
        
    }
}
