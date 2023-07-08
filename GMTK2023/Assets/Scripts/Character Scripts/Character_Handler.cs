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

    [SerializeField] //todo remove
    public List<Character> characters = new();


    void Start()
    {
                
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

    // Converts Functions to Yarn Commands
    private void AddYarnCommands()
    {
        dialogueRunner.AddCommandHandler<Position, Character>("Enter", Enter);
        dialogueRunner.AddCommandHandler<Position>("Exit", Exit);
    }
}
