using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Gametator {

	[CreateAssetMenu(fileName = "New Terrain Feature",menuName = "Gametator/Strategy Framework/Cells/New Terrain Feature...")]
	public class CellFeature : CellTraitBase
	{
		[BoxGroup("General Settings", showLabel: true, order: 0)] public Mesh featureMesh;
		[BoxGroup("General Settings", showLabel: true, order: 0)] public Vector3 positionOffset;

		public override void ApplyTrait(HexCell cell)
		{
			base.ApplyTrait(cell);
		}
	}
}