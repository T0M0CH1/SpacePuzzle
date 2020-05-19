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
    public Canvas canvas;
    static public int[,] PuzzleField = new int[5,5];
    static public GameObject[,] objectsField = new GameObject[5, 5];    

    void Start () {
        for(int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Value = UnityEngine.Random.Range(1, 7);
                var panel = Resources.Load<GameObject>(Enum.GetName(typeof(PuzzleImage), Value));
                var p = Instantiate<GameObject>(panel);
                p.transform.SetParent(canvas.transform, false);
                p.transform.localPosition = new Vector2(100 * x - 200, 200 - 100 * y);
                PuzzleField[x,y] = Value;
            }
        }
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Debug.Log(PuzzleField[j,i]);
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
}
