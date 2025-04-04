using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerRunState runState = new PlayerRunState();

    public PlayerHandleInput InputHandler { get; private set; }
    public PlayerController Controller { get; private set; }
    public Animator Animator { get; private set; }
    

    private void Start()
    {
        InputHandler = GetComponent<PlayerHandleInput>();
        Controller = GetComponent<PlayerController>();
        Animator = GetComponent<Animator>();

        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.HandleStateInput(this);
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState?.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
