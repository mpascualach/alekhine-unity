using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private bool _inCheck;

    private Animator m_animator;

    public bool inCheck
    {
        get
        {
            return _inCheck;
        }
        set
        {
            if (_inCheck == value) return;
            _inCheck = value;

            m_animator = gameObject.GetComponent<Animator>();

            m_animator.SetBool("inCheck", value);

            //if (m_animator.isActiveAndEnabled)
            //{

            //}
        }
    }

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
            if (GameManager.instance.PieceAtGrid(nextGridPoint))
            {
                break;
            }
        }

        foreach (Vector2Int dir in RookDirections)
        {
            Vector2Int nextGridPoint = new Vector2Int(position.x + dir.x, position.y + dir.y);
            locations.Add(nextGridPoint);
            if (GameManager.instance.PieceAtGrid(nextGridPoint))
            {
                break;
            }
        }

        return locations;
    }
}
