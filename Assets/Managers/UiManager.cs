using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    [SerializeField] BuyPropertyUI buyPanel;
    public void ShowBuyPanel(Tile tile, Player player)
    {
        buyPanel.Open(tile, player);
    }


    private void Start()
    {
        buyPanel.Close();
    }
}
