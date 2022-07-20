using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetComponent))]
public class TargetComponentEditor : Editor
{
    private TargetComponent _target;
    private SerializedObject _targetObject;

    private SerializedProperty _typeTarget;
    private SerializedProperty _moveSpeed;
    private SerializedProperty _distanceX;
    private SerializedProperty _distanceY;
    private SerializedProperty _radiusMove;

    private void OnEnable()
    {
        _target = (TargetComponent)target;
        _targetObject = new SerializedObject(_target);

        _typeTarget = _targetObject.FindProperty("_typeTarget");
        _moveSpeed = _targetObject.FindProperty("_movementSpeed");
        _distanceX = _targetObject.FindProperty("_distanceX");
        _distanceY = _targetObject.FindProperty("_distanceY");
        _radiusMove = _targetObject.FindProperty("_radiusRotation");
    }

    public override void OnInspectorGUI()
    {
        _targetObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_typeTarget);
        EditorGUILayout.PropertyField(_moveSpeed);

        switch(_target.TypeTarget)
        {
            case TypeOfTarget.HorizontalMovement:
                EditorGUILayout.PropertyField(_distanceX);
                break;
            case TypeOfTarget.VerticalMovement:
                EditorGUILayout.PropertyField(_distanceY);
                break;
            case TypeOfTarget.RadiusMovement:
                EditorGUILayout.Slider(_radiusMove, 0, 10, new GUIContent("Radius movement"));
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            _targetObject.ApplyModifiedProperties();
        }
        
    }

}
