using System;
using BepInEx;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using System.Collections;

namespace Teleporter
{
    [BepInPlugin("dev.exel80.mapteleporter", "MapTeleporter", "1.0.0")]
    public class Teleporter : BaseUnityPlugin
    {
        void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        [HarmonyPatch(typeof(Chat), nameof(Chat.SendPing))]
        static class Chat_SendPing_Patch
        {
            static void Prefix(Chat __instance, ref Vector3 position)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Player localPlayer = Player.m_localPlayer;
                    localPlayer.TeleportTo(position, localPlayer.transform.rotation, true);
                }
            }
        }
    }
}
