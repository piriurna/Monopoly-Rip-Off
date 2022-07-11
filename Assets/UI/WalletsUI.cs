using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletsUI : MonoBehaviour
{

    [SerializeField] private GameObject playerWalletPrefab;

    public void CreateWallet(Player player, double amount)
    {
        //var lastChild = this.transform.GetChild(transform.childCount - 1);
        var prefab = Instantiate(playerWalletPrefab, new Vector3(0,0,0), Quaternion.identity);
        prefab.transform.Find("Label").GetComponent<Text>().text = player.name.ToString();
        prefab.transform.Find("Value").GetComponent<Text>().text = "$ " + amount.ToString();
        prefab.transform.parent = this.transform;
    }
}
