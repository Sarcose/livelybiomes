using System;
using System.Collections.Generic;
using Genkit;
using Genkit.SimplexNoise;
using UnityEngine;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Biomes;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneBuilders;
using XRL.World.ZoneBuilders.Utility;

using static XRL.World.GameObjectFactory;
using static XRL.World.GameObjectBlueprint;

namespace Sarcose_Biomes {
    //XRL.Messages.MessageQueue.AddPlayerMessage(message, null, false);
    [HasModSensitiveStaticCache]
    public static partial class SarcoseAddBiomes{
        [ModSensitiveCacheInit]
        public static void AddNewBiomes()
        {
            if (!XRL.World.Biomes.BiomeManager.Biomes.ContainsKey("SaltOasis"))
            {
                XRL.World.Biomes.BiomeManager.Biomes.Add("SaltOasis", new SaltOasisBiome());
            }

        }
    }

    public static partial class Utils{
        public static void sPrintc(string message){
            XRL.Messages.MessageQueue.AddPlayerMessage(message, null, false);
        }
        public static void sPrintLog(string message){
            MetricsManager.LogInfo($"====SB==== {message}");
        }

        public class ZoneBound
        {
            public int? MaxX { get; set; }
            public int? MaxY { get; set; }
            public int? MaxZ { get; set; }

            public int? MinX { get; set; }
            public int? MinY { get; set; }
            public int? MinZ { get; set; }

            // Constructor to initialize the bounds
            public ZoneBound(int? maxX = null, int? maxY = null, int? maxZ = null, int? minX = null, int? minY = null, int? minZ = null)
            {
                MaxX = maxX;
                MaxY = maxY;
                MaxZ = maxZ;
                MinX = minX;
                MinY = minY;
                MinZ = minZ;
            }

            // Method to check if a coordinate passes the bounds
            public bool IsWithin(int x, int y, int z)
            {
                if ((MaxX.HasValue && x > MaxX) || (MinX.HasValue && x < MinX))
                    return false;
                
                if ((MaxY.HasValue && y > MaxY) || (MinY.HasValue && y < MinY))
                    return false;
                
                if ((MaxZ.HasValue && z > MaxZ) || (MinZ.HasValue && z < MinZ))
                    return false;

                return true;
            }
        }


        public static bool CanBuildBiome(string ZoneID, int num, int num2, int num3, int num4, int num5, string[] TerrainAllowed = null, ZoneBound bound = null)
        {
			if (num5 < 10)
			{
				return false;
			}
            

			string objectTypeForZone = ZoneManager.GetObjectTypeForZone(num, num2, "JoppaWorld");
			if (GameObjectFactory.Factory.Blueprints.ContainsKey(objectTypeForZone) && GameObjectFactory.Factory.Blueprints[objectTypeForZone].Tags.ContainsKey("NoBiomes"))
			{
				return false;
			}
			if ((string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(ZoneID, "NoBiomes", bClampToLevel30: false, "") == "Yes")
			{
				return false;
			}
            if (TerrainAllowed != null)
            {
                bool allowed = false;
                Location2D location = new Location2D(num, num2);
                XRL.World.GameObject terrainObject = ZoneManager.GetTerrainObjectForZone(num, num2, "JoppaWorld");
                GameObjectBlueprint ZoneBlueprint = XRL.World.GameObjectFactory.Factory.GetBlueprint(terrainObject.Blueprint);
                string terrain = ZoneBlueprint.GetTag("Terrain");
                for (int i = 0; i < TerrainAllowed.Length; i++)
                {
                    if (TerrainAllowed[i] == terrain){
                        allowed = true;
                        break;
                    }

                }
                if (!allowed){
                    return false;
                    }
                
            } else {}
            if (bound != null)
            {
                XRL.World.ZoneID.Parse(ZoneID, out var World, out var ParasangX, out var ParasangY, out var ZoneX, out var ZoneY, out var ZoneZ);
                if (!bound.IsWithin(ParasangX, ParasangY, ZoneZ)) { 
                    return false;
                }
            }
            return true;
        }
    }
}

/**

Biome ideas:
    Partially settled
    Underground settlement  ~ Underground mutator (Clawed) (Albino) (Sonic)
    Interdimensional
    Cavernous -- multiple Z-levels of open air with flight enabled
    Deep Bog - Saltmarsh only (salt mummy mutator)
    Oasis - Saltdunes only
        Also: Oasis mirage
        Saltstones - hillocks of salt that go up into the air
    Pollen Filled - Flower Fields only (flower symbiote mutator?). Insects -- bees? Pollinator faction?
    Echo Chasm - Desert Canyon specific (not sure what this would do)
    Canopy/Sun-drenched and Undercanopy/Shaded - Jungle and Deep Jungle specific, generates walkable tree-ground on Strata -1 and vine-stairs to climb up to it 








**/