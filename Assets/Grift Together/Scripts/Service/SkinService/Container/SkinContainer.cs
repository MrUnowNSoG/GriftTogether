using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "SkinContainer", menuName = "Scriptable Objects/SkinContainer")]
    public class SkinContainer : ScriptableObject {

        [SerializeField] private List<SkinHatCollection> _skinHatCollections = new List<SkinHatCollection>();
        public int GetCountHat => _skinHatCollections.Count;

        [SerializeField] private List<SkinColorCollection> _skinColorCollections = new List<SkinColorCollection>();
        public int GetCountColor => _skinColorCollections.Count;

        [SerializeField] private List<SkinFaceCollection> _skinFaceCollections = new List<SkinFaceCollection>();
        public int GetCountFace => _skinFaceCollections.Count;


        public SkinHatCollection GetHatSkin(SkinHatType type, out int index) {

            index = -1;
            
            for(int i = 0; i < _skinHatCollections.Count; i++) {
                if (_skinHatCollections[i].Type == type) {
                    index = i;
                    return _skinHatCollections[i];
                }
            }

            return null;
        }

        public SkinHatCollection GetHatSkin(int index, out int newIndex) {
            newIndex = ValidationIndex(index, GetCountHat);
            return _skinHatCollections[newIndex];
        }

        public SkinColorCollection GetColorSkin(SkinColorType type, out int index) {

            index = -1;

            for (int i = 0; i < _skinColorCollections.Count; i++) {
                if (_skinColorCollections[i].Type == type) {
                    index = i;
                    return _skinColorCollections[i];
                }
            }

            return null;
        }

        public SkinColorCollection GetColorSkin(int index, out int newIndex) {
            newIndex = ValidationIndex(index, GetCountColor);
            return _skinColorCollections[newIndex];
        }

        public SkinFaceCollection GetFaceSkin(SkinFaceType type, out int index) {

            index = -1;

            for (int i = 0; i < _skinFaceCollections.Count; i++) {
                if (_skinFaceCollections[i].Type == type) {
                    index = i;
                    return _skinFaceCollections[i];
                }
            }

            return null;
        }

        public SkinFaceCollection GetFaceSkin(int index, out int newIndex) {
            newIndex = ValidationIndex(index, GetCountFace);
            return _skinFaceCollections[newIndex];
        }
        private int ValidationIndex(int index, int count) {
            if (index < 0) index = count - 1;
            if(index >= count) index = 0;

            return index;
        }
    }
}
