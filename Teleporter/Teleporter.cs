using System;
using BepInEx;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using System.Collections;
using BepInEx.Configuration;

namespace Teleporter
{
    [BepInPlugin("dev.exel80.mapteleporter", "MapTeleporter", "1.0.0")]
    public class Teleporter : BaseUnityPlugin
    {
        public static ConfigEntry<bool> hidePing;

        void Awake()
        {
            hidePing = Config.Bind("Ping", "hidePing", false, "Hide ping when using MapTeleporter plugin");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        [HarmonyPatch(typeof(Chat), nameof(Chat.SendPing))]
        static class Chat_SendPing_Patch
        {
            static bool Prefix(Chat __instance, ref Vector3 position)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Player localPlayer = Player.m_localPlayer;
                    localPlayer.TeleportTo(position, localPlayer.transform.rotation, true);
                    return !hidePing.Value;
                }
                return true;
            }
        }
    }
}
