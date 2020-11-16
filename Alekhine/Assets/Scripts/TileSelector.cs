using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSelector : MonoBehaviour
{
    public GameObject tileHighlightPrefab;
    private GameObject tileHighlight;

    void Start() {
        Vector2Int gridPoint = Geometry.GridPoint(0,0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
        tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void EnterState() {
        this.enabled = true;
    }

    public void ExitState(GameObject movingPiece) {
        this.enabled = false;
        tileHighlight.SetActive(false);
        MoveSelector move = GetComponent<MoveSelector>();
        move.EnterState(movingPiece);
    }
}
