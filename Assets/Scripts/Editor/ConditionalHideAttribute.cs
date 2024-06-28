using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    //The name of bool field that will be in control
    public string ConditionalSourceField = "";

    /// <summary>
    ///  TRUE = Hide in inspector
    ///  FALSE = Disable in inspector
    /// </summary>
    public bool HideInInspector = false;

    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hiseInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hiseInInspector;
    }
}
