using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class robot_plan
{
    public checkers_item item_select;
    public List<checkers_tray> list_tray = new List<checkers_tray>();

    public checkers_tray get_tray_random()
    {
        int r_index = Random.Range(0, this.list_tray.Count);
        return this.list_tray[r_index];
    }
}

public class checkers : MonoBehaviour
{
    [Header("Obj Main")]
    private int type_model = 0;
    public checkers_row[] c_row;
    public GameObject obj_Player_Prefab;
    public Transform[] arean_player;

    [Header("Obj Config")]
    public Color32 color_a;
    public Color32 color_b;
    public Color32 color_block;
    public Sprite[] sp_checkers;
    public Sprite[] sp_avatar_player;

    [Header("Panel done")]
    public GameObject panel_done;
    public Image img_icon_done_human_vs_robot;
    public Text txt_msg_done_human_vs_robot;
    public GameObject panel_done_human_vs_robot;
    public GameObject panel_done_human_vs_human;
    public GameObject panel_done_4human;

    [Header("Done Two")]
    public Text txt_msg_done_human_vs_human_player1;
    public Text txt_name_done_human_vs_human_player1;
    public Image img_icon_done_human_vs_human_player1;

    public Text txt_msg_done_human_vs_human_player2;
    public Text txt_name_done_human_vs_human_player2;
    public Image img_icon_done_human_vs_human_player2;

    [Header("Done 4 Player")]
    public Text[] txt_msg_done_vs4_player;
    public Text[] txt_name_done_vs4_player;
    public Image[] img_icon_done_vs4_player;

    public GameObject checkers_item_prefab;

    private List<Player_chess> list_player_chess;
    private int sel_index_player = -1;
    private checkers_item checkers_click_temp;

