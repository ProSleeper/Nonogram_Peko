using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//해상도를 신경을 써야함..
//근데 그거 신경쓰다가 결국 시간이 너무 걸릴듯
//일단 19:9 해상도로 개발 후 해상도는 추후에 신경써보

public enum eBOARD_SIZE
{
    DEFAULT = 10,
    HARD    = 15,
    EXPERT  = 20
}

public class BoardManager : MonoBehaviour
{ 
    public static BoardManager instance = null;
    Button resetButton;

    int COLUMN = 0;
    int ROW = 0;
    int boardLevel = 0;
    float drawSize = 0;
    GameObject block;
    List<GameObject> blockList = new List<GameObject>();
    GameObject[,] blockArray;

    Vector3 leftBottom;
    Vector3 rightTop;
    Vector3 center;
    Vector3 boardPadding;

    //tempVariable
    public Text width;
    public Text height;

    void BlockSpriteReset()
    {
        foreach (GameObject item in blockList)
        {
            item.GetComponent<Block>().SpriteReset();
        }
    }

    public void SettingBoard(eBOARD_SIZE size)
    {
        blockArray = new GameObject[(int)size, (int)size];
        COLUMN = (int)size;
        ROW = (int)size;
        drawSize = 0.39f / (ROW * 0.1f);

        if (blockList.Count != 0)
        {
            foreach (GameObject block in blockList)
            {
                Destroy(block);
            }
            blockList.Clear();
        }

        for (int i = 0; i > -(ROW); i--)
        {
            for (int j = 0; j < COLUMN; j++)
            {
                block = Instantiate(Resources.Load("4. Prefabs/Block"), new Vector3(i * drawSize + boardPadding.x, j * drawSize + boardPadding.y), Quaternion.identity) as GameObject;
                block.transform.localScale = new Vector3(drawSize, drawSize, 1);
                blockList.Add(block);
                blockArray[(i * -1), j] = block;
            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //이 worldPointSize는 길이를 주는게 아니라 0부터의 좌표를 주는거임
        //현 메인카메라가 0,0에서 그려지고 가로 5 세로 10의 크기이고 0부터 시작이기 때문에 l,r,t,b 좌표는 -2.5, 2.5, 5, -5
        //이렇게 나오게 되니까 이걸 참고해서 스크린과 월드좌표간의 길이를 구해야

        //↑↑↑위 내용이 유니티의 기본 설정이고 개발의 편의를 위해서 변경!
        //메인 카메라의 위치를 x 2.5 y 5 올려서 카메라의 시작점 즉 lb가 0,0에서 시작하도록 변경했음
        //이렇게 하면 바로 밑에 코드에서 원하는 좌표가 나오고 그 좌표가 곧 크기가 ㄷ

        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)); //오른쪽 빈 공간
        leftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)); //오른쪽 빈 공간
        center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f)); //오른쪽 빈 공간

        boardPadding = new Vector3(leftBottom.x + (rightTop.x - leftBottom.x) * 0.99f, leftBottom.y + (rightTop.y - leftBottom.y) * 0.3f);
        Debug.Log(leftBottom.x);
        Debug.Log(rightTop.x);


        //blockBoard = new Vector3(worldPointSize.x * 0.99f, worldPointSize.y * 0.3f);
        //Debug.Log(blockBoard);
        //Debug.Log(ScreenCenter);   //월드 좌표로 변환된 스크린의 크기
        



        width.text = "width: " + Screen.width.ToString();
        height.text = "height: " + Screen.height.ToString();
        
        resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        resetButton.onClick.AddListener(BlockSpriteReset);
    }
}
