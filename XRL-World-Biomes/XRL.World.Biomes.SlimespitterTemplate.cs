// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.SlimespitterTemplate
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

public static class SlimespitterTemplate
{
	public static void Apply(GameObject GO)
	{
		if (GO.IsCombatObject())
		{
			GO.Slimewalking = true;
			GO.RequirePart<DisplayNameAdjectives>().AddAdjective("slime-spitting");
			GO.RequirePart<SlimespitterIconColor>();
			if (!GO.HasPart("SlimeGlands"))
			{
				GO.RequirePart<Mutations>().AddMutation(new LiquidSpitter("slime"));
			}
		}
	}
}
