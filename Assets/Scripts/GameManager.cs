using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    private float initialZScale = 1.4f;
    private int winPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ボールを落としたとき
    /// </summary>
    public void OnBallDropped(bool isPlayer1)
    {
        // 位置リセット
        {
            var copyPosition = player1.transform.position;
            copyPosition.z = 0f;
            player1.transform.position = copyPosition;
        }
        {
            var copyPosition = player2.transform.position;
            copyPosition.z = 0f;
            player2.transform.position = copyPosition;
        }

        if (isPlayer1)
        {
            // プレイヤー２が連勝していれば、プレイヤー１の板を広げる
            if (winPlayer == 2)
            {
                if(player1.transform.localScale.z < 8.0f)
                {
                    var copyScale = player1.transform.localScale;
                    copyScale.z *= 1.1f;
                    player1.transform.localScale = copyScale;
                }

                if (0.1f < player2.transform.localScale.z)
                {
                    var copyScale = player2.transform.localScale;
                    copyScale.z *= 0.91f;
                    player2.transform.localScale = copyScale;
                }
            }

            winPlayer = 2;
        }
        else
        {
            // プレイヤー１が連勝していれば、プレイヤー２の板を広げる
            if (winPlayer == 1)
            {
                if (0.1f < player1.transform.localScale.z)
                {
                    var copyScale = player1.transform.localScale;
                    copyScale.z = 0.91f;
                    player1.transform.localScale = copyScale;
                }

                if (player2.transform.localScale.z < 8.0f)
                {
                    var copyScale = player2.transform.localScale;
                    copyScale.z *= 1.1f;
                    player2.transform.localScale = copyScale;
                }
            }

            winPlayer = 1;
        }
    }
}
