using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldControl : MonoBehaviour
{
    private string[] puzzleImage =
    {
        "Field",
        "RedHeart",
        "RedCircle",
        "PurpleTier",
        "PurpleEllipse",
        "PurpleDiamond",
        "GreenSquare",
    };
    private int Value = 0;
    public Canvas canvas;
    static public int[,] PuzzleField = new int[5,5];
    static public GameObject[,] objectsField = new GameObject[5, 5];    

    void Start () {
        for(int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Value = Random.Range(1, 7);
                var panel = Resources.Load<GameObject>(puzzleImage[Value]);
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
}
