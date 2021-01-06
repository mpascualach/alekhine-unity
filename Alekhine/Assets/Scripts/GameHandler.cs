using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

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
    private int increment;

    public GameObject capturedPiecesWhite;
    public GameObject capturedPiecesBlack;

    private Player white;
    private Player black;

    public Player currentPlayer;
    public Player otherPlayer;

    public bool againstComputer;
    public bool playerIsWhite;

    public Opponent opponent;

    void Awake() {
        instance = this;
    }

    void Start() {
        pieces = new GameObject[8, 8];

        white = new Player("white", true, false);
        black = new Player("black", false, true);

        currentPlayer = white;
        otherPlayer = black;

        increment = 0;
        SetBoardUp();
    }

    private void SetBoardUp() {
        AddPiece(whiteRook, white, 0, 0);
        //AddPiece(whiteKnight, white, 1, 0);
        //AddPiece(whiteBishop, white, 2, 0);
        //AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        //AddPiece(whiteBishop, white, 5, 0);
        //AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        //for (int i = 0; i < 8; i++)
        //{
        //    AddPiece(whitePawn, white, i, 1);
        //}

        //GenerateMoves(white);

        //for (int i = 0; i < 8; i++)
        //{
        //    AddPiece(blackPawn, black, i, 6);
        //}

        //AddPiece(blackRook, black, 0, 7);
        //AddPiece(blackKnight, black, 1, 7);
        //AddPiece(blackBishop, black, 2, 7);
        //AddPiece(blackQueen, black, 3, 7);
        //AddPiece(blackKing, black, 4, 7);
        //AddPiece(blackBishop, black, 5, 7);
        //AddPiece(blackKnight, black, 6, 7);
        //AddPiece(blackRook, black, 7, 7);

        //CheckTurn();
        // first move
    }

    public void AddPiece(GameObject prefab, Player player, int row, int col) {
        GameObject pieceObject = board.AddPiece(prefab, player, col, row);
        Piece pieceScript = pieceObject.GetComponent<Piece>();
        pieceScript.player = player;

        SetPiecePosition(pieceObject, new Vector2Int(row, col));

        pieceScript.id = increment;
        increment++;

        player.pieces.Add(pieceObject);

        if (pieceScript.type == PieceType.King) player.king = pieceObject;
        pieces[row, col] = pieceObject;
    }

    private void SetPiecePosition(GameObject pieceObject, Vector2Int gridPoint) {
        Piece pieceScript = pieceObject.GetComponent<Piece>();
        pieceScript.position = gridPoint;
    }

    public void GenerateMoves(Player player)
    {
        List<Move> allMoves = new List<Move>();
        foreach (var pieceObject in player.pieces)
        {
            Piece piece = pieceObject.GetComponent<Piece>();
            var moves = MovesForPiece(pieceObject);
            foreach (var move in moves)
            {
                Move newMove = new Move(pieceObject, move);
                allMoves.Add(newMove);
                player.allMoves = allMoves;
            }
        }
    }


    public void CheckTurn()
    {
        if (againstComputer && currentPlayer.isOpponent)
        {
            currentPlayer.ConsiderMove();
        }
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject) {
        Piece piece = pieceObject.GetComponent<Piece>();

        var locations = piece.MoveLocations(piece.position);

        locations.RemoveAll(tile => tile.x < 0 || tile.x > 7
            || tile.y < 0 || tile.y > 7);

        locations.RemoveAll(tile => FriendlyPieceAt(tile) || tile == piece.position);

        return locations;
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

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject targetPiece)
    {
        Piece piece = targetPiece.GetComponent<Piece>();
        return piece.player.name == currentPlayer.name;
    }

    public void Move(GameObject piece, Vector2Int gridPoint) {
        Vector2Int startGridPoint = piece.GetComponent<Piece>().position;
        Piece pieceData = piece.GetComponent<Piece>();

        pieces[startGridPoint.x, startGridPoint.y] = null;

        if (PieceAtGrid(gridPoint))
        {
            CapturePieceAt(gridPoint);
        }

        pieces[gridPoint.x, gridPoint.y] = null;

        SetPiecePosition(piece, gridPoint);

        pieces[gridPoint.x, gridPoint.y] = piece;

        GameObject square = board.transform.GetChild(gridPoint.y).GetChild(gridPoint.x).gameObject;
        board.MovePiece(piece, square.transform.position);

        Square squareScript = square.GetComponent<Square>();
        squareScript.attachedPiece = piece;

        NextPlayer();
        CheckTurn();
    }

    public Piece FindRook(int xCoordinate, Player player)
    {
        Piece king = player.king.GetComponent<Piece>();
        Piece rook = player.pieces.Find(piece => piece.GetComponent<Piece>().position == new Vector2Int(xCoordinate, king.position.y)).GetComponent<Piece>();
        if (rook.type == PieceType.Rook) return rook;
        return null;
    }

    public void Castle(GameObject king, GameObject rook) {
        Piece rookData = rook.GetComponent<Piece>();
        Piece kingData = king.GetComponent<Piece>();

        bool isKingside = rookData.position.x == 7;
        Vector2Int newKingPosition = new Vector2Int( isKingside ? 6 : 2, kingData.position.y);

        Vector2Int kingStartGridPoint = kingData.position;

        pieces[kingStartGridPoint.x, kingStartGridPoint.y] = null;
        SetPiecePosition(king, newKingPosition);

        GameObject kingSquare = board.transform.GetChild(newKingPosition.y).GetChild(newKingPosition.x).gameObject;
        board.MovePiece(king, kingSquare.transform.position);

        Vector2Int newRookPosition = new Vector2Int(isKingside ? 5 : 3, rookData.position.y);
        Vector2Int rookStartGridPoint = rookData.position;

        GameObject rookSquare = board.transform.GetChild(newRookPosition.y).GetChild(newRookPosition.x).gameObject;
        board.MovePiece(rook, rookSquare.transform.position);

        pieces[rookStartGridPoint.x, rookStartGridPoint.y] = null;
        SetPiecePosition(rook, newRookPosition);

        NextPlayer();
        CheckTurn();
    }


    private void UndoMove() {

    }

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

        currentPlayer.capturedPieces.Add(pieceToCapture);
        pieces[gridPoint.x, gridPoint.y] = null;
    }
}
