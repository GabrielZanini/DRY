using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    public Jumping(PlayerStateMachine player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        player.animator.SetBool("Jumping", true);
        player.rigidbody.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
    }

    public override void Tick()
    {
        if (player.rigidbody.velocity.y <= 0f)
        {
            player.SetState(new Falling(player));
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

    }
}
