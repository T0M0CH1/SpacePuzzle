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


    private int checkColor(int Posx, int Posy)
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

    private bool IsSameColor(int myX,int myY,int targetX,int targetY)
    {
        return FieldControl.PuzzleField[myX, myY] == FieldControl.PuzzleField[targetX, targetY] && !isCheck[targetX, targetY] && FieldControl.objectsField[targetX, targetY] != null;
    }
}
