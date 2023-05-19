using CraftingLib.GameDataObjects;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftingLibParts
{
    public static class PartsRegistry
    {
        private static Dictionary<string, Func<AppliancePart>> BasicFunctionDict = new Dictionary<string, Func<AppliancePart>>()
        {
            { "IRON", GDOUtils.GetCastedGDO<AppliancePart, BasicParts.Iron> },
            { "GOLD", GDOUtils.GetCastedGDO<AppliancePart, BasicParts.Gold> }
        };

        private static Dictionary<string, Func<AppliancePart>> FunctionDict = new Dictionary<string, Func<AppliancePart>>();
        public static bool Register<T>(string key) where T : CustomGameDataObject, new()
        {
            key = key.ToUpperInvariant();
            if (FunctionDict.ContainsKey(key))
            {
                Main.LogError($"{key} clashes with previously registered Appliance Part! Please check that it was not already registered. Otherwise, please use a unique key.");
                return false;
            }
            FunctionDict.Add(key, GDOUtils.GetCastedGDO<AppliancePart, T>);
            return true;
        }

        public static AppliancePart GetAppliancePart(string key)
        {
            key = key.ToUpperInvariant();
            if (!FunctionDict.TryGetValue(key, out var func))
                return null;
            return func();
        }

        public static AppliancePart GetBasicAppliancePart<T>(string key) where T : StandardPart
        {
            key = key.ToUpperInvariant();
            if (!BasicFunctionDict.TryGetValue(key, out var func))
                return null;
            return func();
        }

        public static List<AppliancePart> GetAllBasicApplianceParts()
        {
            List<AppliancePart> parts = new List<AppliancePart>();
            foreach (Func<AppliancePart> func in BasicFunctionDict.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value))
                parts.Add(func());
            return parts;
        }
    }
}
