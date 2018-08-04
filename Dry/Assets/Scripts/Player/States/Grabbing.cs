using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : PlayerState
{
    public Grabbing(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.animator.SetBool("GrabbingOnWall", true);
        player.rigidbody.useGravity = false;
    }

    public override void Tick()
    {
        player.GrabbingPosition();

        if (player.InputJumpDown)
        {
            player.SetState(new Jumping(player));
        }
    }

    public override void FixedTick()
    {

    }

    public override void OnStateExit()
    {
        player.animator.SetBool("GrabbingOnWall", false);
        player.rigidbody.useGravity = true;
    }
}