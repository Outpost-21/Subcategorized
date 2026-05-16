using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Subcategorized
{
    public class SubcategorizedMod : Mod
    {
        public static SubcategorizedMod mod;
        public static SubcategorizedSettings settings;

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public SubcategorizedMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<SubcategorizedSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            Log.Message($":: Subcategorized :: ".Colorize(Color.cyan) + $"{CurrentVersion} ::");

            File.WriteAllText(VersionDir, CurrentVersion);

            Harmony harmony = new Harmony("Neronix17.Subcategorized.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "Subcategorized";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            DoOptionsCategoryContents(listing);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public void DoOptionsCategoryContents(Listing_Standard listing)
        {

        }
    }
}
