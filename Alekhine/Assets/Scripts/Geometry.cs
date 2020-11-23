using UnityEngine;

public class Geometry : MonoBehaviour
{
    public float separation;
    public float startingPoint;

    public Board board;

    public GameObject FindSquare(Vector2Int location)
    {
        return board.gameObject.transform.GetChild(location.y).GetChild(location.x).gameObject;
    }

    public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        Vector3 boardPosition = board.gameObject.transform.position;
        float x = -3.5f + boardPosition.x + gridPoint.x;
        float z = -3.5f + boardPosition.z + gridPoint.y;

        return new Vector3(x, boardPosition.y + 0.2f, z);
    }

    static public Vector2Int GridPoint(int col, int row)
    {
        return new Vector2Int(col, row);
    }

    public Vector2Int GridFromPoint(Vector3 point)
    {
        Vector3 boardPosition = board.gameObject.transform.position;

        int col = Mathf.FloorToInt(-6.0f + boardPosition.x + point.x);
        int row = Mathf.FloorToInt(6.0f + boardPosition.z + point.z);

        return new Vector2Int(col, row);
    }
}
