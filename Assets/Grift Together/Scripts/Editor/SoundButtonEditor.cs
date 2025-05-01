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
            // 1) Спочатку малюємо всі стандартні поля Button
            base.OnInspectorGUI();

            // 2) Оновлюємо дані перед редагуванням
            serializedObject.Update();

            // 3) Малюємо наше поле enum
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_soundButtonProp, new GUIContent("Sound Button"));

            // 4) Зберігаємо зміни
            serializedObject.ApplyModifiedProperties();
        }
    }
}
