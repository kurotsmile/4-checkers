using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class checkers_item : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    [Header("Obj Asset")]
    public Sprite sp_attack;
    public Sprite sp_king;

    [Header("Obj config")]
    public Image img_checkers;
    public Image img_checkers_status;
    public int col;
    public int row;
    public int type;
    public player_direction direction;

    [Header("Obj sys")]
    public CanvasGroup canvas_group;
    public Animator ani;
    private checkers_tray tray_father;
    private Player_chess player_chess;
    private bool is_drag = false;
    private bool is_active = false;
    private bool is_king = false;

    public void onload(checkers_tray tr_father,Player_chess p)
    {
        this.is_king = false;
        tr_father.full();
        this.tray_father = tr_father;
        this.player_chess = p;
        this.unactive();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.is_active)
        {
            this.is_drag = true;
            this.canvas_group.blocksRaycasts = false;
            this.transform.SetParent(this.transform.root);
            GameObject.Find("Games").GetComponent<Games>().checkers_managaer.check_move(this);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.is_drag&&this.is_active)
        {
            Vector3 pos_mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(pos_mouse.x, pos_mouse.y, 0f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.is_drag = false;
        this.canvas_group.blocksRaycasts = true;
        this.transform.SetParent(this.tray_father.area_body);
        this.transform.localPosition = Vector3.zero;
        GameObject.Find("Games").GetComponent<Games>().checkers_managaer.uncheck_move();
    }

    public void set_tray_father_new(checkers_tray tray_new)
    {
        tray_new.full();
        this.tray_father = tray_new;
    }

    public checkers_tray get_tray_father()
    {
        return this.tray_father;
    }

    public void active()
    {
        this.is_active = true;
        this.ani.Play("checkers_item_act");
    }

    public void unactive()
    {
        this.is_active = false;
        this.ani.Play("checkers_item_nomal");
    }

    public void die()
    {
        this.tray_father.unFull();
        Destroy(this.gameObject);
        this.player_chess.check_gameover();
    }

    public void level_up()
    {
        if (this.is_king == false)
        {
            this.direction = player_direction.all;
            this.is_king = true;
            this.img_checkers_status.sprite = this.sp_king;
            GameObject.Find("Games").GetComponent<Games>().play_sound(4);
        }
    }

    public Player_chess get_player()
    {
        return this.player_chess;
    }

    public void click()
    {
        if (this.is_active)
            GameObject.Find("Games").GetComponent<Games>().checkers_managaer.check_move_buy_click(this);
        else
            GameObject.Find("Games").GetComponent<Games>().play_sound(2);

    }
}
