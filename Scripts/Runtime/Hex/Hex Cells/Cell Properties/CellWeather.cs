using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Gametator.Strategy
{

	[CreateAssetMenu(fileName = "New Weather",menuName = "Gametator/Strategy Framework/Cells/New Weather...")]
	public class CellWeather : CellTraitBase
	{
        [BoxGroup("General Settings", showLabel: true, order: 0)] public ParticleSystem particleSystem;

        public override void ApplyTrait(HexCell cell)
        {
            base.ApplyTrait(cell);


            Instantiate(particleSystem, cell.transform);
        }
    }
}