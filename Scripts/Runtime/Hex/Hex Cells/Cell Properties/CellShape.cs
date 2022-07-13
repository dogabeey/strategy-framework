using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Gametator {
	[CreateAssetMenu(fileName = "New Terrain Shape",menuName = "Gametator/Strategy Framework/Cells/New Terrain Shape...")]
	public class CellShape : CellTraitBase
	{
        [BoxGroup("General Settings", showLabel: true, order: 0)] public MeshRenderer shapeMesh;
        [BoxGroup("General Settings", showLabel: true, order: 0)] public Vector3 positionOffset;


        public override void ApplyTrait(HexCell cell)
		{
			base.ApplyTrait(cell);

            if (cell.terrainShape)
            {
                if (cell.terrainShape.shapeMesh)
                {
                    // Removing previous shape
                    if (cell.shapeInstance) DestroyImmediate(cell.shapeInstance.gameObject);

                    // Creating shape
                    Debug.Log("Generating shape");
                    cell.shapeInstance = Instantiate(cell.terrainShape.shapeMesh.gameObject,
                        cell.transform.position + cell.terrainShape.positionOffset,
                        Quaternion.identity,
                        cell.transform).
                        GetComponent<MeshRenderer>();

                    if (cell.terrain.terrainMaterial)
                    {
                        // Adding terrain material
                        cell.shapeInstance.material = cell.terrain.terrainMaterial;
                    }
                }
            }
        }
	}
}