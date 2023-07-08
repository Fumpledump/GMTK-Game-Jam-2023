using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using static Character_Handler;

public class Character_Handler : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner; // Yarn Spinner Dialogue Runner

    public enum Position {
        Left,
        Right,
        Middle
    }

    Dictionary<Position, Character> slots = new() {
        { Position.Left, null },
        { Position.Right, null },
        { Position.Middle, null },
    };

    Dictionary<Position, Character> awaitingSlots = new() {
        { Position.Left, null },
        { Position.Right, null },
        { Position.Middle, null },
    };

    [SerializeField]
    public List<Character> characters = new();

    private Dictionary<string, Character> characterLookup = new();
    private Dictionary<string, Position> positionLookup = new();

    void Awake()
    {
        foreach(Character i in characters)
            characterLookup[i.gameObject.name] = i;

        foreach (Position i in slots.Keys)
            positionLookup[i.ToString()] = i;

        AddYarnCommands();
    }

    public void ExitNotify(Character character, Position position) {
        if (slots[position] != character)
            return;

        Promote(position);
        if(slots[position] != null)
            slots[position].Enter(position);
    }

    private void Promote(Position position) {
        slots[position] = awaitingSlots[position];
        awaitingSlots[position] = null;
    }

    //USE THIS :)
    public void Enter(Position position, Character character) {
        if (slots[position] == null) {
            slots[position] = character;
            slots[position].Enter(position);
        } else if(slots[position] == character) {
            awaitingSlots[position] = null;
            slots[position].Enter(position);
        } else {
            awaitingSlots[position] = character;
            slots[position].Exit(position);
        }
    }

    // USE THIS :)
    public void Exit(Position position) {
        if(slots[position] != null) {
            slots[position].Exit(position);
        }
        awaitingSlots[position] = null;
    }

    // USE THIS :)
    public void SetSprite(Character character, string sprite) {
        character.SetSprite(sprite);
    }

    public void YarnEnter(string position, string character) {

        Debug.Log("YarnEnter:" + position + " " + character);

        if (!positionLookup.ContainsKey(position) || !characterLookup.ContainsKey(character))
            return;

        Enter(positionLookup[position], characterLookup[character]);
    }

    public void YarnExit(string position) {
        if (!positionLookup.ContainsKey(position))
            return;

        Exit(positionLookup[position]);
    }

    public void YarnSetSprite(string character, string sprite) {
        if (!characterLookup.ContainsKey(character))
            return;

        SetSprite(characterLookup[character], sprite);
    }

    // Converts Functions to Yarn Commands
    private void AddYarnCommands()
    {
        dialogueRunner.AddCommandHandler<string, string>("Enter", YarnEnter);
        dialogueRunner.AddCommandHandler<string>("Exit", YarnExit);
        dialogueRunner.AddCommandHandler<string, string>("SetSprite", YarnSetSprite);
    }
}
