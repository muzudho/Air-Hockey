using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float rightSpeed;
    [SerializeField] private float upSpeed = 0f;
    [SerializeField] private float forwardSpeed;
    private Rigidbody ballRb;
    private bool player1Win;
    private GameManager gameManager;

    /// <summary>
    /// 反射回数
    /// </summary>
    private int reflectionCount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        ballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Restart
        // 下の方へ落下したら、場の真ん中に出てくる
        if (gameObject.transform.position.y < -20)
        {
            gameObject.transform.position = new Vector3(0, 0.2f, 0);
            rightSpeed = 3.0f;
            upSpeed = 0f;
            forwardSpeed = 3.0f;

            if (player1Win)
            {
                // 1P の方へ飛ばす（勝った方に向かって飛ばす）
                rightSpeed *= -1.0f;
            }

            gameManager.OnBallDropped(!player1Win);
        }
        // Lose
        // 場外に出ていたら、真下に落ちる
        else if (gameObject.transform.position.x < -10 || 10 < gameObject.transform.position.x)
        {
            player1Win = 0 < gameObject.transform.position.x;

            rightSpeed = 0f;
            upSpeed = -10.0f;
            forwardSpeed = 0f;
        }

        transform.Translate(Vector3.right * Time.deltaTime * rightSpeed);
        transform.Translate(Vector3.up * Time.deltaTime * upSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Ball collide to {other.gameObject.name}");

        bool isReflection = false;

        if (other.gameObject.name == "Wall North")
        {
            forwardSpeed *= -1; // 上下反転
            isReflection = true;
        }
        else if (other.gameObject.name == "Wall South")
        {
            forwardSpeed *= -1; // 上下反転
            isReflection = true;
        }
        else if (other.gameObject.name == "Player 1")
        {
            rightSpeed *= -1; // 左右反転
            isReflection = true;
        }
        else if (other.gameObject.name == "Player 2")
        {
            rightSpeed *= -1; // 左右反転
            isReflection = true;
        }

        if (isReflection)
        {
            reflectionCount++;

            // ２回加速して、１回減速する繰り返し。だんだん速くなる
            if (reflectionCount%3==0)
            {
                // 減少
                rightSpeed *= 0.9f;
                forwardSpeed *= 0.9f;
            }
            else
            {
                // 増加
                rightSpeed *= 1.1f; 
                forwardSpeed *= 1.1f;
            }
        }
    }
}
