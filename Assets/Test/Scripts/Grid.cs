using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{        
    /// <summary>
    /// 二位数组的宽高
    /// </summary>
    public static int width = 10;
    public static int height = 20;

    /// <summary>
    /// 分数
    /// </summary>
    public static int score = 0;

    /// <summary>
    /// 格子数组
    /// </summary>
    public static Transform[,] grid = new Transform[width,height];


    /// <summary>
    /// 取整的函数
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),Mathf.Round(v.y));
    }

    /// <summary>
    /// 方块组是否在边界内
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool InsideBorder(Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0;
    }

    /// <summary>
    /// 判断某一行方块是否已满
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if(grid[x,y]==null)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 删除一行的方块
    /// </summary>
    /// <param name="y">行数</param>
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x,y].gameObject);
            grid[x, y] = null;
        }
    }

    /// <summary>
    /// 将给定的一行下移
    /// </summary>
    /// <param name="y">行数</param>
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0,-1,0);
            }
        }
    }

    /// <summary>
    /// 将所有行下降s
    /// </summary>
    /// <param name="y"></param>
    public static void DecreaseRowAbove(int y)
    {
        for (int i = y; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    /// <summary>
    /// 删除所有已满的行
    /// </summary>
    public static void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if(IsRowFull(y))
            {
                DeleteRow(y);
                score++;
                SetScore(score);
                DecreaseRowAbove(y+1);
                y--;
            }
        }
    }

    private static void SetScore(int s)
    {
        GameObject.Find("Score").GetComponent<Text>().text = s + "";
    }
}
