using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Background : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner; // Yarn Spinner Dialogue Runner

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

        dialogueRunner.AddCommandHandler<string>("SetBackground", SetBackground);
    }

    public void SetBackground(string sprite) {
        if (!spriteLookup.ContainsKey(sprite))
            return;

        sr.sprite = spriteLookup[sprite];
    }
}
