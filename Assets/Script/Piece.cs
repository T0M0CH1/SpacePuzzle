using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    // public.
    public bool deleteFlag;

    // private.
    private Image thisImage;
    private RectTransform thisRectTransform;
    private FieldControl.PuzzleImage kind;

    //-------------------------------------------------------
    // MonoBehaviour Function
    //-------------------------------------------------------
    // 初期化処理
    private void Awake()
    {
        // アタッチされている各コンポーネントを取得
        thisImage = GetComponent<Image>();
        thisRectTransform = GetComponent<RectTransform>();

        // フラグを初期化
        deleteFlag = false;

    }


    //-------------------------------------------------------
    // Public Function
    //-------------------------------------------------------
    // ピースの種類とそれに応じた色をセットする
    public void SetKind(FieldControl.PuzzleImage pieceKind)
    {
        kind = pieceKind;
        SetColor();
    }

    // ピースの種類を返す
    public FieldControl.PuzzleImage GetKind()
    {
        return kind;
    }

    // ピースのサイズをセットする
    public void SetSize(int size)
    {
        this.thisRectTransform.sizeDelta = Vector2.one * size;
    }

    //-------------------------------------------------------
    // Private Function
    //-------------------------------------------------------
    // ピースの色を自身の種類の物に変える
    private void SetColor()
    {
        switch (kind)
        {
            case FieldControl.PuzzleImage.GreenSquare:
                thisImage.sprite.name = "GreenSquare";
                break;
            case FieldControl.PuzzleImage.PurpleDiamond:
                thisImage.sprite.name = "PurpleDiamond";
                break;
            case FieldControl.PuzzleImage.PurpleEllipse:
                thisImage.sprite.name = "PurpleEllipse";
                break;
            case FieldControl.PuzzleImage.PurpleTier:
                thisImage.sprite.name = "PurpleTier";
                break;
            case FieldControl.PuzzleImage.RedCircle:
                thisImage.sprite.name = "RedCircle";
                break;
            case FieldControl.PuzzleImage.RedHeart:
                thisImage.sprite.name = "RedHeart";
                break;
            default:
                break;
        }
    }
    //--------------------------------------------------------------------------
    /// <summary>
    /// ピーズを下に移動する
    /// </summary>
    /// <param name="DownFlag"> 下にピーズの有無を確認</param>
    private void ShifttDown(bool DownFlag)
    {
        if(DownFlag)
        {
            var Object_Pos = GetComponent<RectTransform>().anchoredPosition;
          transform.localPosition = new Vector2(Object_Pos.x, Object_Pos.y - 100);
        }
     
    }
    //--------------------------------------------------------------------------
}