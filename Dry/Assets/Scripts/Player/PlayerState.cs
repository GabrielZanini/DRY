using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine player;

    public abstract void Tick();
    public abstract void FixedTick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public PlayerState(PlayerStateMachine player)
    {
        this.player = player;
    }
}
