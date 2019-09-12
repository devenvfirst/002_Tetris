using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public float lastFall = 0;

    // Use this for initialization
    void Start()
    {
        if(!IsValidPos())
        {
            Debug.Log("GameOver");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    /// <summary>
    /// 控制格子的移动
    /// </summary>
    private void MoveControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (IsValidPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (IsValidPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall > 1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (IsValidPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                //删除所有已满的行
                Grid.DeleteFullRows();
                //生成下一个砖块
                FindObjectOfType<Spawner>().SpawnerNext();
                //禁用此脚本
                enabled = false;
            }
            lastFall = Time.time;
        }

    }

    /// <summary>
    /// 更新格子数组
    /// </summary>
    private void UpdateGrid()
    {
        //移除旧数组的格子
        for (int y = 0; y < Grid.height; y++)
        {
            for (int x = 0; x < Grid.width; x++)
            {
                if (Grid.grid[x, y] != null)
                {
                    if (Grid.grid[x, y].parent == transform)
                    {
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }
        //赋值新的格子
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    /// <summary>
    /// 位置是否合理
    /// </summary>
    /// <returns></returns>
    private bool IsValidPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            if (!Grid.InsideBorder(v))
            {
                return false;
            }

            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
