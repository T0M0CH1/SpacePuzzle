using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Idle,
        PieceMove,
        MatchCheck,
        DeletePiece,
        FillPiece,
    }
    private GameState currentState;

    [SerializeField]
    private FieldControl fieldControl;
    [SerializeField]
    private PuzzleChecker puzzleChecker;
    private GameObject selectedPiece;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Idle:
                Idle();
                break;
            case GameState.PieceMove:
                PieceMove();
                break;
            case GameState.FillPiece:
                FillPiece();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedPiece = fieldControl.GetNearestPiece(Input.mousePosition);
            currentState = GameState.PieceMove;
        }
    }

    private void PieceMove()
    {
        if (Input.GetMouseButton(0))
        {
            var piece = fieldControl.GetNearestPiece(Input.mousePosition);
            if (piece != selectedPiece)
            {
                fieldControl.SwitchPiece(selectedPiece, piece);
                currentState = GameState.MatchCheck;
            }
        }
    }

    private void FillPiece()
    {
        //puzzleChecker.FindNullField(FieldControl.PuzzleField);
        //補充用scriptをここに記入

        while (puzzleChecker.Bottom_check())
        {
            puzzleChecker.All_Objectt_ShiftDown();
            //イメージ更新が処理が必要
        }
        fieldControl.AddPiece();

    }
}
