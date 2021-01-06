using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private bool _inCheck;

    private Animator m_animator;

    public bool inCheck;

    private void Start()
    {
        inCheck = false;
    }

    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (Vector2Int dir in BishopDirections)
        {
            Vector2Int nextGridPoint = new Vector2Int(position.x + dir.x, position.y + dir.y);
            locations.Add(nextGridPoint);
        }

        Piece kingsideRook = GameHandler.instance.FindRook(7, player);
        Piece queensideRook = GameHandler.instance.FindRook(0, player);

        foreach (Vector2Int dir in RookDirections)
        {
            Vector2Int nextGridPoint = new Vector2Int(position.x + dir.x, position.y + dir.y);
            locations.Add(nextGridPoint);
        }

        if (kingsideRook && !kingsideRook.developed && !developed)
        {
            bool allowed = true;
            if (allowed)
            {
                locations.Add(new Vector2Int(6, position.y));
            }
        }

        if (queensideRook && !kingsideRook.developed && !developed)
        {
            bool allowed = true;
            if (allowed)
            {
                locations.Add(new Vector2Int(2, position.y));
            }
        }

        return locations;
    }
}
