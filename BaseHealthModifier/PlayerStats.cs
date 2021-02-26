using System;
using BepInEx.Configuration;
using HarmonyLib;
using static Player;

namespace BaseHealthModifier
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

            // Base Health
            hp = (float)BaseHealthModifierPlugin.baseHealth.Value;

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

            __result = (float)BaseHealthModifierPlugin.baseHealth.Value;
        }
    }
}
