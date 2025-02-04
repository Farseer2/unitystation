﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main health scanner interaction. Applying it to a living thing sends a request to the server to
/// tell us their health info.
/// </summary>
public class HealthScanner : NBHandApplyInteractable
{

	protected override bool WillInteract(HandApply interaction, NetworkSide side)
	{
		if (!base.WillInteract(interaction, side)) return false;
		//can only be applied to LHB
		if (!Validations.HasComponent<LivingHealthBehaviour>(interaction.TargetObject)) return false;
		return true;
	}

	protected override void ServerPerformInteraction(HandApply interaction)
	{
		var livingHealth = interaction.TargetObject.GetComponent<LivingHealthBehaviour>();
		string ToShow = (livingHealth.name + " is " + livingHealth.ConsciousState.ToString() + "\n"
		                 + "OverallHealth = " + livingHealth.OverallHealth.ToString() + " Blood level = " + livingHealth.bloodSystem.BloodLevel.ToString() + "\n"
		                 + "Blood levels = " + livingHealth.CalculateOverallBloodLossDamage() + "\n");
		string StringBuffer = "";
		float TotalBruteDamage = 0;
		float TotalBurnDamage = 0;
		foreach (BodyPartBehaviour BodyPart in livingHealth.BodyParts)
		{
			StringBuffer += BodyPart.Type.ToString() + "\t";
			StringBuffer += BodyPart.BruteDamage.ToString() + "\t";
			TotalBruteDamage += BodyPart.BruteDamage;
			StringBuffer += BodyPart.BurnDamage.ToString();
			TotalBurnDamage += BodyPart.BurnDamage;
			StringBuffer += "\n";
		}
		ToShow = ToShow + "Overall, Brute " + TotalBruteDamage.ToString() + " Burn " + TotalBurnDamage.ToString() + " OxyLoss " + livingHealth.bloodSystem.OxygenDamage.ToString() + "\n" + "Body Part, Brute, Burn \n" + StringBuffer;
		UpdateChatMessage.Send(interaction.Performer, ChatChannel.Examine, ToShow);

	}
}
