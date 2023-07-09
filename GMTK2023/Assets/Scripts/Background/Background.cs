using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [Header("Sprites")]
    [SerializeField]
    private List<Sprite> sprites = new();

    private Dictionary<string, Sprite> spriteLookup = new();

    private SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        foreach (Sprite i in sprites)
            spriteLookup[i.name] = i;
        
    }

    void Update()
    {
        
    }

    public void SetSprite(string sprite) {
        if (!spriteLookup.ContainsKey(sprite))
            return;

        sr.sprite = spriteLookup[sprite];
    }

}
