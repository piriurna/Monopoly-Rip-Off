using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, PlayerMovementListener
{

    [SerializeField] private Board board;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private UiManager uiManager;

    [SerializeField] private WalletManager walletManager;

    [SerializeField] private Camera gameCamera;

    private List<GameObject> players;
    private int currentPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        board.SetGameManager(this);
    }

    public void OnBoardReady()
    {
        CreatePlayers();
    }

    private void CreatePlayers()
    {
        players = new List<GameObject>();

        var player1 = createPlayer("Player 1", Color.black);

        var player2 = createPlayer("Player 2", Color.white);

        players.Add(player1);
        players.Add(player2);

        var playersList = new List<Player>();
        foreach(GameObject p in players)
        {
            playersList.Add(p.GetComponent<Player>());
        }

        walletManager.SetPlayers(playersList);

        player1.transform.position = board.getTile(player1.GetComponent<Player>().position).transform.position;

        player2.transform.position = board.getTile(player2.GetComponent<Player>().position).transform.position;
    }

    private GameObject createPlayer(string name, Color color)
    {
        var player = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        player.name = name;
        player.GetComponent<Player>().SetGameCamera(gameCamera);
        player.GetComponent<Player>().playerName = name;
        player.GetComponent<Player>().playerColor = color;
        return player;
    }

    public void RollDice()
    {
        notifyTurnStart();

        var dice1 = Random.Range(1, 6); //Rolldice
        var dice2 = Random.Range(1, 6);

        var totalMove = dice1 + dice2;

        print("Rolled " + totalMove);

        var player = players[currentPlayerIndex];

        MovePlayer(player, totalMove);

    }

    void MovePlayer(GameObject player, int numOfMoves)
    {
        var playerScript = player.GetComponent<Player>();

//        var route = new List<Tile>();
//        for(int i = 0; i < numOfMoves; i++)
//        {
//            var tilePosition = playerScript.position + i;
//
//            route.Add(board.getTile(tilePosition));
//        }

        playerScript.MovePlayer(board, numOfMoves, this);
    }

    //-----------------------
    // Turn Things
    //----------------------
    
    public bool isTurnOngoing = false;

    private List<TurnListener> turnListeners = new List<TurnListener>();

    public void addListener(TurnListener listener)
    {
        turnListeners.Add(listener);
    }

    public void removeListeners()
    {
        turnListeners.Clear();
    }

    private void notifyTurnStart()
    {
        isTurnOngoing = true;
        foreach (TurnListener listener in turnListeners)
        {
            listener.OnTurnStart();
        }
    }

    private void notifyTurnEnd()
    {
        isTurnOngoing = false;
        foreach (TurnListener listener in turnListeners)
        {
            listener.OnTurnEnd();
        }
    }

    void EndTurn()
    {
        notifyTurnEnd();
        var player = players[currentPlayerIndex];
        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
        }
    }

    //-------------------------
    // Player Movement Listener
    //-------------------------

    public void OnMovementFinished(Player player)
    {
        board.getTile(player.position).Activate(player, uiManager);
        EndTurn();
    }
}
