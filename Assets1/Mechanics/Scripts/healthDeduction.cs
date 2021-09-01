using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthDeduction : MonoBehaviour
{
    [SerializeField] Sprite replacement;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void SpriteExchange()
    {
        spriteRenderer.sprite = replacement;
    }
}
