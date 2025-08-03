﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace SmokeleafIndustry.HarmonyInstance
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.ogliss.rimworld.mod.SmokeleafIndustry");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            if (Prefs.DevMode) Log.Message(string.Format("Smokeleaf Industry: successfully completed {0} harmony patches.", harmony.GetPatchedMethods().Select(new Func<MethodBase, Patches>(Harmony.GetPatchInfo)).SelectMany((Patches p) => p.Prefixes.Concat(p.Postfixes).Concat(p.Transpilers)).Count((Patch p) => p.owner.Contains(harmony.Id))));
        }

    }
}