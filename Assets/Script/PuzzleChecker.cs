using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PuzzleChecker : MonoBehaviour {

    
    public static bool[,] isCheck = new bool[5, 5];
    private int score;
    private int bombCount = 0;
    private bool vertical = true;
    private bool side = true;


    void Start()
    {
        score = 0; ;
    }
    void Update () {

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                isCheck = new bool[5, 5];
                int res = checkColor(i, j);
                if (res >= 3)
                {
                    //if(bombCount > 0)
                    //{
                    //    SceneManager.LoadScene("GameOver");
                    //}
                    //Debug.Log(res);
                    targetDelete(isCheck);
                }
            }
        }

    }

    private void targetDelete(bool[,] targets)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (targets[i, j])
                {                    
                    //Debug.Log(FieldControl.objectsField[i, j]);
                    Destroy(FieldControl.objectsField[i, j]);
                    score += 1;
                    FieldControl.PuzzleField[i, j] = 0;
                }
            }
        }
    }


    public int checkColor(int Posx, int Posy)
    {
        int result = 0;
        int mine = FieldControl.PuzzleField[Posx, Posy];
        if (mine == 0) { return 0; }
       
        isCheck[Posx, Posy] = true;
        //下のパズルの状態をチェック
        if (Posy != 4 && IsSameColor(Posx,Posy,Posx,Posy + 1) && vertical)
        {
            result += checkColor(Posx, Posy + 1);
            bombCount += 1;
            side = false;
        }
        //上のパズルの状態をチェック
        if (Posy != 0 && IsSameColor(Posx, Posy, Posx, Posy - 1) && vertical)
        {
            result += checkColor(Posx, Posy - 1);
            bombCount += 1;
            side = false;
        }
        //右のパズルの状態をチェック
        if (Posx != 4 && IsSameColor(Posx, Posy, Posx + 1, Posy) && side)
        {
            result += checkColor(Posx + 1, Posy);
            bombCount += 1;
            vertical = false;
        }
        //左のパズルの状態をチェック
        if (Posx != 0 && IsSameColor(Posx, Posy, Posx - 1, Posy) && side)
        {
            result += checkColor(Posx - 1, Posy);
            bombCount += 1;
            vertical = false;
        }
        return result + 1;
    }

    //下のコマにピースを移動　イゴンヒ
    private void ShiftDown(int x, int y)
    {
        if (FieldControl.PuzzleField[x, y + 1] == 0)//下にピースがないなら
        {
            FieldControl.PuzzleField[x, y + 1] = FieldControl.PuzzleField[x, y];
            FieldControl.PuzzleField[x, y] = 0;
            //オブジェクト移動更新が必要。
        }
    }

    //イゴンヒ
    public void All_Objectt_ShiftDown()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                ShiftDown(x,y);
            }
        }
    }

    //イゴンヒ
    public bool Bottom_check()
    {
        for (int x = 0; x < 5; x++)
        {
            if (FieldControl.PuzzleField[x, 4] == 0)
            {
                return true;
            }
        }
        return false;
    }


  

    private bool IsSameColor(int myX,int myY,int targetX,int targetY)
    {
        return FieldControl.PuzzleField[myX, myY] == FieldControl.PuzzleField[targetX, targetY] && !isCheck[targetX, targetY] && FieldControl.objectsField[targetX, targetY] != null;
    }

}

///// <summary>
///// ピーズがない所を検索
///// </summary>
///// <param name="PuzzleField">検索の対象</param>
///// <returns>ピーズがない数</returns>
//public int FindNullField(int[,] PuzzleField)
//{
//    int NullCount = 0;
//    for (int x = 0; x < 5; x++)
//    {
//        for (int y = 0; y < 5; y++)
//        {
//            if (PuzzleField[x, y] == 0)
//            {
//                NullCount++;
//            }
//        }
//    }
//    return NullCount;
//}
