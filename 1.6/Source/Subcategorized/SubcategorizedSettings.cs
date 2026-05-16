using UnityEngine;
using Verse;

namespace Subcategorized
{
    public class SubcategorizedSettings : ModSettings
    {
        public Vector2 subCatEditorPosition;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref subCatEditorPosition, "subCatEditorPosition", default(Vector2));
        }
    }
}
