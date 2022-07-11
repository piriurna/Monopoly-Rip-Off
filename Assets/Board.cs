using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GameManager gameManager;

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private List<Tile> tiles;

    public Tile getTile(int position)
    {
        var realPosition = position;
        if(position >= tiles.Count)
        {
            realPosition = position - (tiles.Count);
        }
        return tiles[realPosition];
    }

    public List<Tile> getTiles()
    {
        return tiles;
    }
    // Start is called before the first frame update
    void Start()
    {
        tiles = getTileChildren(new List<Tile>(), this.gameObject);
        tiles.Sort((p1, p2) => p1.order.CompareTo(p2.order));
        gameManager.OnBoardReady();
    }


    private List<Tile> getTileChildren(List<Tile> list, GameObject gameObject)
    {
        var finalList = list;
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).tag == "Tile")
            {
                finalList.Add(gameObject.transform.GetChild(i).GetComponent<Tile>());
            }

            if(gameObject.transform.GetChild(i).childCount > 0)
            {
                finalList = getTileChildren(finalList, gameObject.transform.GetChild(i).gameObject);
            }
        }

        return finalList;

    }


}
