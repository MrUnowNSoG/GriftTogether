using TMPro;


namespace GriftTogether {

    public class LocalizationText : TextMeshProUGUI {


        protected override void OnEnable() {
            base.OnEnable();

            if (string.IsNullOrEmpty(text)) return;

            text = GameRoot.LocalizationManager.Get(text);
        }

    }
}
