using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Gametator.Strategy
{
	public abstract class CellTraitBase : ScriptableObject
	{
		[BoxGroup("General Settings", showLabel: true, order: 0)] public new string name;
		[Tooltip("Check if this specific trait should prevent movement entirely")]
		[BoxGroup("General Settings", showLabel: true, order: 0)] public bool preventMovement;
		[InfoBox("Multipliers affects certain properties of the units passing through them." +
			"Each value of a property (e.g Movement Speed) is added together for each trait the " +
			"terrain has. Then the total value is increased by 1 and multiplied with base value." +
			"<b> So 0 means no effect, and negative numbers mean divide instead of multiply.</b>.")]
		[BoxGroup("Multipliers", showLabel: true, order: 1)] public float movementSpeed = 0;
		[BoxGroup("Multipliers", showLabel: true, order: 1)] public float attackerPower = 0;
		[BoxGroup("Multipliers", showLabel: true, order: 1)] public float defenderPower = 0;

		public virtual void ApplyTrait(HexCell cell)
		{
			cell.preventMovement |= preventMovement;
			cell.movementSpeedMultiplier += movementSpeed;
			cell.attackerPowerMultiplier += attackerPower;
			cell.defenderPowerMultiplier += defenderPower;
		}
		public virtual void OnWalkInto(TerrainUnit unit) // TODO: Not implemented yet.
		{

		}
		public virtual void OnStayTick(TerrainUnit unit) // TODO: Not implemented yet.
		{

		}
	}
}