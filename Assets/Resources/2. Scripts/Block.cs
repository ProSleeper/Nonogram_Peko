using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockState
    {
        DEFAULT,
        NUMBER,
        X_OUTPUT
    }

    private SpriteRenderer sprite;
    private Sprite[] spriteArray;
    public BlockState blockState;

    public int arrayXpos = 0;
    public int arrayYpos = 0;


    public (int x, int y) ArrayPosition()
    {
        return (arrayXpos, arrayYpos);
    }

    public void SpriteReset()
    {
        sprite.sprite = spriteArray[0]; 
    }
    
    public void SpriteChange()
    {
        sprite.sprite = spriteArray[1];
    }

    //private void OnMouseDown()
    //{
    //    SpriteChange();
    //}

    //private void OnMouseDrag()
    //{
    //    SpriteChange();
    //    Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //}

    // Start is called before the first frame update
    void Start()
    {
        spriteArray = Resources.LoadAll<Sprite>("3. Textures/Board/tiles48");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        blockState = BlockState.DEFAULT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
