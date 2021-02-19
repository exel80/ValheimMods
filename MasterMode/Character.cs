using System;
using System.Collections.Generic;

using HarmonyLib;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace MasterMode
{
    [HarmonyPatch(typeof(Character), "Awake")]
    static class Character_Enemy_Patch
    {
        static AccessTools.FieldRef<Character, Int32> m_levelRef =
                AccessTools.FieldRefAccess<Character, Int32>("m_level");

        [HarmonyPostfix]
        static void Postfix(Character __instance)
        {
            // Settings
            ConfigEntry<bool> bypassNeutrals = MasterModePluign.GetConfig().Bind("Enemy", "bypassNeutrals", true, "When true, level multiplier will NOT multiply Deer or Boar levels");
            ConfigEntry<Int32> minLevel = MasterModePluign.GetConfig().Bind("Enemy", "minLevel", 3, "Minimum monster level multiplier");
            ConfigEntry<int> maxLevel = MasterModePluign.GetConfig().Bind("Enemy", "maxLevel", 5, "Maximum monster level multiplier");

            // Make sure only modify enemies
            if (__instance.m_name.StartsWith("$enemy_"))
            {
                List<string> neutrals = new List<string>() { "deer", "boar" };
                string EnemyName = __instance.m_name.Substring("$enemy_".Length);

                // If bypassNeutrals true, ignore deer and boar.
                if (bypassNeutrals.Value && neutrals.Contains(EnemyName))
                    return;

                Random rnd = new Random();
                
                int prevLevel = m_levelRef(__instance);

                // Soft cap level to 10
                // TODO: Figure out how to prevent level up same monster multiple times when load back to area.
                if (prevLevel >= 10)
                    return;

                int level = m_levelRef(__instance) * rnd.Next(minLevel.Value, maxLevel.Value);

                __instance.SetLevel(level);

                MasterModePluign.Log(LogLevel.Message, $"Buffed {EnemyName} level from {prevLevel} to {m_levelRef(__instance)}");
            }
        }
    }

    [HarmonyPatch(typeof(Game), "GetPlayerDifficulty")]
    static class Game_GetPlayerDifficulty_Patch
    {
        [HarmonyPostfix]
        static void Postfix(ref int __result)
        {
            ConfigEntry<int> bypassNeutrals = MasterModePluign.GetConfig().Bind("Difficulty", "forceAddPlayers", 3, "Add \"fake players\" to scale up server difficulty\nEnemies HP scale based of player count on the server (0 = disabled)");

            __result += bypassNeutrals.Value;
        }
    }
}
