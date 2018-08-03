using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
    public Idle(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {

    }

    public override void Tick()
    {
        if (player.horizontal != 0)
        {
            player.SetState(new Running(player));
        }
        else if (player.isGrounded && player.InputJumpDown)
        {
            player.SetState(new Jumping(player));
        }
        else
        {
            player.Attacking();
        }
    }

    public override void FixedTick()
    {

    }

    public override void OnStateExit()
    {

    }
}
