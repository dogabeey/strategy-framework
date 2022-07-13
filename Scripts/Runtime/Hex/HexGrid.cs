using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gametator.Strategy
{
    public class HexGrid : MonoBehaviour
    {
        public static HexGrid Instance;

        public List<HexCell> cells = new List<HexCell>();
        [TableMatrix(SquareCells = true)]
        public double[,] adjacencyMatrix;

        private void Start()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #nullable enable
        public HexCell? GetCell(int x, int y)
        {
            return cells.Find(cell => cell.xCoord == x && cell.yCoord == y);
        }
        #nullable disable
    }
}
