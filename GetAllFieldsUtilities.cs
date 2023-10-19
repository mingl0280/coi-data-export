using System;
using System.Collections.Generic;
using System.Reflection;

namespace COIDataExport
{
    public class GetAllFieldsUtilities
    {
        // Generic method to get all static fields of a given type
        protected static List<T> GetAllFieldsOfType<T>(Type nestedClassType)
        {
            var result = new List<T>();

            var fields = nestedClassType.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(T))
                {
                    var value = (T)field.GetValue(null);
                    result.Add(value);
                }
            }

            return result;
        }
        // Generic method to get all static fields of a given type
        protected static List<T> GetAllFieldsOfTypeRecursive<T>(Type RootList)
        {
            var result = new List<T>();
            var nestedTypes = RootList.GetNestedTypes();
            foreach (var nestedType in nestedTypes)
            {
                result.AddRange(GetAllFieldsOfTypeRecursive<T>(nestedType));
            }
            var fields = RootList.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(T))
                {
                    var value = (T)field.GetValue(null);
                    result.Add(value);
                }
            }

            return result;
        }
    }
}