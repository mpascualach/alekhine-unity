using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;

    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    private GameObject[,] pieces;

    public GameObject capturedPiecesWhite;
    public GameObject capturedPiecesBlack;

    public Material defaultWhite;
    public Material defaultBlack;

    private Player white;
    private Player black;
    public Player currentPlayer;
    public Player otherPlayer;

    void Awake() {
        instance = this;
    }

    void Start () {
        pieces = new GameObject[8, 8];

        white = new Player("white", true);
        black = new Player("black", false);

        currentPlayer = white;
        otherPlayer = black;

        SetBoardUp();
    }

    private void SetBoardUp() {
        AddPiece(whiteRook, white, 0, 0);
        AddPiece(whiteKnight, white, 1, 0);
        AddPiece(whiteBishop, white, 2, 0);
        AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        AddPiece(whiteBishop, white, 5, 0);
        AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
        }

        AddPiece(blackRook, black, 0, 7);
        AddPiece(blackKnight, black, 1, 7);
        AddPiece(blackBishop, black, 2, 7);
        AddPiece(blackQueen, black, 3, 7);
        AddPiece(blackKing, black, 4, 7);
        AddPiece(blackBishop, black, 5, 7);
        AddPiece(blackKnight, black, 6, 7);
        AddPiece(blackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(blackPawn, black, i, 6);
        }

        CheckAllMoves();
        CheckPlayerMoves(otherPlayer, true);
    }

    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, player, col, row);
        Piece pieceScript = pieceObject.GetComponent<Piece>();
        pieceScript.player = player;
        player.pieces.Add(pieceObject);

        if (pieceScript.type == PieceType.King)
        {
            player.king = pieceObject;
        }
        pieces[col, row] = pieceObject;

        SetPiecePosition(pieceObject, new Vector2Int(col, row));
    }

    private void SetPiecePosition(GameObject pieceObject, Vector2Int gridPoint)
    {
        pieceObject.GetComponent<Piece>().position = gridPoint;
    }

    private void CheckAllMoves() // scale this maybe for more than 1 player?
    {
        currentPlayer.inCheck = false;
        otherPlayer.inCheck = false;

        CheckPlayerMoves(otherPlayer, false);
        CheckPlayerMoves(currentPlayer, true);
    }

    private void CheckPlayerMoves(Player player, bool checkingCurrentPlayer)
    {
        foreach (GameObject piece in player.pieces)
        {
            MovesForPiece(piece);
            if (!checkingCurrentPlayer && currentPlayer.inCheck)
            {
                // illegal move - fall back
            }
        }
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();

        var locations = piece.MoveLocations(piece.position);

        locations.RemoveAll(tile => tile.x < 0 || tile.x > 7
            || tile.y < 0 || tile.y > 7);

        locations.RemoveAll(tile => FriendlyPieceAt(tile));

        return locations;
    }

    public void SelectPiece(GameObject piece) {
       board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece) {
        board.DeselectPiece(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint) {
        if (!SquareIsInBounds(gridPoint)) {
            return null;
        }
        GameObject otherPiece = pieces[gridPoint.x, gridPoint.y];
        return otherPiece;
    }

    public bool SquareIsInBounds(Vector2Int gridPoint) {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0) {
            return false;
        }
        return true;
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint) {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null) {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece)) {
            return false;
        }

        return true;
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece) {
        return currentPlayer.pieces.Contains(piece);
    }

    public void Move(GameObject piece, Vector3 position, Vector2Int gridPoint) {
        if (PieceAtGrid(gridPoint)) {
            CapturePieceAt(gridPoint);
        }

        Vector2Int startGridPoint = piece.GetComponent<Piece>().position;

        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = null;

        CheckAllMoves();

        SetPiecePosition(piece, gridPoint);

        pieces[gridPoint.x, gridPoint.y] = piece;
        board.MovePiece(piece, position);

        NextPlayer();
    }

    //public Vector2Int GridForPiece(GameObject piece)
    //{
    //    for (int i = 0; i < 8; i++)
    //    {
    //        for (int j = 0; j < 8; j++)
    //        {
    //            if (pieces[i, j] == piece)
    //            {
    //                return new Vector2Int(i, j);
    //            }
    //        }
    //    }
    //    return new Vector2Int(-1, -1);
    //}

    public void NextPlayer() {
        Player tempPlayer = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = tempPlayer;
    }

    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);

        Vector3 spawnPos;

        if (otherPlayer.capturedPieces.Count > 0)
        {
            GameObject latestCapturedPiece = otherPlayer.capturedPieces[otherPlayer.capturedPieces.Count - 1];
            spawnPos = latestCapturedPiece.transform.position;
            spawnPos.x += 5.0f;
        }
        else
        {
            GameObject capturedPieces = currentPlayer.isWhite ? capturedPiecesBlack : capturedPiecesWhite;
            spawnPos = capturedPieces.transform.position;
        }

        pieceToCapture.transform.position = spawnPos;

        //if (pieceToCapture.GetComponent<Piece>().type == PieceType.King) {
        //    Debug.Log(currentPlayer.name + " wins!");
        //    Destroy(board.GetComponent<TileSelector>());
        //    Destroy(board.GetComponent<MoveSelector>());
        //}
        currentPlayer.capturedPieces.Add(pieceToCapture);
        pieces[gridPoint.x, gridPoint.y] = null;
        //Destroy(pieceToCapture);
    }
}
