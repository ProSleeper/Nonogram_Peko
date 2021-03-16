using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BoardManager : MonoBehaviour
{
    private const int DEFAULT_BOARDSIZE = 5;

    public static BoardManager instance = null;
    private Button resetButton;

    private int COLUMN = DEFAULT_BOARDSIZE;
    private int ROW = DEFAULT_BOARDSIZE;
    private int boardLevel = 0;
    private float drawSize = 0;
    private GameObject block;
    private List<GameObject> blockList = new List<GameObject>();



    private void BlockSpriteReset()
    {
        foreach (GameObject item in blockList)
        {
            item.GetComponent<Block>().SpriteReset(); 
        }
    }

    private void Awake()
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


        drawSize = 0.75f;
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COLUMN; j++)
            {
                block = Instantiate(Resources.Load("4. Prefabs/Block"), new Vector3(i * drawSize, j * drawSize), Quaternion.identity) as GameObject;
                block.transform.localScale = new Vector3(drawSize, drawSize, 1);
                blockList.Add(block);
            }
        }

        resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        resetButton.onClick.AddListener(BlockSpriteReset);

        //blockList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
