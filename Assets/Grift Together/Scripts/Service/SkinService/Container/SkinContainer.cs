using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "SkinContainer", menuName = "Scriptable Objects/SkinContainer")]
    public class SkinContainer : ScriptableObject {

        [SerializeField] private List<SkinHatCollection> _skinHatCollections;
        [SerializeField] private List<SkinColorCollection> _skinColorCollections;
        [SerializeField] private List<SkinFaceCollection> _skinFaceCollections;


        public SkinHatCollection GetHatSkin(int index, out SkinContainerCode code) {
            (index, code) = IndexValidation(index, _skinHatCollections.Count);
            return _skinHatCollections[index];
        }

        public SkinColorCollection GetColorSkin(int index, out SkinContainerCode code) {
            (index, code) = IndexValidation(index, _skinColorCollections.Count);
            return _skinColorCollections[index];
        }

        public SkinFaceCollection GetFaceSkin(int index, out SkinContainerCode code) {
            (index, code) = IndexValidation(index, _skinFaceCollections.Count);
            return _skinFaceCollections[index];
        }

        private (int, SkinContainerCode) IndexValidation(int index, int countCollection) {
            
            SkinContainerCode code = SkinContainerCode.UsualSkin;

            if (index <= 0) {
                index = 0;
                code = SkinContainerCode.FirstSkin;
            }

            if (index >= countCollection) {
                index = 0;
                code = SkinContainerCode.FirstSkin;
            }

            if (index + 1 == countCollection) {
                code = SkinContainerCode.LastSkin;
            }

            return (index, code);
        }
    }
}
