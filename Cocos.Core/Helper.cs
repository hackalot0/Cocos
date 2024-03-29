﻿using Cocos.Core.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Cocos.Core
{
    public static class Helper
    {
        public static string GetPublicPropertyStrings(object instance, params string[] additionalValues)
        {
            var vals = new List<string>();

            if (instance != default)
            {
                var tiObj = new TypeInfo(instance.GetType());
            }

            vals.AddRange(additionalValues);
            if (vals.Count == 0)
            {
                if (instance == default) return string.Empty;
                vals.Add(instance.GetType().Name);
            }
            if (vals.Count == 1) return vals[0];
            return $"{vals[0]} {string.Join(" ", vals.Skip(1).Select(a => $"[{a}]"))}";
        }
    }
}