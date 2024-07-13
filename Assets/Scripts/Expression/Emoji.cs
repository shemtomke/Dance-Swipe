using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    private void Update()
    {
        spriteRenderer.sprite = sprite;
        Destroy(this.gameObject, 2f);
    }
}
