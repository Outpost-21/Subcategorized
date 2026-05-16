using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Subcategorized
{
    [HarmonyPatch(typeof(DebugWindowsOpener), "DrawButtons")]
    public static class Patch_DebugWindowsOpener_DrawButtons
    {
        public static bool patched;

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> DrawAdditionalButtons(IEnumerable<CodeInstruction> instructions)
        {
            patched = false;
            CodeInstruction[] instructionsArr = instructions.ToArray();
            FieldInfo widgetRowField = AccessTools.Field(typeof(DebugWindowsOpener), "widgetRow");
            CodeInstruction[] array = instructionsArr;
            foreach (CodeInstruction inst in array)
            {
                if (!patched && widgetRowField != null && inst.opcode == OpCodes.Bne_Un_S)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0, (object)null);
                    yield return new CodeInstruction(OpCodes.Ldfld, (object)widgetRowField);
                    yield return new CodeInstruction(OpCodes.Call, (object)new Action<WidgetRow>(DrawDebugToolbarButton).Method);
                    patched = true;
                }
                yield return inst;
            }
        }

        public static void DrawDebugToolbarButton(WidgetRow widgets)
        {
            if (widgets.ButtonIcon(SubcategorizedTex.DebugBuildingEditor, "Open the Architect Editor. \n\nAllows easy generation of patches to manipulate the subcategories of buildings in the currently open architect category. Has no use in-game really, just makes it easy to create patches."))
            {
                WindowStack windowStack = Find.WindowStack;
                if (windowStack.IsOpen<Dialog_SubCatEditor>())
                {
                    windowStack.TryRemove(typeof(Dialog_SubCatEditor));
                }
                else
                {
                    windowStack.Add(new Dialog_SubCatEditor());
                }
            }
        }
    }
}
