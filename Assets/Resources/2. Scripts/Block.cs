using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private SpriteRenderer sprite;
    private Sprite[] spriteArray;

    public void SpriteReset()
    {
        sprite.sprite = spriteArray[0]; 
    }
    
    void SpriteChange()
    {
        sprite.sprite = spriteArray[1];

    }

    private void OnMouseDown()
    {
        SpriteChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteArray = Resources.LoadAll<Sprite>("3. Textures/Board/tiles48");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
