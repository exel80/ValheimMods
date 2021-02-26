using System;
using System.Collections.Generic;
using System.Reflection;

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

using static ItemDrop.ItemData;

namespace RecipeModifier
{
    [BepInPlugin("dev.exel80.recipemodifier", "RecipeModifier", "1.1.0")]
    public class RecipeModifierPlugin : BaseUnityPlugin
    {
        private static ConfigFile config;

        private static ManualLogSource logSource;

        private static Dictionary<ItemType, double> _multiplier;

        public static ConfigEntry<bool> allowResourceCostUnderOne;

        private void Awake()
        {
            config = Config;

            logSource = Logger;

            allowResourceCostUnderOne = config.Bind("General", "AllowResourceCostUnderOne", false, "Allow resource cost be under one after multiplying\ntrue = Resource cost can go 0\nfalse = Resource cost will be 1 or higher");

            _multiplier = InitializeResourceMultipliers();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        private Dictionary<ItemType, double> InitializeResourceMultipliers()
        {
            // Create blank Dictionary
            Dictionary<ItemType, double> _multipliers = new Dictionary<ItemType, double>();

            // Gear list of ItemTypes. Used to modify default value of multiplier from 1.5 to 2.0
            List<ItemType> _gear_list = new List<ItemType>() { ItemType.Helmet, ItemType.Chest, ItemType.Shoulder, ItemType.Legs };

            // Fill Dictionary with all of the ItemTypes
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                // Name of the type
                string typeName = type.ToString();

                // Resource multiplier default value
                double multiplier = _gear_list.Contains(type) ? 2.0 : 1.5;

                // Fetch multiplier value from config file
                ConfigEntry<double> typeData = Config.Bind(
                    "RecipeMultiplier",
                    typeName,
                    multiplier,
                    $"{typeName} recipe(s) resource multiplier"
                );

                // Add multiplier value to the Dictionary
                _multipliers.Add(type, typeData.Value);
            }

            // Return Dictionary
            return _multipliers;
        }

        public static Dictionary<ItemType, double> GetMultipliers() => _multiplier;

        public static void Log(LogLevel level, object data) => logSource.Log(level, data);

        public static ConfigFile GetConfig() => config;
    }
}
