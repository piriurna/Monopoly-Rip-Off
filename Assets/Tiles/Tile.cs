using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

abstract public class Tile : MonoBehaviour
{

    [SerializeField] protected string title;

    [SerializeField] public int order;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Find("TileText").GetComponent<TextMeshPro>().SetText(title);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Activate(Player player, UiManager uiManager)
    {
        print("Activating tile..." + this.title);
    }
}
