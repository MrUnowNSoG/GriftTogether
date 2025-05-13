using System;
using UnityEngine;

namespace GriftTogether {

    public class SkinService : IService {

        private const string NAME_SKIN_SONTAINER = "SkinContainer";

        private SkinContainer _skinContainer;

        private SkinServiceAgent _currentAgent;

        private SkinHatCollection _currentHat;
        private int _currentHatIndex;
        public string GetHatName => _currentHat.SkinName; 

        private SkinColorCollection _currentColor;
        private int _currentColorIndex;
        public string GetColorName => _currentColor.SkinName;

        private SkinFaceCollection _currentFace;
        private int _currentFaceIndex;
        public string GetFaceName => _currentFace.SkinName;

        public SkinService() {

            var temp = Resources.Load(NAME_SKIN_SONTAINER);

            _skinContainer = temp as SkinContainer;
            if (_skinContainer == null) {
                Debug.LogError("Skin service can't find SKIN CONTAINER!");
            }

            LoadSkinPlayer();
        }

        private void LoadSkinPlayer() {
            PlayerSkinData data = GameRoot.PlayerGlobalManager.LoadPlayerSkin();

            if (Enum.TryParse(data.HatName, true, out SkinHatType hat) == false) hat = SkinHatType.None;
            _currentHat = _skinContainer.GetHatSkin(hat, out _currentHatIndex);

            if (Enum.TryParse(data.ColorName, true, out SkinColorType color) == false) color = SkinColorType.None;
            _currentColor = _skinContainer.GetColorSkin(color, out _currentColorIndex);

            if (Enum.TryParse(data.FaceName, true, out SkinFaceType face) == false) face = SkinFaceType.None;
            _currentFace = _skinContainer.GetFaceSkin(face, out _currentFaceIndex);
        }

        public void SetSkinAgent(SkinServiceAgent agent) {
            _currentAgent = agent;
        }

        public void ResolveCurrentSkin() {
            if (_currentAgent == null) return;

            SpawnHat();
            SpawnColor();
            SpawnFace();
        }

        private void SpawnHat() {
            if (_currentAgent == null) return;

            _currentAgent.SetHat(GameObject.Instantiate(_currentHat.Prefab, _currentAgent.GetHatParent.transform));
        }

        private void SpawnColor() {
            if (_currentAgent == null) return;

            _currentAgent.SetColor(GameObject.Instantiate(_currentColor.Prefab, _currentAgent.GetColorParent.transform));
        }

        private void SpawnFace() {
            if (_currentAgent == null) return;

            _currentAgent.SetFace(GameObject.Instantiate(_currentFace.Prefab, _currentAgent.GetFaceParent.transform));
        }

        public string ChangeSkin<T>(T type, int direction) where T : ISkinCollection {

            if (type is SkinHatCollection) {
                _currentHatIndex += direction;
                return MoveHat();
            }

            if (type is SkinColorCollection) {
                _currentColorIndex += direction;
                return MoveColor();
            }

            if (type is SkinFaceCollection) {
                _currentFaceIndex += direction;
                return MoveFace();
            }

            return "";
        }

        private string MoveHat() {
            _currentHat = _skinContainer.GetHatSkin(_currentHatIndex, out _currentHatIndex);
            SpawnHat();

            return _currentHat.SkinName;
        }

        private string MoveColor() {
            _currentColor = _skinContainer.GetColorSkin(_currentColorIndex, out _currentColorIndex);
            SpawnColor();

            return _currentColor.SkinName;
        }

        private string MoveFace() {
            _currentFace = _skinContainer.GetFaceSkin(_currentFaceIndex, out _currentFaceIndex);
            SpawnFace();

            return _currentFace.SkinName;
        }

        public void SaveCurrentSkin() {
            PlayerSkinData data = new PlayerSkinData();
            data.HatName = _currentHat.Type.ToString();
            data.ColorName = _currentColor.Type.ToString();
            data.FaceName = _currentFace.Type.ToString();

            GameRoot.PlayerPrefsManager.SavePlayerSkin(data);
        }
    }
}
