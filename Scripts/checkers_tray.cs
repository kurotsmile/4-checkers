using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class checkers_tray : MonoBehaviour,IDropHandler
{
    public Image img_bk;
    public Transform area_body;
    public Animator ani;
    public int row;
    public int col;
    public GameObject obj_attack;

    private bool is_full = false;
    private bool is_active = false;
    private bool is_block = false;

    private checkers_item checkers_dice;
    private checkers_item checkers_cur;
    private bool is_color_a = false;

    public void hide(Color32 color_block)
    {
        this.is_block = true;
        this.img_bk.color = color_block;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            checkers_item c_item = eventData.pointerDrag.GetComponent<checkers_item>();
            this.move_to(c_item);
        }
    }

    public void move_to(checkers_item c_item)
    {
        if (this.is_full == false && this.is_active == true)
        {
            c_item.get_tray_father().unFull();
            c_item.set_tray_father_new(this);
            c_item.transform.SetParent(this.transform);
            c_item.transform.localPosition = Vector3.zero;
            c_item.transform.localScale = new Vector3(1f, 1f, 1f);
            c_item.col = this.col;
            c_item.row = this.row;
            this.is_full = true;
            this.checkers_cur = c_item;
            if (checkers_dice != null)
            {
                GameObject.Find("Games").GetComponent<Games>().play_sound(3);
                GameObject.Find("Games").GetComponent<Games>().checkers_managaer.get_curent_player().add_scores();
                GameObject.Find("Games").GetComponent<Games>().create_effect(0, checkers_dice.transform.position);
                checkers_dice.die();
            }
            GameObject.Find("Games").GetComponent<Games>().play_sound(1);
            GameObject.Find("Games").GetComponent<Games>().checkers_managaer.next_player();
            GameObject.Find("Games").GetComponent<Games>().checkers_managaer.uncheck_move();

            
            this.check_checker_level_up(c_item);
        }
        else
        {
            GameObject.Find("Games").GetComponent<Games>().play_sound(2);
        }
    }

    private void check_checker_level_up(checkers_item c_check)
    {
        if (c_check.direction == player_direction.bottom&&this.row==0)
        {
            c_check.level_up();
        }else if (c_check.direction == player_direction.top && this.row == 7)
        {
            c_check.level_up();
        }
        else if (c_check.direction == player_direction.left && this.col == 0)
        {
            c_check.level_up();
        }
        else if (c_check.direction == player_direction.rigth && this.col == 7)
        {
            c_check.level_up();
        }
    }

    public void reset()
    {
        if (this.checkers_cur != null) Destroy(this.checkers_cur.gameObject);
        if (this.checkers_dice != null) Destroy(this.checkers_dice.gameObject);
        this.checkers_cur = null;
        this.checkers_dice = null;
        this.obj_attack.SetActive(false);
        this.is_full = false;
        this.unPlan();
    }

    public void plan()
    {
        this.ani.Play("tray_checker");
    }

    public void unPlan()
    {
        this.ani.Play("tray_checker_nomal");
    }

    public void unFull()
    {
        this.checkers_cur = null;
        this.is_full = false;
    }

    public void full()
    {
        this.is_full = true;
    }

    public bool get_full_status()
    {
        return this.is_full;
    }

    public void active()
    {
        this.is_active = true;
    }

    public void unActive()
    {
        this.checkers_dice = null;
        this.is_active = false;
    }

    public void attack()
    {
        this.ani.Play("tray_checker_attack");
        this.obj_attack.SetActive(true);
    }

    public void unAttack()
    {
        this.obj_attack.SetActive(false);
    }

    public void set_checkers_die(checkers_item c)
    {
        this.checkers_dice = c;
    }

    public void set_checkers_cur(checkers_item c)
    {
        this.checkers_cur = c;
    }

    public checkers_item get_checkers_in_tray()
    {
        return this.checkers_cur;
    }

    public void click()
    {
        this.move_to(GameObject.Find("Games").GetComponent<Games>().checkers_managaer.get_checkers_item_sel());
    }

    public bool get_status_block()
    {
        return this.is_block;
    }

    public void set_color_bk_a(Color32 color_bk)
    {
        this.is_color_a = true;
        this.img_bk.color= color_bk;
    }

    public void set_color_bk_b(Color32 color_bk)
    {
        this.is_color_a = false;
        this.img_bk.color = color_bk;
    }

    public bool get_color_status_a()
    {
        return this.is_color_a;
    }
}
