namespace Ollama.Core.Extensions;

public static class ObjectExtensions
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        WriteIndented = false,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>
    /// Converts <paramref name="obj"/> to a JSON string.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static string AsJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, jsonSerializerOptions);
    }

    /// <summary>
    /// Converts  string to an instance of <typeparamref name="T"/>.
    /// </summary>
    /// <param name="json">The json string.</param>
    /// <returns></returns>
    public static T? FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
    }

    /// <summary>  
    /// Converts <paramref name="obj"/> to a dictionary.
    /// </summary>  
    /// <param name="obj">The specified object.</param>  
    /// <param name="isInclude">Flag indicating whether to include the properties in <paramref name="propertyNames"/>.   
    /// <br/>True to include <paramref name="propertyNames"/>, false to exclude <paramref name="propertyNames"/>.</param>  
    /// <param name="propertyNames">The property names to include/exclude.</param>  
    /// <returns></returns>  
    public static Dictionary<string, object?> AsDictionary(this object obj, bool isInclude = true, params string[] propertyNames)
    {
        Dictionary<string, object?> dictionary = [];

        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
            if (property.CanRead)
            {
                if (propertyNames == null || propertyNames.Length == 0)
                {
                    dictionary.Add(property.Name, property.GetValue(obj, null));
                }
                else
                {
                    if (isInclude)
                    {
                        if (propertyNames.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            dictionary.Add(property.Name, property.GetValue(obj, null));
                        }
                    }
                    else
                    {
                        if (!propertyNames.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            dictionary.Add(property.Name, property.GetValue(obj, null));
                        }
                    }
                }
            }
        }

        return dictionary;
    }
}