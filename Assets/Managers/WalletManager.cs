using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    const double INITIAL_WALLET_AMOUNT = 1000.0;
    private List<Player> players;

    private Dictionary<Player, double> wallets = new Dictionary<Player, double>();

    [SerializeField] WalletsUI walletsUi;

    public void SetPlayers(List<Player> players)
    {
        this.players = players;
        foreach(Player player in players)
        {
            wallets.Add(player, INITIAL_WALLET_AMOUNT);
            walletsUi.CreateWallet(player, INITIAL_WALLET_AMOUNT);
        }
    }

    public List<Player> GetPlayers()
    {
        return players;
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
        wallets.Remove(player);
    }

    public Player GetPlayer(int index)
    {
        return players[index];
    } 

    public double GetBalance(Player player)
    {
        return wallets[player];
    }

    public void SetPlayerBalance(Player player, double amount)
    {
        var differenceValue = 0.0;
        if (amount < GetBalance(player))
        {
            differenceValue = player.GetWalletBalance() - amount;
            SubstractBalance(player, differenceValue);
        } else
        {
            differenceValue = amount - player.GetWalletBalance();
            AddBalance(player, differenceValue);
        }
    }

    public void AddBalance(Player player, double amount)
    {
        wallets[player] += amount;
    }

    public void SubstractBalance(Player player, double amount)
    {
        wallets[player] -= amount;
    }
}
