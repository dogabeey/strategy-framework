using System;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.IO;
using System.Reflection;
using Elsheimy.Components.Linears;
using EPOOutline;

namespace Gametator.Strategy
{
    public class HexCell : MonoBehaviour, IEqualityComparer
    {
        [LabelText("Coordinates:")]
        [HideLabel] [ReadOnly] [HorizontalGroup("Coordinates")] public float xCoord;
        [HideLabel] [ReadOnly] [HorizontalGroup("Coordinates")] public float yCoord;
        #region Warning
        [InfoBox("No country assets found. Under any Resources folder, create a Country asset " +
            "by right-clicking on Project tab and clicking Create->Gametator->Strategy Framework->Country>" +
            "New Country", VisibleIf = nameof(HasNoCountry), InfoMessageType = InfoMessageType.Warning)]
        #endregion
        [ValueDropdown(nameof(GetAllCountries))] [OnValueChanged(nameof(SetOutline))]
        public Country owner;
        [Space]
        [SceneObjectsOnly] public MeshRenderer hexagonMesh;
        [SceneObjectsOnly] public MeshRenderer squareMesh;
        [Space]
        #region Warning
        [InfoBox("No cell terrain assets found. Under any Resources folder, create a Cell Terrain asset " +
            "by right-clicking on Project tab and clicking Create->Gametator->Strategy Framework->Cells>" +
            "New Terrain Type", VisibleIf = nameof(HasNoCellTerrains), InfoMessageType = InfoMessageType.Warning)]
        #endregion
        [ValueDropdown(nameof(GetAllCellTerrains))] [OnValueChanged(nameof(OnChangedTerrain))]
        public CellTerrain terrain;
        #region Warning
        [InfoBox("No cell shape assets found. Under any Resources folder, create a Cell Shape asset " +
            "by right-clicking on Project tab and clicking Create->Gametator->Strategy Framework->Cells>" +
            "New Terrain Shape", VisibleIf = nameof(HasNoCellShapes), InfoMessageType = InfoMessageType.Warning)]
        #endregion
        [ValueDropdown(nameof(GetAllCellShapes))] [OnValueChanged(nameof(OnChangedShape))]
        public CellShape terrainShape;
        #region Warning
        [InfoBox("No cell feature assets found. Under any Resources folder, create a Cell Feature asset " +
            "by right-clicking on Project tab and clicking Create->Gametator->Strategy Framework->Cells>" +
            "New Terrain Feature", VisibleIf = nameof(HasNoCellFeatures), InfoMessageType = InfoMessageType.Warning)]
        #endregion
        [ValueDropdown(nameof(GetAllCellFeatures))] [OnValueChanged(nameof(OnChangedFeature))]
        public CellFeature terrainFeature;

        [HideInInspector] public MeshRenderer shapeInstance;
        [HideInInspector] public bool isHexagon;
        [ReadOnly] public bool preventMovement = false;
        [ReadOnly] public float movementSpeedMultiplier = 0;
        [ReadOnly] public float attackerPowerMultiplier = 0;
        [ReadOnly] public float defenderPowerMultiplier = 0;

        private void Start()
        {
            SetOutline();
        }

        private void SetOutline()
        {
            Outlinable outlinable = GetComponent<Outlinable>();
            outlinable.AddAllChildRenderersToRenderingList(RenderersAddingMode.MeshRenderer);
            outlinable.enabled = owner;
            if(owner)
            {
                outlinable.OutlineParameters.Color = owner.mapColor;
                outlinable.OutlineParameters.FillPass.SetColor("_PublicColor",
                    new Color(
                        owner.mapColor.r,
                        owner.mapColor.b,
                        owner.mapColor.g,
                        0.2f                //Alpha
                    )
                );
            }
        }

        public bool IsNeighborOf(HexCell hexCell)
        {
            if(isHexagon)
            {
                return Mathf.Abs(this.xCoord - hexCell.xCoord) <= 1 && Mathf.Abs(this.yCoord - hexCell.yCoord) <= 1 && !this.preventMovement && !hexCell.preventMovement;
            }
            else
            {
                return Mathf.Abs(this.xCoord - hexCell.xCoord) + Mathf.Abs(this.yCoord - hexCell.yCoord) == 1 && !this.preventMovement && !hexCell.preventMovement;
            }
        }
        public static bool AreNeighbors(HexCell cell1, HexCell cell2)
        {
            return cell1.IsNeighborOf(cell2);
        }
        public static bool AreNeighborsByRange(HexGrid currentGrid, HexCell cell1, HexCell cell2, int range)
        {
            double[,] poweredAdj = MatrixFunctions.Power(currentGrid.adjacencyMatrix, range);
            int cell1Index = currentGrid.cells.IndexOf(cell1);
            int cell2Index = currentGrid.cells.IndexOf(cell2);
            return poweredAdj[cell1Index, cell2Index] > 0;
        }

        #region System
        public new bool Equals(object x, object y)
        {
            HexCell cell1 = (HexCell)x;
            HexCell cell2 = (HexCell)y;

            return cell1.xCoord == cell2.xCoord && cell1.yCoord == cell2.yCoord;
        }

        public int GetHashCode(object obj)
        {
            return Convert.ToInt32(xCoord) * 19 + Convert.ToInt32(yCoord) * 37 + hexagonMesh.GetHashCode() * 7 + squareMesh.GetHashCode() * 73;
        }
        #endregion

        #region Odin Inspector
        public IEnumerable GetAllCellTerrains()
        {
            return Resources.LoadAll<CellTerrain>("");
        }
        public IEnumerable GetAllCellShapes()
        {
            return Resources.LoadAll<CellShape>("");
        }
        public IEnumerable GetAllCellFeatures()
        {
            return Resources.LoadAll<CellFeature>("");
        }
        public IEnumerable GetAllCountries()
        {
            return Resources.LoadAll<Country>("");
        }

        public void OnChangedTerrain()
        {
            ApplyTraits(this, true);
        }
        public void OnChangedShape()
        {
            ApplyTraits(this, true);
        }
        public void OnChangedFeature()
        {
            ApplyTraits(this, true);
        }
        public bool HasNoCellTerrains()
        {
            return Resources.LoadAll<CellTerrain>("").Length == 0;
        }
        public bool HasNoCellShapes()
        {
            return Resources.LoadAll<CellShape>("").Length == 0;
        }
        public bool HasNoCellFeatures()
        {
            return Resources.LoadAll<CellFeature>("").Length == 0;
        }
        public bool HasNoCountry()
        {
            return Resources.LoadAll<Country>("").Length == 0;
        }

        public static void ApplyTraits(HexCell cell, bool removeExisting = false)
        {
            

            // Reset multipliers here.
            cell.movementSpeedMultiplier = 0;
            cell.attackerPowerMultiplier = 0;
            cell.defenderPowerMultiplier = 0;

            foreach (FieldInfo field in GetTraits(cell))
            {
                CellTraitBase trait = (CellTraitBase)field.GetValue(cell);
                trait.ApplyTrait(cell);
            }
        }
        static List<FieldInfo> GetTraits(HexCell cell)
        {
            List<FieldInfo> traitFields = new List<FieldInfo>();

            FieldInfo[] fields = typeof(HexCell).GetFields();

            foreach (FieldInfo field in fields)
            {
                if(field.GetValue(cell) != null)
                if (field.GetValue(cell).GetType().IsSubclassOf(typeof(CellTraitBase)))
                {
                    traitFields.Add(field);
                }
            }

            return traitFields;
        }
        #endregion
    }


}
