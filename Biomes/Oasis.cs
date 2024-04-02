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
            //'JoppaWorld.4.22.1.0.10'
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
                LayeredNoise layeredNoise = LayeredNoise.CreateLinearOctiveLayers(3, 1.33f, 0.12f, XRLCore.Core.Game.GetWorldSeed("OasisNoise").ToString());    //here the label provided is only a label for consistency.
                float num12 = 0.54f;    //this is a density threshold. Fungal uses 0.72f. Other biomes use 0.66f. For Oasis we want it to be sparse, so we're using 0.54f
                float num13 = 0f;
                float num14 = 1f;
                int num16 = 4;          // Biome levels. This controls how fine the detail is in the biome, increasing or decreasing the variations between generations. Most use 4.
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

        //NOTE ABOUT GETBIOMEVALUE:
        /*
        MutateZone and MutateZoneName determine the actual Biome applied and whether a Biome is applied at all. biomeValue 1, 2, 3 are used for everything. 3 is the rarest.

        In other words: 
        biomeValue >= 0 everywhere, probably literally every single parasang
        biomeValue >= 1 common, honestly too common for my taste
        biomeValue >= 2 uncommon
        biomeValue >= 3 rare
        rhetorically, let's bump that up to:
        biomeValue == 1 uncommon
        biomeValue == 2 very uncommon
        biomeValue == 3 very rare
        
        extrapolating:

            for a rare biome we want the MutateZone to only act on 2 or 3 or higher, and MutateZoneName to only provide adjective/name 2 or 3 for instance. For the Oasis, we will use 3 only.

        */

        public override void MutateZone(Zone Z)
        { 
            int biomeValue = GetBiomeValue(Z.ZoneID);
            //FungalJungle.FungalUpAZone is a function that Fungal uses specifically to populate a zone, probably heavily.
            if (Z.GetZoneProperty("relaxedbiomes") != "true")   //this check seems to be in every one. relaxedbiomes seems to remove builders, perhaps making it less intense
            {   //variety and extra builder evaluation
                //here we are choosing different populations.
                if (biomeValue == 2)        //oasis rarity
                {
                    //new PopTableZoneBuilder().BuildZone(Z, "Slimy3");
                    //add a population table to it
                    //decentish chests
                }
                if (biomeValue == 3)        //oasis rarity
                {
                    //new PopTableZoneBuilder().BuildZone(Z, "Slimy3");
                    //add a population table to it
                    //probably add freshwater to this one specifically
                    //rare chests
                }
            }
            if (biomeValue >= 2) //for every oasis, probably liquidpools
            {
                //new LiquidPools().BuildZone(Z, "SlimePuddle", 3 * GetBiomeValue(Z.ZoneID) - 4, "0", "Slimy Plants");
                //palm trees
                //saltwater pools, brackish pools
                //many wild animals, probably grazing hedonists chillin here. Mechanmimists stopping over.
            }
        }

        public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
        {
            return IBiome.MutateZoneNameWith(Name, GetBiomeValue(ZoneID), null, null, null, "hidden", "oasis", "refreshing");
        }

        public override GameObject MutateGameObject(GameObject Object, string ZoneID)
        {
            return Object;  //I don't think oasis should have mutated creatures tbh
        }

        public override bool IsNotable(string ZoneID)  
        { 
            if (GetBiomeValue(ZoneID) >= 3){return true;}
            return false;    
        }

        

    }

}
