using System;
using System.Collections.Generic;
using System.Reflection;

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

using static ItemDrop.ItemData;

/**
 * Original idea from Aurarus#3253
 * "I'm wondering how difficult it'd be to make a "Master mode" mod for valheim"
 * 
 * X = Done
 * % = Half way done OR working on it
 *   = (empty) Not started yet
 * 
 * TODO:
 * [] All enemies besides deer and boar spawn as one/ two star or higher
 * [X] Crafting recipes for gear are doubled in price/ [?] include harder to find materials
 * [X] Default health without food is 1 instead of 25
 * [] Bosses deal more damage and have faster attack animations
*/

namespace MasterMode
{
    [BepInPlugin("dev.exel80.mastermode", "Master Mode", "1.0.0")]
    public class MasterModePluign : BaseUnityPlugin
    {
        private static ConfigFile config;

        private static ManualLogSource logSource;

        private static Dictionary<ItemType, Int32> _multiplier;

        private void Awake()
        {
            config = Config;

            logSource = Logger;

            _multiplier = InitializeResourceMultipliers();

            //_multiplier.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(UnityEngine.Debug.Log);

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        private Dictionary<ItemType, Int32> InitializeResourceMultipliers()
        {
            // Create blank Dictionary
            Dictionary<ItemType, Int32> _multipliers = new Dictionary<ItemType, Int32>();

            // Gear list of ItemTypes. Used to modify default value of multiplier from 2 to 3
            List<ItemType> _gear_list = new List<ItemType>() { ItemType.Helmet, ItemType.Chest, ItemType.Shoulder, ItemType.Legs };

            // Fill Dictionary with all of the ItemTypes
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                // Name of the type
                string typeName = type.ToString();

                // Resource multiplier default value
                int multiplier = _gear_list.Contains(type) ? 3 : 2;

                // Fetch multiplier value from config file
                ConfigEntry<Int32> typeData = Config.Bind(
                    "RecipeMultiplier", 
                    typeName,
                    multiplier,
                    $"{typeName} recipe resource multiplier"
                );

                // Add multiplier value to the Dictionary
                _multipliers.Add(type, typeData.Value);
            }

            // Return Dictionary
            return _multipliers;
        }

        public static Dictionary<ItemType, Int32> GetMultipliers() => _multiplier;

        public static void Log(LogLevel level, object data) => logSource.Log(level, data);

        public static ConfigFile GetConfig() => config;
    }
}
