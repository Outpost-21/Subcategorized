using LudeonTK;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Subcategorized
{
    [StaticConstructorOnStartup]
    public class Dialog_SubCatEditor : Window
    {
        public Vector2 windowPosition;

        public static List<DebugActionNode> cachedNodes;

        public int reorderableGroupID = -1;

        public Dictionary<string, string> nameCache = new Dictionary<string, string>();

        public int lastLabelCacheFrame = -1;

        public Vector2 defaultSize = new Vector2(1200f, 240f);

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        public Dictionary<ThingDef, ArchitectDefChanges> currentDefChanges = new Dictionary<ThingDef, ArchitectDefChanges>();

        public ArchitectCategoryTab SelectedCategory => ((MainTabWindow_Architect)Find.WindowStack.WindowOfType<MainTabWindow_Architect>())?.selectedDesPanel ?? null;

        public Dialog_SubCatEditor()
        {
            resizeable = true;
            draggable = true;
            focusWhenOpened = false;
            drawShadow = false;
            closeOnAccept = false;
            closeOnCancel = false;
            preventCameraMotion = false;
            drawInScreenshotMode = false;
            windowPosition = SubcategorizedStartup.SubCatEditorPosition;
            onlyDrawInDevMode = true;
            lastLabelCacheFrame = RealTime.frameCount;
        }

        public override void DoWindowContents(Rect inRect)
        {
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            DoSubCatStuff(listing);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
            if (!Mathf.Approximately(windowRect.x, windowPosition.x) || !Mathf.Approximately(windowRect.y, windowPosition.y))
            {
                windowPosition = new Vector2(windowRect.x, windowRect.y);
                SubcategorizedStartup.SubCatEditorPosition = windowPosition;
            }
        }

        public void DoSubCatStuff(Listing_Standard ls)
        {

        }

        public override void WindowUpdate()
        {
            base.WindowUpdate();
            if (RealTime.frameCount >= lastLabelCacheFrame + 30)
            {
                nameCache.Clear();
                lastLabelCacheFrame = RealTime.frameCount;
            }
        }

        public override void SetInitialSizeAndPosition()
        {
            windowPosition.x = Mathf.Clamp(windowPosition.x, 0f, UI.screenWidth - defaultSize.x);
            windowPosition.y = Mathf.Clamp(windowPosition.y, 0f, UI.screenHeight - defaultSize.y);
            windowRect = new Rect(windowPosition.x, windowPosition.y, defaultSize.x, defaultSize.y);
            windowRect = windowRect.Rounded();
        }
    }

    public class ArchitectDefChanges
    {
        public DesignationCategoryDef category;
        public List<string> subcats;
    }
}
