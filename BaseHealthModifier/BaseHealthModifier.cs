using System;
using System.Collections.Generic;
using System.Reflection;

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace BaseHealthModifier
{
    [BepInPlugin("dev.exel80.basehealthmodifier", "BaseHealthModifier", "1.0.0")]
    public class BaseHealthModifierPlugin : BaseUnityPlugin
    {
        private static ConfigFile config;

        private static ManualLogSource logSource;

        public static ConfigEntry<Int32> baseHealth;

        private void Awake()
        {
            config = Config;

            logSource = Logger;

            baseHealth = GetConfig().Bind("Player", "BaseHealth", 1, "Player base health when haven't eat anything");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        public static void Log(LogLevel level, object data) => logSource.Log(level, data);

        public static ConfigFile GetConfig() => config;
    }
}
