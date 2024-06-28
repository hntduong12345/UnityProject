using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
//using UnityEngine;
//using UnityEditor;
 
[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute conHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(conHAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if(!conHAtt.HideInInspector || enabled )
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    /// <summary>
    /// Calculate the height of our property so that (when the property needs to be hidden) the following properties that are being drawn don’t overlap
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute conHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(conHAtt, property);

        if(!conHAtt.HideInInspector || enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute conHAtt, SerializedProperty property)
    {
        bool enabled = true;

        //Return the property path of the property we want to apply the attribute to
        string propertyPath = property.propertyPath;

        //Changes the path to the conditionalsource property path
        string conditionPath = propertyPath.Replace(property.name, conHAtt.ConditionalSourceField);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if(sourcePropertyValue != null)
        {
            enabled = sourcePropertyValue.boolValue;
        }
        else
        {
            Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: "
                + conHAtt.ConditionalSourceField);
        }

        return enabled;
    }
}