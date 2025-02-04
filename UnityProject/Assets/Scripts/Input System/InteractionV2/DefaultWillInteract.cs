
/// <summary>
/// Util class containing the default WIllinteract logic for each interaction type
/// </summary>
public static class DefaultWillInteract
{
	/// <summary>
	/// Default Willinteract logic for the given interaction type T.
	///
	/// Chooses and invokes DefaultWillInteract method corresponding to the supplied type.
	/// </summary>
	/// <param name="interaction">interaction to check</param>
	/// <param name="side">side of the network this is being checked on</param>
	/// <typeparam name="T">type of interaction</typeparam>
	/// <returns></returns>
	public static bool Default<T>(T interaction, NetworkSide side) where T : Interaction
	{
		if (typeof(T) == typeof(PositionalHandApply))
		{
			return PositionalHandApply(interaction as PositionalHandApply, side);
		}
		else if (typeof(T) == typeof(HandApply))
		{
			return HandApply(interaction as HandApply, side);
		}
		else if (typeof(T) == typeof(AimApply))
		{
			return AimApply(interaction as AimApply, side);
		}
		else if (typeof(T) == typeof(MouseDrop))
		{
			return MouseDrop(interaction as MouseDrop, side);
		}
		else if (typeof(T) == typeof(HandActivate))
		{
			return HandActivate(interaction as HandActivate, side);
		}
		else if (typeof(T) == typeof(InventoryApply))
		{
			return InventoryApply(interaction as InventoryApply, side);
		}
		Logger.LogError("Unable to recognize interaction type.");
		return false;
	}

	/// <summary>
	/// Default WillInteract logic for InventoryApply
	/// </summary>
	public static bool InventoryApply(InventoryApply interaction, NetworkSide side)
	{
		return Validations.CanInteract(interaction.Performer, side);
	}

	/// <summary>
	/// Default WillInteract logic for Activate
	/// </summary>
	public static bool HandActivate(HandActivate interaction, NetworkSide side)
	{
		return Validations.CanInteract(interaction.Performer, side);
	}

	/// <summary>
	/// Default WillInteract logic for MouseDrop
	/// </summary>
	public static bool MouseDrop(MouseDrop interaction, NetworkSide side)
	{
		return Validations.CanApply(interaction, side);
	}

	/// <summary>
	/// Default WillInteract logic for AimApply
	/// </summary>
	public static bool AimApply(AimApply interaction, NetworkSide side)
	{
		return Validations.CanInteract(interaction.Performer, side);
	}

	/// <summary>
	/// Default WillInteract logic for HandApply
	/// </summary>
	public static bool HandApply(HandApply interaction, NetworkSide side)
	{
		return Validations.CanApply(interaction, side);
	}

	/// <summary>
	/// Default WIllInteract logic for PositionalHandApply interactions
	/// </summary>
	public static bool PositionalHandApply(PositionalHandApply interaction, NetworkSide side)
	{
		return Validations.CanApply(interaction, side);
	}

}
