using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private Button defaultButton;
    private Button hardButton;
    private Button expertButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        defaultButton = GameObject.Find("Default").GetComponent<Button>();
        hardButton = GameObject.Find("Hard").GetComponent<Button>();
        expertButton = GameObject.Find("Expert").GetComponent<Button>();

        defaultButton.onClick.AddListener(DefaultButtonClick);
        hardButton.onClick.AddListener(HardButtonClick);
        expertButton.onClick.AddListener(ExpertButtonClick);


    }

    void DefaultButtonClick()
    {
        ButtonClick(eBOARD_SIZE.DEFAULT);
    }

    void HardButtonClick()
    {
        ButtonClick(eBOARD_SIZE.HARD);

    }

    void ExpertButtonClick()
    {
        ButtonClick(eBOARD_SIZE.EXPERT);

    }

    void ButtonClick(eBOARD_SIZE size)
    {
        BoardManager.instance.SettingBoard(size);
    }

}
