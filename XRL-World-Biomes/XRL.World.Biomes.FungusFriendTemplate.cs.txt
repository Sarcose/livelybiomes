// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.FungusFriendTemplate
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

public static class FungusFriendTemplate
{
	public static void Apply(GameObject GO, string InfectionBlueprint, Zone Z)
	{
		if (GO?.pRender != null && GO.HasPart("Body"))
		{
			GO.RequirePart<SocialRoles>().RequireRole("friend to fungi");
			GO.RequirePart<FungiFriendIconColor>();
			if (33.in100())
			{
				FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint);
			}
			if (15.in100())
			{
				FungalSporeInfection.ApplyFungalInfection(GO, InfectionBlueprint);
			}
			if (!Z.GetZoneProperty("relaxedbiomes").EqualsNoCase("true") && GO.pBrain != null)
			{
				GO.pBrain.FactionMembership["Fungi"] = 100;
			}
		}
	}
}
