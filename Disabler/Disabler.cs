using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace Disabler
{
    [BepInPlugin("dev.exel80.disabler", "Disabler", "1.0.1")]
    public class Disabler : BaseUnityPlugin
    {
        public static ConfigEntry<bool> bedCheckFire;
        public static ConfigEntry<bool> bedCheckEnemies;
        public static ConfigEntry<bool> bedCheckExposure;
        public static ConfigEntry<bool> bedCheckWet;

        public static ConfigEntry<bool> craftingstationCheckRoof;

        void Awake()
        {
            //Bed Settings
            bedCheckFire = Config.Bind("Bed", "Fire", true, "Disable requirement of fire nearby the bed");
            bedCheckEnemies = Config.Bind("Bed", "Enemies", false, "Disable requirement of killing nearby enemies");
            bedCheckExposure = Config.Bind("Bed", "Exposure", false, "Disable requirement of building a roof for the bed");
            bedCheckWet = Config.Bind("Bed", "Wet", true, "Disable requirement of being dry before interacting with the bed");

            //CraftingStation Settings
            craftingstationCheckRoof = Config.Bind("CraftingStation", "Roof", true, "Disable requirement of roof when using the crafting station(s)");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        #region CraftingStation
        [HarmonyPatch(typeof(CraftingStation), nameof(CraftingStation.CheckUsable))]
        static class CheckRoof_CraftingStation_Patch
        {
            static AccessTools.FieldRef<CraftingStation, bool> m_craftRequireRoofRef =
                AccessTools.FieldRefAccess<CraftingStation, bool>("m_craftRequireRoof");

            static bool Prefix(CraftingStation __instance)
            {
                m_craftRequireRoofRef(__instance) = !craftingstationCheckRoof.Value;
                return true;
            }
        }
        #endregion

        #region Bed
        [HarmonyPatch(typeof(Bed), "CheckFire")]
        static class CheckFire_Bed_Patch
        {
            static bool Prefix(ref bool __result)
            {
                if (bedCheckFire.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Bed), "CheckEnemies")]
        static class CheckEnemies_Bed_Patch
        {
            static bool Prefix(ref bool __result)
            {
                if (bedCheckEnemies.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Bed), "CheckExposure")]
        static class CheckExposure_Bed_Patch
        {
            static bool Prefix(ref bool __result)
            {
                if (bedCheckExposure.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Bed), "CheckWet")]
        static class CheckWet_Bed_Patch
        {
            static bool Prefix(ref bool __result)
            {
                if (bedCheckWet.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }
        #endregion
    }
}
