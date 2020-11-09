using UnityEngine;

public class Geometry : MonoBehaviour
{
    public float separation;
    public float startingPoint;

    static public Transform FindBoard()
    {
        return GameObject.Find("Chessboard").transform;
    }

    static public Vector3 LocateSquare(int row, int col)
    {
        return FindBoard().GetChild(row).transform.GetChild(col).transform.position;
    }


    static public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        Vector3 boardPosition = FindBoard().position;
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
        Vector3 boardPosition = FindBoard().position;

        int col = Mathf.FloorToInt(-6.0f + boardPosition.x + point.x);
        int row = Mathf.FloorToInt(6.0f + boardPosition.z + point.z);

        return new Vector2Int(col, row);
    }
}
