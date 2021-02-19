using System;
using BepInEx.Configuration;
using HarmonyLib;
using static Player;

namespace MasterMode
{
    [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
    static class PlayerStats_GetTotalFoodValue_Patch
    {
        [HarmonyPostfix]
        static void Postfix(Player __instance, ref float hp)
        {
            /**
             *  Modify how much base health a player has. Just make sure to recalculate food
             *  Thank you Xenofell#7360 for showing that Player:GetTotalFoodValue exist
             */

            ConfigEntry<Int32> baseHealth = MasterModePluign.GetConfig().Bind("Player", "BaseHealth", 1, "Player base health when haven't eat anything");

            // Base Health
            hp = (float)baseHealth.Value;

            // Recalculate food
            foreach (Food food in __instance.GetFoods())
            {
                hp += food.m_health;
            }
        }
    }

    [HarmonyPatch(typeof(Player), "GetBaseFoodHP")]
    static class PlayerStats_GetBaseFoodHP_Patch
    {
        [HarmonyPostfix]
        static void Postfix(ref float __result)
        {
            /*
             *  This modify "white health bar" next to the current health.
             *  Modifying this value doesn't affect how much heal you regenerate, it is just a visual.
             */

            ConfigEntry<Int32> baseHealth = MasterModePluign.GetConfig().Bind("Player", "BaseHealth", 1, "Player base health when haven't eat anything");

            __result = (float)baseHealth.Value;
        }
    }
}
