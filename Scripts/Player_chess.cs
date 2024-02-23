using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum player_direction
{
    top,left,rigth,bottom,all
}

public class Player_chess : MonoBehaviour
{
    [Header("Asset Obj")]
    public Sprite sp_robot;
    public Sprite sp_human;

    [Header("Player Obj")]
    public Image image_icon;
    public Image image_model_icon;
    public Text txt_name_player;
    public Text txt_scores_player;
    public player_direction direction;
    public Animator ani;
    public GameObject obj_btn_skip;
    public GameObject obj_btn_model;

    public bool is_gameover = false;
    public bool is_robot = false;

    private int scores = 0;

    private List<checkers_item> list_checker;

    public void on_load(string s_name,Sprite sp_avatar)
    {
        this.image_icon.sprite = sp_avatar;
        this.txt_name_player.text = s_name;
        this.list_checker = new List<checkers_item>();
        this.update_mode_ui();
    }

    public void add_scores()
    {
        this.scores++;
        this.txt_scores_player.text = this.scores.ToString();
    }

    public void add_ches(checkers_item item_c)
    {
        this.list_checker.Add(item_c);
    }

    public void set_active_all_checker(bool is_act)
    {
        for (int i = 0; i < this.list_checker.Count; i++)
        {
            if (this.list_checker[i] != null)
            {
                if (is_act)
                    this.list_checker[i].active();
                else
                    this.list_checker[i].unactive();
            }
        }

        if (is_act)
        {
            this.ani.Play("Chess_player_attack");
            if (!this.is_robot) this.obj_btn_skip.SetActive(true);
        }
        else
        {
            this.ani.Play("Chess_player");
            this.obj_btn_skip.SetActive(false);
        }    
    }

    public void check_gameover()
    {
        int count_null = 0;
        for (int i = 0; i < this.list_checker.Count; i++)
        {
            if (this.list_checker[i] == null) count_null++;
        }

        if (count_null >= this.list_checker.Count-1)
        {
            GameObject.Find("Games").GetComponent<Games>().checkers_managaer.show_done();
            this.is_gameover = true;
        }
    }

    public void set_robot(bool is_robot)
    {
        this.is_robot = is_robot;
        if (this.is_robot)
            this.obj_btn_skip.SetActive(false);
        else
            this.obj_btn_skip.SetActive(true);
        this.update_mode_ui();
    }

    public void btn_skip()
    {
        GameObject.Find("Games").GetComponent<Games>().checkers_managaer.next_player();
    }

    public void play_robot()
    {
        if (this.is_gameover == false)
        {
            List<robot_plan> list_r = new List<robot_plan>();
            for (int i = 0; i < this.list_checker.Count; i++)
            {
                if (this.list_checker[i] != null)
                {
                    robot_plan r = GameObject.Find("Games").GetComponent<Games>().checkers_managaer.check_move_robot(this.list_checker[i]);
                    if (r.list_tray.Count > 0) list_r.Add(r);
                }
            }

            int random_r = Random.Range(0, list_r.Count);
            robot_plan r_play = list_r[random_r];
            checkers_tray tray_play = r_play.get_tray_random();
            tray_play.active();
            tray_play.move_to(r_play.item_select);
        }
    }

    public void btn_change_mode()
    {
        if (this.is_robot)
            this.is_robot = false;
        else
            this.is_robot = true;
        this.update_mode_ui();
    }

    private void update_mode_ui()
    {
        if (this.is_robot)
            this.image_model_icon.sprite = this.sp_human;
        else
            this.image_model_icon.sprite = this.sp_robot;
    }
}
