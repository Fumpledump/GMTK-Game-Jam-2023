using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {


    protected Bounds cameraBounds;
    public enum State {
        Active,
        Inactive,
        Appearing,
        Disappearing,
    }


    [Header("Constants")]
    [SerializeField]
    protected float SIDE_BUFFER = 2f;
    [SerializeField]
    protected float SPEED = 8f;

    [Header("Default State")]
    [SerializeField]
    protected State state = State.Inactive;

    [Header("Sprites")]
    [SerializeField]
    protected List<Sprite> sprites = new();

    protected Character_Handler.Position position;
    protected SpriteRenderer sr;

    private float initX;
    private float initY;

    private Dictionary<Character_Handler.Position, Vector2> entranceTargets = new() {
        { Character_Handler.Position.Left, new() },
        { Character_Handler.Position.Right, new() },
        { Character_Handler.Position.Middle, new() },
    };

    private Dictionary<Character_Handler.Position, Vector2> exitTargets = new() {
        { Character_Handler.Position.Left, new() },
        { Character_Handler.Position.Right, new() },
        { Character_Handler.Position.Middle, new() },
    };
    private Character_Handler ch;

    private Dictionary<string, Sprite> spriteLookup = new();

    void Awake() {
        cameraBounds = new(Camera.main.transform.position, new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2, 0));
        initX = transform.position.x;
        initY = transform.position.y;
        sr = GetComponent<SpriteRenderer>();

        foreach (Sprite i in sprites)
            spriteLookup[i.name] = i;

        UpdateTargets();
    }


    void Start() {
        if (state == State.Inactive)
            sr.enabled = false;

        ch = GameObject.FindGameObjectWithTag("Character_Manager").GetComponent<Character_Handler>();
    }
    private void UpdateTargets() {
        entranceTargets[Character_Handler.Position.Left] = new(cameraBounds.center.x - cameraBounds.extents.x + SIDE_BUFFER, initY);
        entranceTargets[Character_Handler.Position.Right] = new(cameraBounds.center.x + cameraBounds.extents.x - SIDE_BUFFER, initY);
        entranceTargets[Character_Handler.Position.Middle] = new(initX, initY);

        exitTargets[Character_Handler.Position.Left] = new(cameraBounds.center.x - cameraBounds.extents.x - sr.bounds.extents.x, initY);
        exitTargets[Character_Handler.Position.Right] = new(cameraBounds.center.x + cameraBounds.extents.x + sr.bounds.extents.x, initY);
        exitTargets[Character_Handler.Position.Middle] = new(initX, cameraBounds.center.y - cameraBounds.extents.y - sr.bounds.extents.y);

    }


    void Update() {
        AppearingUpdate();
        DisappearingUpdate();
    }

    private void AppearingUpdate() {
        if (state == State.Appearing) {
            switch (position) {
                case Character_Handler.Position.Left:
                    transform.position = new Vector3(transform.position.x + SPEED * Time.deltaTime, initY, transform.position.z);
                    break;
                case Character_Handler.Position.Right:
                    transform.position = new Vector3(transform.position.x - SPEED * Time.deltaTime, initY, transform.position.z);
                    break;
                case Character_Handler.Position.Middle:
                    transform.position = new Vector3(initX, transform.position.y + SPEED * Time.deltaTime, transform.position.z);
                    break;
            }

            if (IsDoneEntering(position))
                SetState(State.Active);
        }
    }

    private void DisappearingUpdate() {
        if (state == State.Disappearing) {
            switch (position) {
                case Character_Handler.Position.Left:
                    transform.position = new Vector3(transform.position.x - SPEED * Time.deltaTime, initY, transform.position.z);
                    break;
                case Character_Handler.Position.Right:
                    transform.position = new Vector3(transform.position.x + SPEED * Time.deltaTime, initY, transform.position.z);
                    break;
                case Character_Handler.Position.Middle:
                    transform.position = new Vector3(initX, transform.position.y - SPEED * Time.deltaTime, transform.position.z);
                    break;
            }

            if (IsDoneExiting(position)) {
                SetState(State.Inactive);
                ch.ExitNotify(this, position);
            }
        }
    }

    private bool IsDoneEntering(Character_Handler.Position position) {
        switch (position) {
            case Character_Handler.Position.Left:
                return transform.position.x > entranceTargets[Character_Handler.Position.Left].x;
            case Character_Handler.Position.Right:
                return transform.position.x < entranceTargets[Character_Handler.Position.Right].x;
            case Character_Handler.Position.Middle:
                return transform.position.y > entranceTargets[Character_Handler.Position.Middle].y;
        }
        return false;
    }
    private bool IsDoneExiting(Character_Handler.Position position) {
        switch (position) {
            case Character_Handler.Position.Left:
                return transform.position.x < exitTargets[Character_Handler.Position.Left].x;
            case Character_Handler.Position.Right:
                return transform.position.x > exitTargets[Character_Handler.Position.Right].x;
            case Character_Handler.Position.Middle:
                return transform.position.y < exitTargets[Character_Handler.Position.Middle].y;
        }
        return false;
    }

    //UNSAFE Instead, call Character_Handler.Enter
    public void Enter(Character_Handler.Position position) {
        if (this.position == position && (state == State.Appearing || state == State.Active))
            return;

        // if already supposed to be active somewhere else
        if (state != State.Inactive && this.position != position)
            ch.ExitNotify(this, this.position);

        if (!(state == State.Disappearing && this.position == position))
            transform.position = new(exitTargets[position].x, exitTargets[position].y, transform.position.z);

        SetState(State.Appearing);
        this.position = position;
    }

    //safe probably but use Character_Handler.Exit if possible
    public void Exit(Character_Handler.Position position) {
        if (state == State.Inactive || (this.position == position && state == State.Disappearing))
            return;

        // if already supposed to be active somewhere else
        if (state != State.Inactive && this.position != position)
            ch.ExitNotify(this, this.position);

        if (!(state == State.Appearing && this.position == position))
            transform.position = new(entranceTargets[position].x, entranceTargets[position].y, transform.position.z);

        SetState(State.Disappearing);
        this.position = position;
    }

    private void SetState(State state) {

        switch (state) {
            case (State.Inactive):
                sr.enabled = false;
                break;
            case State.Appearing:
                sr.enabled = true;
                break;
            case State.Active:
                sr.enabled = true;
                break;
        }

        this.state = state;
    }

    private void AdjustEnter() {
        transform.position = new(entranceTargets[position].x, entranceTargets[position].y, transform.position.z);
    }

    private void AdjustExit() {
        transform.position = new(exitTargets[position].x, exitTargets[position].y, transform.position.z);
    }

    private void Adjust() {
        if (state == State.Active)
            AdjustEnter();
        else if (state == State.Inactive)
            AdjustExit();
    }
    
    public void SetSprite(string sprite) {
        if (!spriteLookup.ContainsKey(sprite))
            return;

        sr.sprite = spriteLookup[sprite];
        UpdateTargets();
        Adjust();
    }
}
