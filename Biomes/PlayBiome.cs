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

    XRL.World.Biomes.BiomeManagerBiomes.Add("PlayBiome", new PlayBiome());

    public class PlayBiome : IBiome
    {

    }

}

