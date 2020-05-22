using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldControl : MonoBehaviour
{
    public enum PuzzleImage
    {
        Field = 0,
        RedHeart = 1,
        RedCircle = 2,
        PurpleTier = 3,
        PurpleEllipse = 4,
        PurpleDiamond = 5,
        GreenSquare = 6,
    }
    private int Value = 0;
    //private bool IsShift = false;

    public Canvas canvas;
    static public int[,] PuzzleField = new int[5, 5];
    static public GameObject[,] objectsField = new GameObject[5, 5];

    static public Vector2[,] positionArray = new Vector2[5, 5];

    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Value = UnityEngine.Random.Range(1, 7);
                var panel = Resources.Load<GameObject>(Enum.GetName(typeof(PuzzleImage), Value));
                var p = Instantiate<GameObject>(panel);
                p.transform.SetParent(canvas.transform, false);
                p.transform.localPosition = new Vector2(100 * x - 200, 200 - 100 * y);
                PuzzleField[x, y] = Value;

               //positionArray[x, y] = p.transform.localPosition; //座標を保存
            }
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Debug.Log(PuzzleField[j, i]);
                }
            }
            Debug.Log("-----------------");
        }
    }

    public GameObject GetNearestPiece(Vector3 input)
    {
        var minDist = float.MaxValue;
        GameObject nearestPiece = null;

        // 入力値と盤面のピース位置との距離を計算し、一番距離が短いピースを探す
        foreach (var p in objectsField)
        {
            var dist = Vector3.Distance(input, p.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestPiece = p;
            }
        }

        return nearestPiece;
    }

    public void SwitchPiece(GameObject p1, GameObject p2)
    {
        // 位置を移動する
        var p1Position = p1.transform.position;
        p1.transform.position = p2.transform.position;
        p2.transform.position = p1Position;

        // 盤面データを更新する
        var p1BoardPos = GetPieceBoardPos(p1);
        var p2BoardPos = GetPieceBoardPos(p2);
        objectsField[(int)p1BoardPos.x, (int)p1BoardPos.y] = p2;
        objectsField[(int)p2BoardPos.x, (int)p2BoardPos.y] = p1;
    }
    private Vector2 GetPieceBoardPos(GameObject piece)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (objectsField[i, j] == piece)
                {
                    return new Vector2(i, j);
                }
            }
        }

        return Vector2.zero;
    }   

    //空いた所にピースを追加　イゴンヒ
    public void AddPiece()
    { 
        for (int x = 0; x < 5; x++)
        {    
            for(int y = 0; y < 4; y++)
            {
                if (PuzzleField[x, y] == 0)
                {
                    Value = UnityEngine.Random.Range(1, 7);
                    var panel = Resources.Load<GameObject>(Enum.GetName(typeof(PuzzleImage), Value));
                    var p = Instantiate<GameObject>(panel);
                    p.transform.SetParent(canvas.transform, false);
                    p.transform.localPosition = new Vector2(100 * x - 200, 200);
                    PuzzleField[x, y] = Value;
                }
            }
        }
    }

    //---------------------------------------------------

}

////------------------------------------------------イゴンヒ　
////ピースの存在有無をチェックしてピースを下に移動
//public IEnumerator FindNullField()
//{
//    for(int x = 0; x < 5; x++)
//    {
//        for(int y = 0; y < 5; y++)
//        {
//            if(PuzzleField[x, y] == 0)
//            {
//                yield return StartCoroutine(ShiftDown(x, y));
//                break;
//            }
//        }
//    }
//}

////------------------------------------------------イゴンヒ
////ピースを下に移動する処理（未完成）
//public IEnumerator ShiftDown(int x, int yStart, float shiftDelay = .03f)
//{
//    IsShift = true;
//    int nullCount = 0;
//    for(int y = yStart; y < 5; y++)
//    { 
//        if(PuzzleField[x, y] == 0)
//        {
//            nullCount++;
//        }
//    }


//    for (int i = 0; i < nullCount; i++)
//    {
//        yield return new WaitForSeconds(shiftDelay);

//        for (int y = yStart; y < 5; y++)
//        {
//            if (PuzzleField[x,y] == 0)
//            {
//                PuzzleField[x, y] = PuzzleField[x, y +1];
//                new Vector2(100 * x - 200, 200 - 100 * y);
//            }
//        }
//    }
//    IsShift = false;

//}
