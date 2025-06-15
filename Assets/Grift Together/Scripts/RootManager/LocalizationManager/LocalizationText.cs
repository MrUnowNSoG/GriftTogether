using TMPro;
using UnityEngine;


namespace GriftTogether {

    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [SelectionBase]
    public class LocalizationText : TextMeshProUGUI {


        protected override void OnEnable() {
         
            base.OnEnable();

            if (GameRoot.LocalizationManager != null && GameRoot.LocalizationManager.Init) {

                if (string.IsNullOrEmpty(text)) return;
                text = GameRoot.LocalizationManager.Get(text);
            }
        }

        public override string text {
            get { return base.text; }
            set {
                base.text = value;
            }
        }

    }
}
