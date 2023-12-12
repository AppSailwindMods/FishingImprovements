﻿using Crest;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FishingImprovements.Patches
{
    public static class FishingRodFishPatches
    {
        [HarmonyPatch(typeof(FishingRodFish))]
        [HarmonyPatch("ReleaseFish")]
        public static class ReleaseFishPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(FishingRodFish __instance)
            {
                return FishingImprovementsMain.instance.fishCanEscape.Value;
            }
        }

        [HarmonyPatch(typeof(FishingRodFish))]
        [HarmonyPatch("Update")]
        public static class FishUpdatePatch
        {
            [HarmonyPrefix]
            public static void Prefix(FishingRodFish __instance, ref ShipItemFishingRod ___rod, ref SimpleFloatingObject ___floater, ref ConfigurableJoint ___bobberJoint, ref float ___fishTimer)
            {
                if (__instance.currentFish == null && ___rod.health > 0f && ___rod.held && ___floater.InWater && ___bobberJoint.linearLimit.limit > 1f && __instance.gameObject.layer != 16)
                {
                    if (FishingImprovementsMain.instance.instantFishCatch.Value)
                    {
                        ___fishTimer = -1;
                    }
                }
            }
        }
    }
}
