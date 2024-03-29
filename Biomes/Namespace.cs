using System;
using Genkit.SimplexNoise;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Biomes;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneBuilders;

[HasGameBasedStaticCache]
namespace Sarcose_Biomes {
    //XRL.Messages.MessageQueue.AddPlayerMessage(message, null, false);

    public static partial class Utils{
        public static void sPrintc(string message){
            XRL.Messages.MessageQueue.AddPlayerMessage(message, null, false);
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
            public ZoneBound(int? maxX, int? maxY, int? maxZ, int? minX, int? minY, int? minZ)
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


        public static bool CanBuildBiome(string ZoneID, string[] TerrainTypes = {}, int[] StrataRange = {}, ZoneBound bound)
        {
            if (!ZoneID.Contains("."))
			{
				return false;
			}
			string[] array = ZoneID.Split('.');
			int num = Convert.ToInt32(array[1]);
			int num2 = Convert.ToInt32(array[2]);
			int num3 = Convert.ToInt32(array[3]);
			int num4 = Convert.ToInt32(array[4]);
			int num5 = Convert.ToInt32(array[5]);
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
            for (int i = 0; i < TerrainTypes.Length; i++)
            {
                string Terrain = TerrainTypes[i];
                //get terrain type and test against terrain type
            }
            if ((!StrataRange.Length == 0)){
                //test if our Strata Range is valid
            }
            //TODO: test what var type it is and verify it's compatible with int
            //TODO: decide whether ParasangX vs ZoneX is worth considering. Psychic uses ParasangX but ZoneZ -- clearly strata is more... stratified than X/Y
            XRL.World.ZoneID.Parse(ZoneID, out var World, out var ParasangX, out var ParasangY, out var ZoneX, out var ZoneY, out var ZoneZ)
            if (!bound.IsWithin(ParasangX, ParasangY, ZoneZ)) { return false; }

            return true;
        }
    }


    //XRL.World.Biomes.BiomeManagerBiomes.Add("PlayBiome", new PlayBiome());

    public class PlayBiome : IBiome
    {
        [NonSerialized]
        [GameBasedStaticCache(true, false, CreateInstance = false)] //this is verbatim at the top of every biome declaration.
    	public static byte[,,] BiomeLevels; //this is defined prior to all biomes and seems to be set prior to calling GetBiomeValue()

        public override int GetBiomeValue(string ZoneID)
        {   //this seems to seed the zone. It seeds the zone to the biome manager, while also seeding the zone for internal logic
            sPrintc("GetBiomeValue run");
            //for instance, it sets biomeValue in ZoneID which is used in MutateZone
            bool cannotBuild = false; //some evaluatio9n
            //(!ZoneID.Contains(".")) -- if the ZoneID *doesn't* have a . or in other words isn't an address? Maybe?
            //if it has certain incompatibilities like terrain type or parasang type or strata level?
            if (cannotBuild){
                return 0;
            }
        }

        public override void MutateZone(Zone Z)
        {   //this is definitely the bulk of the work here. We have builders being added etc
            sPrintc("MutateZone run");
            int biomeValue = GetBiomeValue(Z.ZoneID);
            //FungalJungle.FungalUpAZone is a function that Fungal uses specifically to populate a zone, probably heavily.
            if (Z.GetZoneProperty("relaxedbiomes") != "true")
            {   //variety evaluation
                if (biomeValue == 1)
                {
                    //add zone builders to change the terrain
                    //new PopTableZoneBuilder().BuildZone(Z, "Slimy1");
                }
                if (biomeValue == 2)
                {
                    //new PopTableZoneBuilder().BuildZone(Z, "Slimy2");
                }
                if (biomeValue == 3)
                {
                    //new PopTableZoneBuilder().BuildZone(Z, "Slimy3");
                }
            }
            if (biomeValue >= 1) //some kind of sound evaluation.
            {
                //Z.SetZoneProperty("ambient_bed_2", "Sounds/Ambiences/amb_biome_slimy");
            }
            //add liquid pools if applicable
            //new LiquidPools().BuildZone(Z, "SlimePuddle", 3 * GetBiomeValue(Z.ZoneID) - 4, "0", "Slimy Plants");

            //mutate zone inhabitants. For instance, psychic, rusty, etc
            //see the respective biomes for examples. It spawns GameObjects manually if needed




        }

        public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
        {
            sPrintc("MutateZoneName run");
        }

        public override GameObject MutateGameObject(GameObject Object, string ZoneID)
        {
            sPrintc("MutateGameObject run");
            //use templates to mutate objects.
        }

        public override bool IsNotable(string ZoneID)
        {   //probably determines whether or not this shows in the journal - it looks like this is set to false everywhere but rusty
            //and rusty biomes do seem to be notable...
            return false;
        }
    }

}




/**
PlayBiome testing:


    Different shapes of blocks

**/



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