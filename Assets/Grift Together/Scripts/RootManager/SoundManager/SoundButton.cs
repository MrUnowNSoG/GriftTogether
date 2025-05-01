using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GriftTogether {

    public class SoundButton : Button {

        [Space(10)] [Header("Overloading")] 
        [SerializeField] private TypeSoundButton _soundButton;

        public override void OnPointerClick(PointerEventData eventData) {
            GameRoot.SoundManager.PlayButtonSound(_soundButton);
            base.OnPointerClick(eventData);
        }

    }
}
