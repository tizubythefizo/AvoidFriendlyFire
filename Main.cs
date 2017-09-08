﻿using System.Collections.Generic;
using System.Linq;
using HugsLib.Utils;
using RimWorld;
using Verse;

namespace AvoidFriendlyFire
{
    public class Main : HugsLib.ModBase
    {
        public override string ModIdentifier => "AvoidFriendlyFire";

        internal static Main Instance { get; private set; }

        internal new ModLogger Logger => base.Logger;

        private FireConeOverlay _fireConeOverlay;

        private ExtendedDataStorage _extendedDataStorage;

        private FireManager _fireManager;

        public Main()
        {
            Instance = this;
        }

        public override void Tick(int currentTick)
        {
            base.Tick(currentTick);
            _fireManager?.RemoveExpiredCones(currentTick);
        }

        public override void WorldLoaded()
        {
            base.WorldLoaded();
            _extendedDataStorage =
                UtilityWorldObjectManager.GetUtilityWorldObject<ExtendedDataStorage>();
        }

        public override void MapLoaded(Map map)
        {
            base.MapLoaded(map);
            _fireManager = new FireManager();
            _fireConeOverlay = new FireConeOverlay();
        }

        public void UpdateFireConeOverlay(bool enabled)
        {
            if (_fireConeOverlay == null)
                return;

            _fireConeOverlay.Update(enabled);
        }

        public static Pawn GetSelectedDraftedPawn()
        {
            List<object> selectedObjects = Find.Selector.SelectedObjects;
            if (selectedObjects == null || selectedObjects.Count != 1)
                return null;

            var pawn = selectedObjects.First() as Pawn;
            if (pawn == null || !pawn.Drafted)
                return null;

            return pawn;
        }

        public ExtendedDataStorage GetExtendedDataStorage()
        {
            return _extendedDataStorage;
        }

        public FireManager GetFireManager()
        {
            return _fireManager;
        }
    }
}
