using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;

    private List<Vector2Int> moveLocations = new List<Vector2Int>();
    private List<GameObject> locationHighlights = new List<GameObject>();

    void Start() {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    public void EnterState(GameObject piece) {
        movingPiece = piece;
        this.enabled = true;

        moveLocations = GameManager.instance.MovesForPiece(movingPiece);

        foreach(Vector2Int location in moveLocations) {
            GameObject highlight;

            Vector3 highlightPosition = Geometry.LocateSquare(location.y, location.x);

            GameObject locationType = GameManager.instance.PieceAtGrid(location) ? attackLocationPrefab : moveLocationPrefab;

            highlight = Instantiate(attackLocationPrefab, highlightPosition, Quaternion.identity, gameObject.transform);
        }
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0)) {
                if (moveLocations.Contains(gridPoint)) {
                    if (GameManager.instance.PieceAtGrid(gridPoint))
                    {
                        GameManager.instance.CapturePieceAt(gridPoint);
                    }
                    GameManager.instance.Move(movingPiece, gridPoint);
                }

                if (moveLocations.Count > 0 && !moveLocations.Contains(gridPoint)) {
                    return;
                }

                ExitState();
            }
        } else {
            tileHighlight.SetActive(false);
        }
    }

    public void ExitState() {
        this.enabled = false;
        tileHighlight.SetActive(false);

        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }

        //TileSelector selector = GetComponent<TileSelector>();
        //selector.EnterState();
    }


}
