using UnityEngine;

public class Geometry : MonoBehaviour
{
    public float separation;
    public float startingPoint;

    static private Vector3 FindBoard()
    {
        return GameObject.Find("Chessboard").transform.position;
    }

    static public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        Vector3 boardPosition = FindBoard();
        float x = -3.5f + boardPosition.x + gridPoint.x;
        float z = -3.5f + boardPosition.z + gridPoint.y;

        return new Vector3(x, boardPosition.y + 0.2f, z);
    }

    static public Vector2Int GridPoint(int col, int row)
    {
        return new Vector2Int(col, row);
    }

    static public Vector2Int GridFromPoint(Vector3 point)
    {
        Vector3 boardPosition = FindBoard();

        int col = Mathf.FloorToInt(-6.0f + boardPosition.x + point.x);
        int row = Mathf.FloorToInt(6.0f + boardPosition.z + point.z);

        return new Vector2Int(col, row);
    }
}
