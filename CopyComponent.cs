using UnityEngine;
using System;
using System.Reflection;

public static class CopyComponent 
{
    public static T GetCopyOf<T>(this Component component, T componentToCopy) where T : Component
    {
        Type type = component.GetType();

        if (type != componentToCopy.GetType()) return null; 

        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

        PropertyInfo[] propertyInformation = type.GetProperties(flags);

        foreach (var information in propertyInformation)
        {
            if (information.CanWrite)
            {
                information.SetValue(component, information.GetValue(componentToCopy, null), null);
            }
        }

        FieldInfo[] fieldInformation = type.GetFields(flags);

        foreach (var information in fieldInformation)
        {
            information.SetValue(component, information.GetValue(componentToCopy));
        }

        return component as T;
    }
}
