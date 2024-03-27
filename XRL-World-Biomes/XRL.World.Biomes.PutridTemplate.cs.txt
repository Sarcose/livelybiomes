// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.PutridTemplate
using XRL.World;
using XRL.World.Parts;

public static class PutridTemplate
{
	public static void Apply(GameObject GO)
	{
		GO.SetIntProperty("Putrid", 1);
		if (GO.pRender != null && GO != null && GO.HasPart("Body"))
		{
			GO.pRender.DisplayName = "putrid " + GO.pRender.DisplayName;
			GO.pRender.ColorString = "&g^W";
			GO.AddPart(new VomitOnHit());
		}
		if (GO.HasStat("Hitpoints"))
		{
			GO.Statistics["Hitpoints"].BaseValue *= 2;
		}
		if (GO.HasStat("AV"))
		{
			GO.Statistics["AV"].BaseValue += 3;
		}
		GO.AddPart(new SpawnOnDeath("Bloatfly"));
	}
}
