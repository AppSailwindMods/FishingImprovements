using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FishingImprovements.Patches
{
    public static class ShipItemFishingRodPatches
    {
        [HarmonyPatch(typeof(ShipItemFishingRod))]
        [HarmonyPatch("ExtraLateUpdate")]
        public static class ExtraLateUpdatePatch
        {
            [HarmonyPostfix]
            public static void Postfix()
            {

            }
        }

        [HarmonyPatch(typeof(ShipItemFishingRod))]
        [HarmonyPatch("OnAltActivate")]
        public static class OnAltActivatePatch
        {
            [HarmonyPrefix]
            public static bool Prefix(ShipItemFishingRod __instance, ref FishingRodFish ___fish, ref ConfigurableJoint ___bobberJoint, ref float ___minLength)
            {
                if (!__instance.sold)
                {
                    return true;
                }
                if (___fish.currentFish && ___bobberJoint.linearLimit.limit <= ___minLength + FishingImprovementsMain.instance.pickUpDistance.Value)
                {
                    var collectFish = AccessTools.Method(typeof(ShipItemFishingRod), "CollectFish");
                    __instance.StartCoroutine((IEnumerator)collectFish.Invoke(__instance, new object[] { }));
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(ShipItemFishingRod))]
        [HarmonyPatch("DetachHook")]
        public static class DetachHookPatch
        {
            [HarmonyPrefix]
            public static bool Prefix(ShipItemFishingRod __instance)
            {
                return FishingImprovementsMain.instance.destroyHook.Value;
            }
        }

        [HarmonyPatch(typeof(ShipItemFishingRod))]
        [HarmonyPatch("OnScroll")]
        public static class OnScrollPatch
        {
            [HarmonyPrefix]
            public static void Prefix(ShipItemFishingRod __instance, ref float ___reelCooldown, ref float ___scrollRollSpeed)
            {
                if (FishingImprovementsMain.instance.instantScroll.Value)
                {
                    ___reelCooldown = 0;
                }
                ___scrollRollSpeed = FishingImprovementsMain.instance.scrollSensitivity.Value;
            }
        }
    }
}
