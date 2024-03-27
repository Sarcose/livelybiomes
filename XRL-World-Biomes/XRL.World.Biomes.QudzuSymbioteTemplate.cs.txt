// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.QudzuSymbioteTemplate
using XRL.World;
using XRL.World.Parts;

public static class QudzuSymbioteTemplate
{
	public static void Apply(GameObject GO, Zone Z)
	{
		if (GO?.pRender != null && GO.HasPart("Body") && !GO.GetBlueprint().DescendsFrom("BaseRobot"))
		{
			GO.AddPart(new RustOnHit());
			GO.RequirePart<SocialRoles>().RequireRole("{{r|qudzu}} symbiote");
			GO.RequirePart<QudzuSymbioteIconColor>();
			if (GO.pBrain != null && !Z.GetZoneProperty("relaxedbiomes").EqualsNoCase("true"))
			{
				GO.pBrain.FactionMembership["Vines"] = 100;
			}
		}
	}
}
