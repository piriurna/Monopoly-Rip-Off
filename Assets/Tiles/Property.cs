using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Property : Tile
{
    [SerializeField] public double price;

    public Player owner;

    public Property()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        var tileTextObject = this.transform.gameObject.transform.Find("TileText");
        var tileText = tileTextObject.GetComponent<TextMeshPro>();
        tileText.SetText(title);

        var tilePriceObject = this.transform.gameObject.transform.Find("TilePrice");
        var tilePrice = tilePriceObject.GetComponent<TextMeshPro>();
        tilePrice.SetText("€ " + price.ToString());

        var tileColorObject = this.transform.Find("TileColor");
        if (tileColorObject == null) return;

        var tileRedenrer = tileColorObject.GetComponent<Renderer>();
        var setParent = this.transform.parent.GetComponent<Set>();
        tileRedenrer.material.SetColor("_Color", new Color(setParent.setColor.r, setParent.setColor.g, setParent.setColor.b));

    }

    public override void Activate(Player player, UiManager uiManager)
    {
        base.Activate(player, uiManager);
        if (owner == null)
        {
            print("Doesnt have a owner... Presenting buy option");
            showBuyOption(uiManager, player);
        } else
        {
            if(player.HasEnoughBalance(this.price))
            {
                print("Already have a owner... Charging property price");
                owner.Charge(player, this.price);
            }
        }
    }

    public void setOwner(Player player)
    {
        owner = player;   
        var tileColorObject = this.transform.gameObject.transform.Find("TileColor");
        if (tileColorObject == null) return;

        var tileRedenrer = tileColorObject.GetComponent<Renderer>();
        if (owner != null)
        {
            var playerColor = new Color(owner.playerColor.r, owner.playerColor.g, owner.playerColor.b);
            tileRedenrer.material.SetColor("_Color", playerColor);
        }
    }

    private void showBuyOption(UiManager uiManager, Player player)
    {
        uiManager.ShowBuyPanel(this, player);
    }


    public void OnPropertyBought()
    {

    }
}
