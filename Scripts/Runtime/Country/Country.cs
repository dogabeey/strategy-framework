using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Gametator.Strategy
{
	[CreateAssetMenu(fileName = "New Country", menuName = "Gametator/Strategy Framework/Country/New Country...")]
	public class Country : ScriptableObject
	{
		public string countryName;
		public Color mapColor;
	}
}