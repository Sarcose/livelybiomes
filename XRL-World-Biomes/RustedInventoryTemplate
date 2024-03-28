// Assembly-CSharp, Version=2.0.206.71, Culture=neutral, PublicKeyToken=null
// XRL.World.Biomes.RustedInventoryTemplate
using XRL.World;
using XRL.World.Effects;

public static class RustedInventoryTemplate
{
	private static void ApplyMetalRust(GameObject Object)
	{
		if (Object.HasPart("Metal") && 50.in100())
		{
			Object.ApplyEffect(new Rusted());
		}
	}

	public static void Apply(GameObject GO)
	{
		if (GO?.pRender != null)
		{
			GO.Body?.ForeachEquippedObject(ApplyMetalRust);
			GO.Inventory?.ReverseForeachObject(ApplyMetalRust);
		}
	}
}
