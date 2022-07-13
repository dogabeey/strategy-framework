using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gametator {
    [RequireComponent(typeof(MeshCollider))]
    public class CellCollider : MonoBehaviour
    {
        HexCell cellParent;

        // Start is called before the first frame update
        void Start()
        {
            cellParent = GetComponentInParent<HexCell>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown()
        {
            EventManager.TriggerEvent(HexConstants.EVENTS.CELL_SELECTED, new EventParam(paramObj: cellParent.gameObject));
        }
    }
}