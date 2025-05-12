using TMPro;
using UnityEngine;


namespace GriftTogether {

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [SelectionBase]
    public class LocalizationText : TextMeshProUGUI {

        private bool _init = false;

        protected override void OnEnable() {
         
            base.OnEnable();

            if (_init == false) {

                if (GameRoot.LocalizationManager == null) return;
                if (string.IsNullOrEmpty(text)) return;

                text = GameRoot.LocalizationManager.Get(text);
            }
        }

        public override string text {
            get { return base.text; }
            set {
                _init = true;
                base.text = value;
            }
        }

    }
}
