using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int position = 0;

    public string playerName;

    private Board board;

    private Camera gameCamera;

    private WalletManager walletManger;

    public void SetWalletManager(WalletManager newWalletManager)
    {
        this.walletManger = newWalletManager;
    }

    public Camera GetGameCamera()
    {
        return gameCamera;
    }

    public void SetGameCamera(Camera camera)
    {
        this.gameCamera = camera;
    }

    [SerializeField] GameObject moneyFloatingText;

    [SerializeField] private double walletBalance = 1000;

    public double GetWalletBalance()
    {
        return walletBalance;
    }


    public void SubtractWalletBalance(double amount)
    {
        walletBalance -= amount;
        var prefab = Instantiate(moneyFloatingText, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 1), Quaternion.identity);
        //prefab.transform.Find("GameObject").eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, 0);
        prefab.transform.Find("GameObject").LookAt(gameCamera.transform);
        prefab.GetComponentInChildren<TextMesh>().text = amount.ToString();
        prefab.GetComponentInChildren<TextMesh>().color = Color.red;
    }

    public void AddWalletBalance(double amount)
    {
        walletBalance -= amount;
        var prefab = Instantiate(moneyFloatingText, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 1), Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = amount.ToString();
        prefab.GetComponentInChildren<TextMesh>().color = Color.green;
    }
    public bool HasEnoughBalance(double amount)
    {
        return amount <= walletBalance;
    }

    public void BuyProperty(Property property)
    {
        if(HasEnoughBalance(property.price))
        {
            property.setOwner(this);
            this.SubtractWalletBalance(property.price);
        }
    }

    public void Charge(Player player, double price)
    {
        if (player.HasEnoughBalance(price))
        {
            player.SubtractWalletBalance(price);
            this.AddWalletBalance(price);
        }
    }

    [Range(0.0f, 10.0f)] [SerializeField] float speed = 10f;

    public Color playerColor;

    private bool isMoving;

    private int steps;

    private bool shouldMove = false;

    private PlayerMovementListener listener;

    // Start is called before the first frame update
    void Start()
    {

        speed = 10f;
        var body = this.transform.Find("Body");
        var bodyRenderer = body.GetComponent<Renderer>();
        bodyRenderer.material.SetColor("_Color", new Color(playerColor.r, playerColor.g, playerColor.b));
    }

    private void Update()
    {
        if(shouldMove && !isMoving)
        {
            StartCoroutine(Move());
        }

        if(steps == 0 && shouldMove)
        {
            shouldMove = false;
        }
    }

    public void MovePlayer(Board board, int numOfMoves, PlayerMovementListener listener)
    {
        this.steps = numOfMoves;
        this.board = board;
        shouldMove = true;

        this.listener = listener;
    }

    private IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }

        isMoving = true;

        position = board.getTile(position + 1).order;

        while (steps >= 0)
        {
            Vector3 newPos = board.getTile(position).transform.position;
            while(MoveToNextNode(newPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.05f);
            steps--;
            if(steps > 0)
            {
                position = board.getTile(position + 1).order;
            }
        }

        isMoving = false;
        listener.OnMovementFinished(this);
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime));
    }
}
