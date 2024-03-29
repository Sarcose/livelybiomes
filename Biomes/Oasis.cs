using System;
using Genkit.SimplexNoise;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Biomes;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneBuilders;
using static Sarcose_Biomes.Utils;

[HasGameBasedStaticCache]
namespace Sarcose_Biomes {

    public class SaltOasisBiome : IBiome
    {
        [NonSerialized]
        [GameBasedStaticCache(true, false, CreateInstance = false)] //this is verbatim at the top of every biome declaration.

    	public static byte[,,] BiomeLevels;


        public override int GetBiomeValue(string ZoneID)
        {  
            bool CanBuild = CanBuildBiome(ZoneID, TerrainTypes: new string[] {"SaltDunes"}, StrataRange: new int[] {0,10}, new ZoneBound(maxX: 100, maxY: 200)); // basic checks for all Biomes 
            
            //test other biome specific disqualifiers if they apply
            if (!CanBuild){
                return 0;
            }

            //now build the seed to return
            int num = 1; //some amount
            return num; //we can just test if returning any number other than 0 works to generate the biome.

        }

        public override void MutateZone(Zone Z)
        {   //this is definitely the bulk of the work here. We have builders being added etc
            sPrintc("MutateZone run");
            int biomeValue = GetBiomeValue(Z.ZoneID);
            //FungalJungle.FungalUpAZone is a function that Fungal uses specifically to populate a zone, probably heavily.
            if (Z.GetZoneProperty("relaxedbiomes") != "true")   //this check seems to be in every one. relaxedbiomes seems to remove builders, perhaps making it less intense
            {   //variety and extra builder evaluation
                if (biomeValue == 1)
                {
                    //add zone builders to change the terrain
                    //add extra oasis stuff
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

            //actual oasis stuff:
            //add 1-2 saltwater pools
            //add 1-2 brackish pools
            //add 1 pool of freshwater with a small amount of water/tiles
            //add palm trees. Probably not angry palms. Cactus? Flowers? Coconuts?
            //high chance for dromad traders maybe?


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
