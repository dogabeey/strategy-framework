//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.UI;
//using UnityEngine.UI;
//using DG.Tweening;
//using Sirenix.OdinInspector;

//namespace Gametator {
//    [CustomPreview(typeof(HexCell))]
//    public class HexCellPreview : ObjectPreview
//    {
//        public override bool HasPreviewGUI()
//        {
//            return true;
//        }

//        public override void OnPreviewGUI(Rect r, GUIStyle background)
//        {
//            HexCell cell = (HexCell)target;
//            GUI.DrawTexture(r, cell.editorIcon.texture);
//        }
//    }
//}