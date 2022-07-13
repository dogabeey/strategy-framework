using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Gametator.Strategy
{

	[CreateAssetMenu(fileName = "New Terrain Type",menuName = "Gametator/Strategy Framework/Cells/New Terrain Type...")]
	public class CellTerrain : CellTraitBase
	{
        [BoxGroup("General Settings", showLabel: true, order: 0)] public Material terrainMaterial;

        public override void ApplyTrait(HexCell cell)
        {
            base.ApplyTrait(cell);

            if (cell.hexagonMesh) cell.hexagonMesh.material = terrainMaterial;
            if (cell.squareMesh) cell.squareMesh.material = terrainMaterial;

        }
    }
}