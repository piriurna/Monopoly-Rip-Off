using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPropertyUI : MonoBehaviour
{

    Tile tile;

    Player player;

    public Button acceptButton;
    public Button negativeButton;
    public Text notEnoughFundsError;


    private void Start()
    {
        acceptButton.onClick.AddListener(OnAcceptClick);
        negativeButton.onClick.AddListener(OnRefusePressed);

    }


    public void Open(Tile tile, Player player)
    {
        this.gameObject.SetActive(true);
        this.tile = tile;
        this.player = player;
    }

    public void Close()
    {
        resetView();
    }

    public void OnAcceptClick()
    {
        print("ACCEPT PRESSED");
        if (tile is Property)
        {
            if(player.HasEnoughBalance((tile as Property).price))
            {
                player.BuyProperty(tile as Property);
                print("Property Bought");
                resetView();
            }
            else
            {
                print("Player doesn't have enough Funds");
                notEnoughFundsError.gameObject.SetActive(true);
            }
        }      
    }

    private void resetView()
    {
        this.gameObject.SetActive(false);
        player = null;
        tile = null;
        notEnoughFundsError.gameObject.SetActive(false);
    }
    public void OnRefusePressed()
    {
        resetView();
    }

}
