using System;
using System.Collections.Generic;

using BepInEx.Logging;
using HarmonyLib;
using RecipeModifier;

namespace MasterMode
{
    [HarmonyPatch(typeof(ObjectDB), "Awake")]
    static class Recipe_Patch
    {
        static AccessTools.FieldRef<ObjectDB, List<Recipe>> m_recipesRef =
                AccessTools.FieldRefAccess<ObjectDB, List<Recipe>>("m_recipes");

        [HarmonyPrefix]
        static void Prefix(ObjectDB __instance)
        {
            foreach (Recipe item in m_recipesRef(__instance))
            {
                // Null check
                if (item.m_item == null)
                    continue;

                foreach (Piece.Requirement resource in item.m_resources)
                {
                    if (RecipeModifierPlugin.GetMultipliers().TryGetValue(item.m_item.m_itemData.m_shared.m_itemType, out double value))
                    {
                        int calc = (int)Math.Round(resource.m_amount * value);
                        resource.m_amount = !RecipeModifierPlugin.allowResourceCostUnderOne.Value
                            ? Math.Max(1, calc)
                            : calc;
                    }
                    else
                        RecipeModifierPlugin.Log(LogLevel.Error, $"Something went wrong when trying to fetch {item.m_item.m_itemData.m_shared.m_itemType} value from config file.");
                }
            }
        }
    }
}
