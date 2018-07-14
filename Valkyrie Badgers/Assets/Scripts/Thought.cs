using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Thought : MonoBehaviour {

    public Sprite thoughtSprite;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = thoughtSprite;
    }
}
