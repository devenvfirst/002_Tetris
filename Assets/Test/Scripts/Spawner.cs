using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// 格子游戏物体数组
    /// </summary>
    public GameObject[] blocks;

    /// <summary>
    /// 所有方块的精灵图片
    /// </summary>
    public Sprite[] sprs;

    /// <summary>
    /// 是否是第一次生成
    /// </summary>
    public static bool isFirst = true;

    /// <summary>
    /// 当前方块索引
    /// </summary>
    public int current = 0;

    /// <summary>
    /// 下一个方块的索引
    /// </summary>
    public int next = 0;

    // Use this for initialization
    void Start()
    {
        SpawnerNext();
    }

    /// <summary>
    /// 生成方块
    /// </summary>
    public void SpawnerNext()
    {
        if(isFirst)
        {
            isFirst = false;
            current = Random.Range(0,blocks.Length);
            next = Random.Range(0, blocks.Length);
        }
        else
        {
            current = next;
            next = Random.Range(0, blocks.Length);
        }

        //随机产生方块
        Instantiate(blocks[current],transform.position,Quaternion.identity);
        GameObject.Find("Image").GetComponent<Image>().sprite = sprs[next];
    }
}
