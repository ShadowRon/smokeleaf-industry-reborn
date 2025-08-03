using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using HarmonyLib;
using Verse.Sound;
using System.Text.RegularExpressions;

namespace SmokeleafIndustry.HarmonyInstance
{
    [HarmonyPatch(typeof(BackCompatibility), "BackCompatibleDefName")]
    public static class BackCompatibility_BackCompatibleDefName_Patch
    {
        public static void Postfix(Type defType, string defName, ref string __result)
        {
            if (GenDefDatabase.GetDefSilentFail(defType, defName, false) == null)
            {
                string newName = string.Empty;
                //    Log.Message(string.Format("Checking for replacement for {0} Type: {1}", defName, defType));
                if (defType == typeof(ThingDef))
                {
                    /*
                    if (defName.Contains("ChaosDeamon_"))
                    {
                        if (defName.Contains("Corpse_"))
                        {
                            newName = Regex.Replace(defName, "Corpse_ChaosDeamon_", "Corpse_OG_Chaos_Deamon_");
                        }
                        else
                            newName = Regex.Replace(defName, "ChaosDeamon_", "OG_Chaos_Deamon_");
                    }
                    */

                    if (defName == "HempFabric")
                    {
                        newName = "HempCloth";
                    }

                }
                if (defType == typeof(FactionDef))
                {

                }
                if (defType == typeof(PawnKindDef))
                {

                }
                if (defType == typeof(ResearchProjectDef))
                {

                }
                if (defType == typeof(HediffDef))
                {

                }
                if (defType == typeof(BodyDef))
                {

                }
                if (defType == typeof(ScenarioDef))
                {

                }
                if (!newName.NullOrEmpty())
                {
                    __result = newName;
                }
                if (defName == __result)
                {
                    //    Log.Warning(string.Format("AMA No replacement found for: {0} T:{1}", defName, defType));
                }
                else
                {
                    //    Log.Message(string.Format("Replacement found: {0} T:{1}", __result, defType));
                }
            }
        }
    }

}