    public void load_game_by_model(int type_model)
    {
        this.type_model = type_model;
        this.panel_done.SetActive(false);
        this.clear_all_player();
        this.list_player_chess = new List<Player_chess>();

        for (int i = 0; i < this.c_row.Length; i++) this.c_row[i].reset();

        for (int i = 0; i < this.c_row.Length; i++)
        {
            this.c_row[i].load(i, this.color_a, this.color_b);
        }


        if (this.type_model == 0 || this.type_model == 1)
        {
            if (this.type_model == 0)
            {
                this.create_player(this.arean_player[0], 180, "Robot",this.sp_avatar_player[0]);
                this.create_player(this.arean_player[1], 0, "You",this.sp_avatar_player[1]);
                this.list_player_chess[0].set_robot(true);
            }
            else
            {
                this.create_player(this.arean_player[0], 180, "Player 1",this.sp_avatar_player[2]);
                this.create_player(this.arean_player[1], 0, "Player 2", this.sp_avatar_player[3]);
            }


            this.list_player_chess[0].direction = player_direction.top;
            this.list_player_chess[1].direction = player_direction.bottom;

            this.arean_player[2].gameObject.SetActive(false);
            this.arean_player[3].gameObject.SetActive(false);

            this.create_checkers_two_player(0, 0, this.list_player_chess[0],180f);
            this.create_checkers_two_player(1, 0, this.list_player_chess[0],180f);
            this.create_checkers_two_player(2, 0, this.list_player_chess[0],180f);

            this.create_checkers_two_player(5, 1, this.list_player_chess[1],0f);
            this.create_checkers_two_player(6, 1, this.list_player_chess[1],0f);
            this.create_checkers_two_player(7, 1, this.list_player_chess[1],0f);
        }

        if (this.type_model == 2 || this.type_model == 3)
        {



            if (this.type_model == 2)
            {
                this.create_player(this.arean_player[0], 180, "Player 1", this.sp_avatar_player[2]);
                this.create_player(this.arean_player[3], -90f, "Player 2", this.sp_avatar_player[3]);
                this.create_player(this.arean_player[1], 0, "Player 3", this.sp_avatar_player[4]);
                this.create_player(this.arean_player[2], 90f, "Player 4", this.sp_avatar_player[5]);
            }
            else
            {
                this.create_player(this.arean_player[0], 180, "Player 1", this.sp_avatar_player[2]);
                this.create_player(this.arean_player[3], -90f, "Player 2", this.sp_avatar_player[3]);
                this.create_player(this.arean_player[1], 0, "Player 3", this.sp_avatar_player[4]);
                this.create_player(this.arean_player[2], 90f, "Player 4", this.sp_avatar_player[5]);
                this.list_player_chess[0].set_robot(true);
                this.list_player_chess[2].set_robot(true);
            }

            this.list_player_chess[0].direction = player_direction.top;
            this.list_player_chess[1].direction = player_direction.rigth;
            this.list_player_chess[2].direction = player_direction.bottom;
            this.list_player_chess[3].direction = player_direction.left;

            this.arean_player[2].gameObject.SetActive(true);
            this.arean_player[3].gameObject.SetActive(true);

            this.delete_tray_by_pos(0, 0);
            this.delete_tray_by_pos(0, 1);
            this.delete_tray_by_pos(1, 0);
            this.delete_tray_by_pos(1, 1);

            this.delete_tray_by_pos(0, 6);
            this.delete_tray_by_pos(0, 7);
            this.delete_tray_by_pos(1, 6);
            this.delete_tray_by_pos(1, 7);

            this.delete_tray_by_pos(6, 0);
            this.delete_tray_by_pos(6, 1);
            this.delete_tray_by_pos(7, 0);
            this.delete_tray_by_pos(7, 1);

            this.delete_tray_by_pos(6, 6);
            this.delete_tray_by_pos(6, 7);
            this.delete_tray_by_pos(7, 6);
            this.delete_tray_by_pos(7, 7);

            this.create_checkers_by_pos(0, 2, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(0, 3, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(0, 4, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(0, 5, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(1, 2, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(1, 3, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(1, 4, 0, this.list_player_chess[0], 180f);
            this.create_checkers_by_pos(1, 5, 0, this.list_player_chess[0], 180f);

            this.create_checkers_by_pos(2, 0, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(3, 0, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(4, 0, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(5, 0, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(2, 1, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(3, 1, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(4, 1, 2, this.list_player_chess[1], -90f);
            this.create_checkers_by_pos(5, 1, 2, this.list_player_chess[1], -90f);

            this.create_checkers_by_pos(6, 2, 1, this.list_player_chess[2],0f);
            this.create_checkers_by_pos(6, 3, 1, this.list_player_chess[2],0f);
            this.create_checkers_by_pos(6, 4, 1, this.list_player_chess[2], 0f);
            this.create_checkers_by_pos(6, 5, 1, this.list_player_chess[2], 0f);
            this.create_checkers_by_pos(7, 2, 1, this.list_player_chess[2], 0f);
            this.create_checkers_by_pos(7, 3, 1, this.list_player_chess[2], 0f);
            this.create_checkers_by_pos(7, 4, 1, this.list_player_chess[2], 0f);
            this.create_checkers_by_pos(7, 5, 1, this.list_player_chess[2], 0f);

            this.create_checkers_by_pos(2, 6, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(3, 6, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(4, 6, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(5, 6, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(2, 7, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(3, 7, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(4, 7, 3, this.list_player_chess[3], 90f);
            this.create_checkers_by_pos(5, 7, 3, this.list_player_chess[3], 90f);
        }

        this.select_player_first();
    }

    public void select_player_first()
    {
        this.sel_index_player = Random.Range(0, this.list_player_chess.Count);
        this.sel_player(this.sel_index_player);
    }

    public void create_player(Transform area_player, float deg, string s_name,Sprite sp_avatar)
    {
        GameObject obj_player = Instantiate(this.obj_Player_Prefab);
        obj_player.transform.SetParent(area_player);
        obj_player.transform.localScale = new Vector3(1f, 1f, 1f);
        obj_player.transform.localPosition = new Vector3(0f, 0f, 0f);
        obj_player.transform.localRotation = Quaternion.Euler(0f, 0f, deg);
        Player_chess player_chess = obj_player.GetComponent<Player_chess>();
        player_chess.on_load(s_name,sp_avatar);
        this.list_player_chess.Add(player_chess);

    }

    public void create_checkers_two_player(int index_row, int type_checker, Player_chess player_chess,float deg)
    {
        checkers_tray[] arr_tray = this.c_row[index_row].get_list_tray();
        for (int i = 0; i < arr_tray.Length; i++)
        {
            if (index_row % 2 == 0)
            {
                if (i % 2 == 0) this.create_checkers(arr_tray[i], type_checker, index_row, i, player_chess,deg);
            }
            else
            {
                if (i % 2 != 0) this.create_checkers(arr_tray[i], type_checker, index_row, i, player_chess,deg);
            }
        }
    }

    public void create_checkers_by_pos(int index_row, int col, int type_c, Player_chess player_chess,float deg)
    {
        this.create_checkers(this.c_row[index_row].get_tray(col), type_c, index_row, col, player_chess,deg);
    }

    public void delete_tray_by_pos(int index_row, int col)
    {
        this.c_row[index_row].get_tray(col).hide(this.color_block);
    }

    private void create_checkers(checkers_tray tray, int type_checker, int c_row, int c_col, Player_chess player,float deg)
    {
        GameObject obj_checkers = Instantiate(this.checkers_item_prefab);
        obj_checkers.transform.SetParent(tray.area_body);
        obj_checkers.transform.localPosition = Vector3.zero;
        obj_checkers.transform.localScale = new Vector3(1f, 1f, 1f);
        obj_checkers.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, deg));
        checkers_item item_checkers = obj_checkers.GetComponent<checkers_item>();
        item_checkers.onload(tray,player);
        item_checkers.img_checkers.sprite = this.sp_checkers[type_checker];
        item_checkers.row = c_row;
        item_checkers.col = c_col;
        item_checkers.type = type_checker;
        item_checkers.direction = player.direction;
        tray.set_checkers_cur(item_checkers);
        player.add_ches(item_checkers);
    }

    public void on_reset()
    {
        this.load_game_by_model(this.type_model);
    }

    public void check_move_buy_click(checkers_item c_item)
    {
        this.uncheck_move();
        this.checkers_click_temp = c_item;
        this.check_move(c_item);
    }

    public void check_move(checkers_item c_item)
    {
        this.set_active_all_tray(false);

        c_item.get_tray_father().plan();

        checkers_tray tray_left_top_attack = null;
        checkers_tray tray_right_top_attack = null;
        checkers_tray tray_left_bottom_attack = null;
        checkers_tray tray_right_bottom_attack = null;

        checkers_tray tray_left_top = null;
        checkers_tray tray_right_top = null;
        checkers_tray tray_left_bottom = null;
        checkers_tray tray_right_bottom = null;


        if (c_item.direction == player_direction.bottom)
        {
            if (check_in_board(c_item.row - 2, c_item.col - 2)) tray_left_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row - 2, c_item.col + 2)) tray_right_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row - 1, c_item.col - 1)) tray_left_top = this.c_row[c_item.row - 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row - 1, c_item.col + 1)) tray_right_top = this.c_row[c_item.row - 1].get_tray(c_item.col + 1);
        }
        else if (c_item.direction == player_direction.top)
        {
            if (check_in_board(c_item.row + 2, c_item.col - 2)) tray_left_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row + 2, c_item.col + 2)) tray_right_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row + 1, c_item.col - 1)) tray_left_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row + 1, c_item.col + 1)) tray_right_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col + 1);
        }
        else if (c_item.direction == player_direction.left)
        {
            if (check_in_board(c_item.row - 2, c_item.col - 2)) tray_left_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row + 2, c_item.col - 2)) tray_left_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col - 2);

            if (check_in_board(c_item.row - 1, c_item.col - 1)) tray_left_top = this.c_row[c_item.row - 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row + 1, c_item.col - 1)) tray_left_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col - 1);
        }
        else if (c_item.direction == player_direction.rigth)
        {
            if (check_in_board(c_item.row - 2, c_item.col + 2)) tray_right_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col + 2);
            if (check_in_board(c_item.row + 2, c_item.col + 2)) tray_right_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row - 1, c_item.col + 1)) tray_right_top = this.c_row[c_item.row - 1].get_tray(c_item.col + 1);
            if (check_in_board(c_item.row + 1, c_item.col + 1)) tray_right_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col + 1);
        }
        else if (c_item.direction == player_direction.all)
        {
            if (check_in_board(c_item.row - 2, c_item.col - 2)) tray_left_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row - 2, c_item.col + 2)) tray_right_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col + 2);
            if (check_in_board(c_item.row + 2, c_item.col - 2)) tray_left_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row + 2, c_item.col + 2)) tray_right_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row - 1, c_item.col - 1)) tray_left_top = this.c_row[c_item.row - 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row - 1, c_item.col + 1)) tray_right_top = this.c_row[c_item.row - 1].get_tray(c_item.col + 1);
            if (check_in_board(c_item.row + 1, c_item.col - 1)) tray_left_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row + 1, c_item.col + 1)) tray_right_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col + 1);
        }

        this.check_attack_and_plan_tray(tray_left_top_attack, tray_left_top, c_item);
        this.check_attack_and_plan_tray(tray_right_top_attack, tray_right_top, c_item);
        this.check_attack_and_plan_tray(tray_left_bottom_attack, tray_left_bottom, c_item);
        this.check_attack_and_plan_tray(tray_right_bottom_attack, tray_right_bottom, c_item);
    }

    private void check_attack_and_plan_tray(checkers_tray tray_attack, checkers_tray tray_plan, checkers_item c_check)
    {

        if (tray_plan != null)
        {
            if (!tray_plan.get_full_status()&&!tray_plan.get_status_block())
            {
                tray_plan.plan();
                tray_plan.active();
            }
        }

        if (tray_attack != null)
        {
            if (!tray_attack.get_full_status()&&!tray_attack.get_status_block())
            {
                if (tray_plan.get_full_status())
                {
                    checkers_item check_plan_die = tray_plan.get_checkers_in_tray();
                    if (c_check.type != check_plan_die.type)
                    {
                        tray_attack.set_checkers_die(check_plan_die);
                        tray_attack.attack();
                        tray_attack.active();
                    }
                }
            }
        }
    }

    private bool check_in_board(int row,int col)
    {
        if (col > -1 && col < 8 && row > -1 && row < 8) return true;
        else return false;
    }

    public void uncheck_move()
    {
        for (int i = 0; i < c_row.Length; i++) this.c_row[i].unPlan_all_item();
    }

    private void clear_all_player()
    {
        if(this.list_player_chess!=null)
        for (int i = 0; i < this.list_player_chess.Count; i++) Destroy(this.list_player_chess[i].gameObject);
    }

    public void next_player()
    {
        this.sel_index_player++;
        if (this.sel_index_player >= this.list_player_chess.Count) this.sel_index_player = 0;
        this.sel_player(this.sel_index_player);
    }

    private void sel_player(int index_player)
    {
        this.set_active_all_check(false);
        if (this.list_player_chess[index_player].is_gameover == false)
        {
            this.list_player_chess[index_player].set_active_all_checker(true);
            if (this.get_curent_player().is_robot)
            {
                this.GetComponent<Games>().carrot.delay_function(1.1f, this.get_curent_player().play_robot);
            }
        }
        else
        {
            this.next_player();
        }

    }

    private void set_active_all_check(bool is_act)
    {
        for (int i = 0; i < this.list_player_chess.Count; i++) this.list_player_chess[i].set_active_all_checker(is_act);
    }

    private void set_active_all_tray(bool is_act)
    {
        for (int i = 0; i < this.c_row.Length; i++) this.c_row[i].set_active(is_act);
    }

    public Player_chess get_curent_player()
    {
        return this.list_player_chess[this.sel_index_player];
    }

    public void show_done()
    {
        
        this.panel_done_human_vs_robot.SetActive(false);
        this.panel_done_human_vs_human.SetActive(false);
        this.panel_done_4human.SetActive(false);

        if (this.type_model == 0)
        {
            this.act_gameover();
            this.panel_done_human_vs_robot.SetActive(true);
            if (this.list_player_chess[0].is_gameover) {
                this.img_icon_done_human_vs_robot.sprite = this.sp_avatar_player[1];
                this.txt_msg_done_human_vs_robot.text = "You Win!";
                this.GetComponent<Games>().add_scores_player();
            }
            else
            {
                this.img_icon_done_human_vs_robot.sprite = this.sp_avatar_player[0];
                this.txt_msg_done_human_vs_robot.text = "You lose!";
            }
        }else if (this.type_model == 1)
        {
            this.act_gameover();
            this.panel_done_human_vs_human.SetActive(true);

            this.img_icon_done_human_vs_human_player1.sprite = this.list_player_chess[0].image_icon.sprite;
            this.img_icon_done_human_vs_human_player2.sprite = this.list_player_chess[1].image_icon.sprite;

            this.txt_name_done_human_vs_human_player1.text= this.list_player_chess[0].txt_name_player.text;
            this.txt_name_done_human_vs_human_player2.text= this.list_player_chess[1].txt_name_player.text;

            if (this.list_player_chess[0].is_gameover)
            {
                this.txt_msg_done_human_vs_human_player1.text= "You Win!";
                this.txt_msg_done_human_vs_human_player2.text= "You lose!";
            }
            else
            {
                this.txt_msg_done_human_vs_human_player1.text = "You lose!";
                this.txt_msg_done_human_vs_human_player2.text = "You Win!";
            }
        }
        else
        {
            int count_gameover = 0;
            for(int i = 0; i < this.list_player_chess.Count; i++)
            {
                if (this.list_player_chess[i].is_gameover==true) count_gameover++;
            }

            if (count_gameover >= 2)
            {
                for (int i = 0; i < this.list_player_chess.Count; i++)
                {
                    this.img_icon_done_vs4_player[i].sprite = this.list_player_chess[i].image_icon.sprite;
                    this.txt_name_done_vs4_player[i].text = this.list_player_chess[i].txt_name_player.text;

                    if (this.list_player_chess[i].is_gameover)
                        this.txt_msg_done_vs4_player[i].text = "You lose!";
                    else
                        this.txt_msg_done_vs4_player[i].text = "You Win!";
                }
                this.act_gameover();
                this.panel_done_4human.SetActive(false);
            }
        }
        
    }

    private void act_gameover()
    {
        this.panel_done.SetActive(true);
        this.GetComponent<Games>().play_sound(5);
    }

    public robot_plan check_move_robot(checkers_item c_item)
    {
        robot_plan r = new robot_plan();
        r.item_select = c_item;

        checkers_tray tray_left_top = null;
        checkers_tray tray_right_top = null;
        checkers_tray tray_left_bottom = null;
        checkers_tray tray_right_bottom = null;

        checkers_tray tray_left_top_attack = null;
        checkers_tray tray_right_top_attack = null;
        checkers_tray tray_left_bottom_attack = null;
        checkers_tray tray_right_bottom_attack = null;

        if (c_item.direction == player_direction.bottom)
        {
            if (check_in_board(c_item.row - 2, c_item.col - 2)) tray_left_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row - 2, c_item.col + 2)) tray_right_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row - 1, c_item.col - 1)) tray_left_top = this.c_row[c_item.row - 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row - 1, c_item.col + 1)) tray_right_top = this.c_row[c_item.row - 1].get_tray(c_item.col + 1);
        }
        else if (c_item.direction == player_direction.top)
        {
            if (check_in_board(c_item.row + 2, c_item.col - 2)) tray_left_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row + 2, c_item.col + 2)) tray_right_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row + 1, c_item.col - 1)) tray_left_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row + 1, c_item.col + 1)) tray_right_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col + 1);
        }
        else if (c_item.direction == player_direction.left)
        {
            if (check_in_board(c_item.row - 2, c_item.col - 2)) tray_left_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row + 2, c_item.col - 2)) tray_left_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col - 2);

            if (check_in_board(c_item.row - 1, c_item.col - 1)) tray_left_top = this.c_row[c_item.row - 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row + 1, c_item.col - 1)) tray_left_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col - 1);
        }
        else if (c_item.direction == player_direction.rigth)
        {
            if (check_in_board(c_item.row - 2, c_item.col + 2)) tray_right_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col + 2);
            if (check_in_board(c_item.row + 2, c_item.col + 2)) tray_right_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row - 1, c_item.col + 1)) tray_right_top = this.c_row[c_item.row - 1].get_tray(c_item.col + 1);
            if (check_in_board(c_item.row + 1, c_item.col + 1)) tray_right_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col + 1);
        }
        else if (c_item.direction == player_direction.all)
        {
            if (check_in_board(c_item.row - 2, c_item.col - 2)) tray_left_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row - 2, c_item.col + 2)) tray_right_top_attack = this.c_row[c_item.row - 2].get_tray(c_item.col + 2);
            if (check_in_board(c_item.row + 2, c_item.col - 2)) tray_left_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col - 2);
            if (check_in_board(c_item.row + 2, c_item.col + 2)) tray_right_bottom_attack = this.c_row[c_item.row + 2].get_tray(c_item.col + 2);

            if (check_in_board(c_item.row - 1, c_item.col - 1)) tray_left_top = this.c_row[c_item.row - 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row - 1, c_item.col + 1)) tray_right_top = this.c_row[c_item.row - 1].get_tray(c_item.col + 1);
            if (check_in_board(c_item.row + 1, c_item.col - 1)) tray_left_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col - 1);
            if (check_in_board(c_item.row + 1, c_item.col + 1)) tray_right_bottom = this.c_row[c_item.row + 1].get_tray(c_item.col + 1);
        }

        tray_left_top_attack=this.check_attack_and_plan_tray_robot(tray_left_top_attack, tray_left_top, c_item);
        tray_right_top_attack=this.check_attack_and_plan_tray_robot(tray_right_top_attack, tray_right_top, c_item);
        tray_left_bottom_attack=this.check_attack_and_plan_tray_robot(tray_left_bottom_attack, tray_left_bottom, c_item);
        tray_right_bottom_attack=this.check_attack_and_plan_tray_robot(tray_right_bottom_attack, tray_right_bottom, c_item);

        if (tray_left_top_attack!=null) r.list_tray.Add(tray_left_top_attack);
        if (tray_right_top_attack!=null) r.list_tray.Add(tray_right_top_attack);
        if (tray_left_bottom_attack != null) r.list_tray.Add(tray_left_bottom_attack);
        if (tray_right_bottom_attack!=null) r.list_tray.Add(tray_right_bottom_attack);

        return r;
    }

    private checkers_tray check_attack_and_plan_tray_robot(checkers_tray tray_attack, checkers_tray tray_plan, checkers_item c_check)
    {
        checkers_tray tray_sel=null;
        if (tray_plan != null)
        {
            if (!tray_plan.get_full_status()&& !tray_plan.get_status_block()) tray_sel = tray_plan;
        }

        if (tray_attack != null)
        {
            if (!tray_attack.get_full_status()&& !tray_attack.get_status_block())
            {
                if (tray_plan.get_full_status())
                {
                    checkers_item check_plan_die = tray_plan.get_checkers_in_tray();
                    if (c_check.type != check_plan_die.type)
                    {
                        tray_attack.set_checkers_die(check_plan_die);
                        tray_sel = tray_attack;
                    }
                }
            }
        }

        return tray_sel;
    }

    public checkers_item get_checkers_item_sel()
    {
        return this.checkers_click_temp;
    }

    public int get_type_mode()
    {
        return this.type_model;
    }

    public void back_home()
    {
        for(int i = 0; i < this.list_player_chess.Count; i++)
        {
            this.list_player_chess[i].is_gameover = true;
        }
    }

    public void load_theme()
    {
        for (int i = 0; i < this.c_row.Length; i++)
        {
            this.c_row[i].change_theme(this.color_a, this.color_b);
        }
    }
}
