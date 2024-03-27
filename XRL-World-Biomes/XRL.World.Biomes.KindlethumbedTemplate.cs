// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.KindlethumbedTemplate
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

public static class KindlethumbedTemplate
{
	public static void Apply(GameObject GO)
	{
		GO.Slimewalking = true;
		if (GO.HasBodyPart("Hand"))
		{
			GO.RequirePart<DisplayNameAdjectives>().AddAdjective("kindlethumbed");
			GO.RequirePart<DisplayNameColor>().SetColorByPriority("r", 10);
			GO.RequirePart<KindlethumbedIconColor>();
			if (!GO.HasPart(typeof(FlamingRay)))
			{
				GO.RequirePart<Mutations>().AddMutation(new FlamingRay());
			}
		}
	}
}
