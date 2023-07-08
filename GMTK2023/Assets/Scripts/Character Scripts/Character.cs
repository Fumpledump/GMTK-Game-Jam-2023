using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private const float SIDE_BUFFER = 30;

    protected Bounds cameraBounds;
    public enum State {
        Active,
        Inactive,
        Appearing,
        Disappearing,
    }


    [SerializeField]
    protected State state = State.Inactive;

    void Awake()
    {
        cameraBounds = new(Camera.main.transform.position, new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2, 0));
        
    }

    
    void Update()
    {
        
    }

    private void HasEntered() {

    }

    public void Activate(Character_Handler.Position position) {
        if (state == State.Active || state == State.Appearing)
            return;

        SetState(State.Appearing);

        //IEnumerator ActivateRoutine() {
        //    while(state == State.Appearing) {
        //
        //    }
        //}
    }


    public void SetState(State state) {

        switch (state) {
            case (State.Inactive):
                transform.gameObject.SetActive(false);
                break;
        }

        this.state = state;
    }

    public void Is() {

    }
}
