// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.FirethumbedTemplate
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

public static class FirethumbedTemplate
{
	public static void Apply(GameObject GO)
	{
		GO.Slimewalking = true;
		if (GO.HasBodyPart("Hand"))
		{
			GO.RequirePart<DisplayNameAdjectives>().AddAdjective("firethumbed");
			GO.RequirePart<DisplayNameColor>().SetColorByPriority("R", 10);
			GO.RequirePart<FirethumbedIconColor>();
			if (!GO.HasPart(typeof(FlamingRay)))
			{
				GO.RequirePart<Mutations>().AddMutation(new FlamingRay(), 6);
			}
		}
	}
}
