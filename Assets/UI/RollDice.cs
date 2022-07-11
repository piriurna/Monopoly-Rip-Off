using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour, TurnListener
{

    [SerializeField] GameManager gameManager;

    void Start()
    {
        gameManager.addListener(this);    
    }

    public void OnClick()
    {
        gameManager.RollDice();
    }

    public void OnTurnEnd()
    {
        this.GetComponent<Button>().interactable = true;
    }

    public void OnTurnStart()
    {
        this.GetComponent<Button>().interactable = false;
    }
}
