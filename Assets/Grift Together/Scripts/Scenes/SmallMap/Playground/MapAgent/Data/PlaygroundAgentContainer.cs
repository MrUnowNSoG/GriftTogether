using UnityEngine;

namespace GriftTogether {

    [CreateAssetMenu(fileName = "PlaygroundAgentContainer", menuName = "Scriptable Objects/PlaygroundAgentContainer")]
    public class PlaygroundAgentContainer : ScriptableObject {

        [Space(0)][Header("Main")]
        [SerializeField] private string _indeficationAgent;
        public string GetIndeficationAgent => _indeficationAgent;

        [SerializeField] private string _nameAgent;
        public string GetNameAgent => _nameAgent;


        [Space(10)][Header("Buy")]
        [SerializeField] private string _description;
        public string GetBuyDescription => _description;


        [SerializeField] private int _priceAgent;
        public int GetPriceAgent => _priceAgent;


        [Space(10)][Header("Rent")]
        [SerializeField] private int _rentPrice;
        public int GetRentPrice => _rentPrice;

        [SerializeField] private string _rentDescription;
        public string GetRentDescription => _rentDescription;
        
    }
}
