namespace GriftTogether {

    public class PlayerGlobalControllerSkin {


        private PlayerSkinData _skinData;
        public PlayerSkinData GetCurrentPlayerSkinData => _skinData;

        public PlayerGlobalControllerSkin() {
            SetBaseValue();
        }

        private void SetBaseValue() {
            _skinData = new PlayerSkinData();

            _skinData.HatName = SkinHatType.None.ToString();
            _skinData.ColorName = SkinColorType.None.ToString();
            _skinData.FaceName = SkinFaceType.None.ToString();
        }

        public PlayerSkinData LoadPlayerSkin() {
            _skinData = GameRoot.PlayerPrefsManager.LoadSkinData();
            
            if( _skinData == null ) SetBaseValue();

            return _skinData;
        }

        public void SavePlayerSkin(PlayerSkinData skinData) {
            _skinData = skinData;
            
            GameRoot.PlayerPrefsManager.SavePlayerSkin(skinData);
        }
    }
}
