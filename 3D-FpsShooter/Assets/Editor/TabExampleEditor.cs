using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TabExample))]
public class TabExampleEditor : Editor
{
    private TabExample _myTarget;
    private SerializedObject _soTarget;

    private SerializedProperty stringVar1;
    private SerializedProperty stringVar2;
    private SerializedProperty stringVar3;
    private SerializedProperty stringVar4;
    private SerializedProperty stringVar5;

    private SerializedProperty intVar1;
    private SerializedProperty intVar2;
    private SerializedProperty intVar3;
    private SerializedProperty intVar4;
    private SerializedProperty intVar5;

    private void OnEnable()
    {
        _myTarget = (TabExample)target;
        _soTarget = new SerializedObject(target);

        stringVar1 = _soTarget.FindProperty("stringVar1");
        stringVar2 = _soTarget.FindProperty("stringVar2");
        stringVar3 = _soTarget.FindProperty("stringVar3");
        stringVar4 = _soTarget.FindProperty("stringVar4");
        stringVar5 = _soTarget.FindProperty("stringVar5");

        intVar1 = _soTarget.FindProperty("intVar1");
        intVar2 = _soTarget.FindProperty("intVar2");
        intVar3 = _soTarget.FindProperty("intVar3");
        intVar4 = _soTarget.FindProperty("intVar4");
        intVar5 = _soTarget.FindProperty("intVar5");
    }


    public override void OnInspectorGUI()
    {
        _soTarget.Update();
        EditorGUI.BeginChangeCheck();

        _myTarget.toolBarTab = GUILayout.Toolbar(_myTarget.toolBarTab, new string[] { "Strings", "Integers", "Tab3", "Tab4" });

        switch(_myTarget.toolBarTab)
        {
            case 0:
                _myTarget.currentTab = "Strings";
                break;
            case 1:
                _myTarget.currentTab = "Integers";
                break;
            case 2:
                _myTarget.currentTab = "Tab3";
                break;
            case 3:
                _myTarget.currentTab = "Tab4";
                break;

        }

        if (EditorGUI.EndChangeCheck())
        {
            _soTarget.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }

        switch (_myTarget.currentTab)
        {
            case "Strings":
                EditorGUILayout.PropertyField(stringVar1);
                EditorGUILayout.PropertyField(stringVar2);
                EditorGUILayout.PropertyField(stringVar3);
                EditorGUILayout.PropertyField(stringVar4);
                EditorGUILayout.PropertyField(stringVar5);
                break;
            case "Integers":
                EditorGUILayout.PropertyField(intVar1);
                EditorGUILayout.PropertyField(intVar2);
                EditorGUILayout.PropertyField(intVar3);
                EditorGUILayout.PropertyField(intVar4);
                EditorGUILayout.PropertyField(intVar5);
                break;
            case "Tab3":
                break;
            case "Tab4":
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            _soTarget.ApplyModifiedProperties();
        }

        
    }
}
