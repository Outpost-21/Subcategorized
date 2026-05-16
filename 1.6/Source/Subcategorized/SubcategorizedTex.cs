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
    public class SubcategorizedTex
    {
        public static readonly Texture2D DebugBuildingEditor = ContentFinder<Texture2D>.Get("Adjuster/UI/DebugBuildingEditor");
    }
}
