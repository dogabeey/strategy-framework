using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.IO;

namespace Gametator.Strategy
{
    public class TerrainUnit : MonoBehaviour
    {
        public float baseHealth;
        public float baseAttack;
        public int baseRange;

        [ReadOnly] public HexCell currentCell;



        #region Properties
        public float Health
        {
            get
            {
                return baseHealth;
            }
        }
        public float Attack
        {
            get
            {
                return baseAttack;
            }
        }
        public int Range
        {
            get
            {
                return baseRange;
            }
        }
        #endregion

        public void GoToHex(HexCell targetCell)
        {

        }

    }
}