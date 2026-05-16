using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabulaRasa;
using UnityEngine;
using Verse;

namespace Subcategorized
{
    [StaticConstructorOnStartup]
    public static class SubcategorizedStartup
    {
        public static SubcategorizedSettings Setting => SubcategorizedMod.settings;

        static SubcategorizedStartup()
        {
            // Automatically make any architect category allow subcategories.
            foreach (DesignationCategoryDef d in DefDatabase<DesignationCategoryDef>.AllDefs)
            {
                if (!d.HasModExtension<DefModExt_SubcategoryDisplay>())
                {
                    if (d.modExtensions.NullOrEmpty())
                    {
                        d.modExtensions = new List<DefModExtension>();
                    }
                    d.modExtensions.Add(new DefModExt_SubcategoryDisplay());
                }
            }
        }

        public static Vector2 SubCatEditorPosition
        {
            get
            {
                return Setting.subCatEditorPosition;
            }
            set
            {
                if (Setting.subCatEditorPosition != value)
                {
                    Setting.subCatEditorPosition = value;
                    SubcategorizedMod.settings.Write();
                }
            }
        }
    }
}
