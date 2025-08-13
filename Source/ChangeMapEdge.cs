
using System;
using Verse;
using UnityEngine;
using RimWorld;

namespace ChangeMapEdge
{
    public class ChangeMapEdgeSettings : ModSettings
    {
        public int noBuildEdge = 0;
        public int noZoneEdge = 0;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref noBuildEdge, "noBuildEdge");
            Scribe_Values.Look(ref noZoneEdge, "noZoneEdge");

            base.ExposeData();
        }
    }

    public class ChangeMapEdge : Mod
    {
        ChangeMapEdgeSettings settings;
        string noBuildBuffer = "";
        string noZoneBuffer = "";

        static ChangeMapEdge Instance;

        static public int GetNoBuildLimit() => Instance.settings.noBuildEdge;
        static public int GetNoZoneLimit() => Instance.settings.noZoneEdge;

        public ChangeMapEdge(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ChangeMapEdgeSettings>();
            noBuildBuffer = settings.noBuildEdge.ToString();
            noZoneBuffer = settings.noZoneEdge.ToString();

            Instance = this;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            Rect lineRectBuild = listingStandard.GetRect(Text.LineHeight);
            Widgets.DrawHighlightIfMouseover(lineRectBuild);
            TooltipHandler.TipRegion(lineRectBuild, "Restrict building to X tiles from the edge. Game default: 10");
            Widgets.TextFieldNumericLabeled<int>(lineRectBuild, "No-build edge size", ref settings.noBuildEdge, ref noBuildBuffer, 0, 20);

            listingStandard.Gap();

            Rect lineRectZone = listingStandard.GetRect(Text.LineHeight);
            Widgets.DrawHighlightIfMouseover(lineRectZone);
            TooltipHandler.TipRegion(lineRectZone, "Restrict zones to X tiles from the edge. Game default: 5");
            Widgets.TextFieldNumericLabeled<int>(lineRectZone, "No-zone edge size", ref settings.noZoneEdge, ref noZoneBuffer, 0, 20);

            listingStandard.End();

            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "ChangeMapEdge";
        }

    }
}
