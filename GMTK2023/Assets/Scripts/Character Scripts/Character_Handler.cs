using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Handler : MonoBehaviour
{

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


    void Update()
    {
        //todo remove testing
        if (Input.GetKeyDown(KeyCode.Z))
            Enter(Position.Left, characters.ToArray()[0]);
        if (Input.GetKeyDown(KeyCode.X))
            Exit(Position.Left);
        if (Input.GetKeyDown(KeyCode.C))
            Enter(Position.Right, characters.ToArray()[0]);
        if (Input.GetKeyDown(KeyCode.V))
            Exit(Position.Right);
        if (Input.GetKeyDown(KeyCode.B))
            Enter(Position.Middle, characters.ToArray()[0]);
        if (Input.GetKeyDown(KeyCode.N))
            Exit(Position.Middle);


        if (Input.GetKeyDown(KeyCode.A))
            Enter(Position.Left, characters.ToArray()[1]);
        if (Input.GetKeyDown(KeyCode.S))
            Exit(Position.Left);
        if (Input.GetKeyDown(KeyCode.D))
            Enter(Position.Right, characters.ToArray()[1]);
        if (Input.GetKeyDown(KeyCode.F))
            Exit(Position.Right);
        if (Input.GetKeyDown(KeyCode.G))
            Enter(Position.Middle, characters.ToArray()[1]);
        if (Input.GetKeyDown(KeyCode.H))
            Exit(Position.Middle);


        if (Input.GetKeyDown(KeyCode.K))
            characters[0].SetSprite(Random.Range(0,8));
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


    public void Exit(Position position) {
        if(slots[position] != null) {
            slots[position].Exit(position);
        }
        awaitingSlots[position] = null;
    }
}
