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

    // 盤面上のかけている部分にピースを補充する
    private void FillPiece()
    {
       puzzleChecker.FindNullField(FieldControl.PuzzleField);

       // fieldControl.StartCoroutine("FindNullField");
       //補充用scriptをここに記入
    }
}
