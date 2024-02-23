using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ani_Obj : MonoBehaviour
{
    public Animator ani;

    public void play_ani_home()
    {
        this.ani.enabled = true;
        this.ani.Play("ani_game");
        
    }

    public void play_ani_gameplay()
    {
        this.ani.enabled = true;
        this.ani.Play("ani_game_play");
    }


    public void stop_ani()
    {
        this.ani.enabled = false;
    }
}
