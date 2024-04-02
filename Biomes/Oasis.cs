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


namespace Sarcose_Biomes {

    [HasGameBasedStaticCache]
    public class SaltOasisBiome : IBiome
    {
        [NonSerialized]
        [GameBasedStaticCache(true, false, CreateInstance = false)] //this is verbatim at the top of every biome declaration.

    	public static byte[,,] BiomeLevels;

        public override int GetBiomeValue(string ZoneID)
        {  
            if (!ZoneID.Contains("."))
			{
				return 0;
			}
            string[] array = ZoneID.Split('.');
			int num = Convert.ToInt32(array[1]);
			int num2 = Convert.ToInt32(array[2]);
			int num3 = Convert.ToInt32(array[3]);
			int num4 = Convert.ToInt32(array[4]);
			int num5 = Convert.ToInt32(array[5]);
            int num6 = 240;
			int num7 = 75;
			int num8 = 10;
			int num9 = num * 3 + num3;
			int num10 = num2 * 3 + num4;
			int num11 = num5 % num8;

            bool CanBuild = CanBuildBiome(ZoneID, num, num2, num3, num4, num5, TerrainAllowed: new string[] { "Saltdunes" }, bound: new ZoneBound(maxZ: 10));

            //test other biome specific disqualifiers if they apply
            if (!CanBuild){
                return 0;
            }
            if (BiomeLevels == null)
            {
                BiomeLevels = new byte[num6, num7, num8];
                LayeredNoise layeredNoise = LayeredNoise.CreateLinearOctiveLayers(3, 1.33f, 0.12f, XRLCore.Core.Game.GetWorldSeed("RustyNoise").ToString());
                float num12 = 0.66f;
                float num13 = 0f;
                float num14 = 1f;
                float[,,] array2 = layeredNoise.Generate3D(num6, num7, num8);
                for (int i = 0; i < num8; i++)
                {
                    for (int j = 0; j < num7; j++)
                    {
                        for (int k = 0; k < num6; k++)
                        {
                            float num15 = array2[k, j, i];
                            num15 = (array2[k, j, i] = ((!(num15 < num12)) ? ((num15 - num12) * (1f / (1f - num12))) : 0f));
                            if (num15 > num13)
                            {
                                num13 = num15;
                            }
                            if (num15 < num14)
                            {
                                num14 = num15;
                            }
                        }
                    }
                }
                for (int l = 0; l < num8; l++)
                {
                    for (int m = 0; m < num7; m++)
                    {
                        for (int n = 0; n < num6; n++)
                        {
                            int num16 = 4;
                            int num17 = (int)((array2[n, m, l] - num14) / ((num13 - num14) / (float)num16));
                            if (num17 < 0)
                            {
                                num17 = 0;
                            }
                            if (num17 > num16 - 1)
                            {
                                num17 = num16 - 1;
                            }
                            BiomeLevels[n, m, l] = (byte)num17;
                        }
                    }
                }
            }
            return BiomeLevels[num9, num10, num11];
        }

        public override void MutateZone(Zone Z)
        {   //this is definitely the bulk of the work here. We have builders being added etc
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
            new LiquidPools().BuildZone(Z, "SlimePuddle", 3 * GetBiomeValue(Z.ZoneID) - 4, "0", "Slimy Plants");

            //mutate zone inhabitants. For instance, psychic, rusty, etc
            //see the respective biomes for examples. It spawns GameObjects manually if needed




        }

        public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
        {
            return IBiome.MutateZoneNameWith(Name, GetBiomeValue(ZoneID), "oasis", "hidden", "secret pool", "sequestered", "oasis", null);
        }

        public override GameObject MutateGameObject(GameObject Object, string ZoneID)
        {
         //   sPrintc("MutateGameObject run");
            //use templates to mutate objects.
            return Object;  //do not mutate for now
        }

        public override bool IsNotable(string ZoneID)  
        { 
            if (GetBiomeValue(ZoneID) > 0){return true;}
            return false;    
        }

        

    }

}
