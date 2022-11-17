
using BepInEx;
using HarmonyLib;

namespace MC_SVBlueprintFilterInvert
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        private enum type { weapons, equipments, items, ships };

        public const string pluginGuid = "mc.starvalor.bpfilterinvert";
        public const string pluginName = "SV Blueprint Filter Invert";
        public const string pluginVersion = "1.0.0";

        private static bool[] show = new bool[4];

        public void Awake()
        {
            show[(int)type.weapons] = true;
            show[(int)type.equipments] = false;
            show[(int)type.items] = false;
            show[(int)type.ships] = false;
            Harmony.CreateAndPatchAll(typeof(Main));
        }

        private static void Refresh(BlueprintCrafting instance, ref bool[] showType)
        {
            showType[(int)type.weapons] = show[(int)type.weapons];
            showType[(int)type.equipments] = show[(int)type.equipments];
            showType[(int)type.items] = show[(int)type.items];
            showType[(int)type.ships] = show[(int)type.ships];
            instance.RefreshScreen();
        }

        [HarmonyPatch(typeof(BlueprintCrafting), nameof(BlueprintCrafting.Open))]
        [HarmonyPostfix]
        private static void BPCraftOpen_Post(BlueprintCrafting __instance, ref bool[] ___showType)
        {
            Refresh(__instance, ref ___showType);
        }

        [HarmonyPatch(typeof(BlueprintCrafting), nameof(BlueprintCrafting.SetToggleWeapons))]
        [HarmonyPostfix]
        private static void BPCraftToggleWep_Post(BlueprintCrafting __instance, ref bool[] ___showType)
        {
            show[(int)type.weapons] = true;
            show[(int)type.equipments] = false;
            show[(int)type.items] = false;
            show[(int)type.ships] = false;
            Refresh(__instance, ref ___showType);
        }

        [HarmonyPatch(typeof(BlueprintCrafting), nameof(BlueprintCrafting.SetToggleEquipment))]
        [HarmonyPostfix]
        private static void BPCraftToggleEq_Post(BlueprintCrafting __instance, ref bool[] ___showType)
        {
            show[(int)type.weapons] = false;
            show[(int)type.equipments] = true;
            show[(int)type.items] = false;
            show[(int)type.ships] = false;
            Refresh(__instance, ref ___showType);
        }

        [HarmonyPatch(typeof(BlueprintCrafting), nameof(BlueprintCrafting.SetToggleItems))]
        [HarmonyPostfix]
        private static void BPCraftToggleItems_Post(BlueprintCrafting __instance, ref bool[] ___showType)
        {
            show[(int)type.weapons] = false;
            show[(int)type.equipments] = false;
            show[(int)type.items] = true;
            show[(int)type.ships] = false;
            Refresh(__instance, ref ___showType);
        }

        [HarmonyPatch(typeof(BlueprintCrafting), nameof(BlueprintCrafting.SetToggleShips))]
        [HarmonyPostfix]
        private static void BPCraftToggleShips_Post(BlueprintCrafting __instance, ref bool[] ___showType)
        {
            show[(int)type.weapons] = false;
            show[(int)type.equipments] = false;
            show[(int)type.items] = false;
            show[(int)type.ships] = true;
            Refresh(__instance, ref ___showType);
        }
    }
}
