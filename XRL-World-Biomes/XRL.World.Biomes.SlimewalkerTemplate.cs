// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.SlimewalkerTemplate
using XRL.World;
using XRL.World.Parts;

public static class SlimewalkerTemplate
{
	public static void Apply(GameObject GO)
	{
		if (GO.IsCombatObject())
		{
			GO.Slimewalking = true;
			if (GO.HasBodyPart("Foot") || GO.HasBodyPart("Feet"))
			{
				GO.RequirePart<DisplayNameAdjectives>().AddAdjective("web-toed");
			}
			else
			{
				GO.RequirePart<DisplayNameAdjectives>().AddAdjective("slimy-finned");
			}
			GO.RequirePart<SlimewalkerIconColor>();
		}
	}
}
