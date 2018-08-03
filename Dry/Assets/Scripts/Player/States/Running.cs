using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : PlayerState
{
    public Running(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.animator.SetBool("Running", true);
    }


    public override void Tick()
    {
        if (player.horizontal == 0)
        {
            player.SetState(new Idle(player));
        }
        else if(player.isGrounded && player.InputJumpDown)
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
        player.Moving();
    }

    public override void OnStateExit()
    {
        player.animator.SetBool("Running", false);
    }
}
