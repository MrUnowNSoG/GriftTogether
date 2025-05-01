using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace GriftTogether {


    [CustomEditor(typeof(SoundButton))]
    public class SoundButtonEditor : ButtonEditor {
        SerializedProperty _soundButtonProp;

        protected override void OnEnable() {
            base.OnEnable();
            _soundButtonProp = serializedObject.FindProperty("_soundButton");
        }

        public override void OnInspectorGUI() {
            // 1) �������� ������� �� ��������� ���� Button
            base.OnInspectorGUI();

            // 2) ��������� ��� ����� ������������
            serializedObject.Update();

            // 3) ������� ���� ���� enum
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_soundButtonProp, new GUIContent("Sound Button"));

            // 4) �������� ����
            serializedObject.ApplyModifiedProperties();
        }
    }
}
