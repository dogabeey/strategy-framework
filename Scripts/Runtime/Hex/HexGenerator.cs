using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Reflection;

namespace Gametator.Strategy
{
    public class HexGenerator : MonoBehaviour
    {
        const float threeSqrt = 1.73205f;
        const float hexScaleMultiplier = 1.127441f;
        const float squareScaleMultiplier = 0.95f;

        [OnValueChanged(nameof(Generate))] [Range(1, 20)] public int width, height;
        [OnValueChanged(nameof(Generate))] public float cellDistance;
        [OnValueChanged(nameof(Generate))] public float cellScale = 1;
        [OnValueChanged(nameof(Generate))] public bool isHexagon = true;
        [Space]
        [AssetsOnly] public HexGrid gridPrefab;
        [AssetsOnly] public HexCell cellPrefab;

        HexGrid hexGrid;
        //public bool higherZStart;

        public float XDistance
        {
            get => cellDistance;
        }
        public float YDistance
        {
            get => isHexagon ? XDistance * threeSqrt / 2 : XDistance;
        }

        // Start is called before the first frame update
        void Start()
        {
            //Generate();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Generate()
        {
            if (hexGrid) DestroyImmediate(hexGrid.gameObject, true);

            hexGrid = Instantiate(gridPrefab, transform);
            List<HexCell> cells = new List<HexCell>();
            // Her bir hücreyi teker teker traverse ederek  hücreleri oluşturuyoruz.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Hücreyi oluşturacağımız pozisyonu hesaplıyoruz
                    int isEven = i % 2 == 0 ? 0 : 1;
                    Vector3 position = new Vector3(
                         x: i * YDistance,
                         y: 0,
                         z: j * XDistance - (isHexagon ? isEven * (XDistance / 2) : 0)
                    );
                    // Hücreyi oluşturup parametrelerini belirliyoruz.
                    HexCell cell = Instantiate(cellPrefab, position, cellPrefab.transform.rotation);
                    cell.transform.SetParent(hexGrid.transform, false);
                    cell.hexagonMesh.gameObject.SetActive(isHexagon);
                    cell.squareMesh.gameObject.SetActive(!isHexagon);
                    cell.transform.localScale *= cellScale * (isHexagon ? hexScaleMultiplier : hexScaleMultiplier * squareScaleMultiplier);
                    cell.xCoord = i;
                    cell.yCoord = j + (i % 2 == 1 && isHexagon ? 0.5f : 0); // Eğer hexagon ise, bütün tek sayılı x'lerde y'ye 0.5 ekle.
                    cell.isHexagon = isHexagon;

                    HexCell.ApplyTraits(cell);

                    cells.Add(cell);
                }
            }
            // Bütün hücre değerlerini grid'e atıyoruz.
            hexGrid.cells = cells;
            // Komşuluk matrisini oluşturuyoruz.
            hexGrid.adjacencyMatrix = new double[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    hexGrid.adjacencyMatrix[i,j] = hexGrid.cells[i].IsNeighborOf(hexGrid.cells[j]) ? 1 : 0;
                }
            }
        }
    }
}
