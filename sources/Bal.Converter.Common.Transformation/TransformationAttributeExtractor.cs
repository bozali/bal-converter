﻿using Bal.Converter.Common.Transformation.Attributes;

namespace Bal.Converter.Common.Transformation;

public static class TransformationAttributeExtractor
{
    public static string? ExtractExtension(Type type)
    {

        if (!type.GetInterfaces().Contains(typeof(IFileTransformation)))
        {
            return null;
        }

        if (Attribute.GetCustomAttribute(type, typeof(ExtensionAttribute)) is not ExtensionAttribute extensionAttribute)
        {
            return null;
        }

        return extensionAttribute.Extension;
    }

    public static Type[] ExtractSupportedTypes(Type type)
    {
        var targetAttributes = Attribute.GetCustomAttributes(type, typeof(TargetAttribute)) as TargetAttribute[];

        if (targetAttributes == null || !targetAttributes.Any())
        {
            throw new ArgumentException($"Target is missing for {type.FullName}.");
        }

        return targetAttributes.Select(t => t.Target).ToArray();
    }
}