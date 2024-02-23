using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkers_row : MonoBehaviour
{
    public checkers_tray[] tray;
    private int index_row;

    public void load(int index_row,Color32 color_a,Color32 color_b)
    {
        this.index_row = index_row;
        for(int i = 0; i < this.tray.Length; i++)
        {
            if (this.index_row % 2 == 0)
            {
                if (i % 2 == 0)
                    this.tray[i].set_color_bk_a(color_a);
                else
                    this.tray[i].set_color_bk_b(color_b);
            }
            else
            {
                if (i % 2 == 0)
                    this.tray[i].set_color_bk_b(color_b);
                else
                    this.tray[i].set_color_bk_a(color_a);
            }

            this.tray[i].col = i;
            this.tray[i].row = index_row;
        }
    }

    public checkers_tray[] get_list_tray()
    {
        return this.tray;
    }

    public checkers_tray get_tray(int col)
    {
        return this.tray[col];
    }

    public void reset()
    {
        for (int i = 0; i < this.tray.Length; i++)
        {
            GameObject.Find("Games").GetComponent<Games>().carrot.clear_contain(this.tray[i].area_body);
            this.tray[i].reset();
        }
    }

    public void unPlan_all_item()
    {
        for (int i = 0; i < this.tray.Length; i++)
        {
            this.tray[i].unPlan();
            this.tray[i].unAttack();
        }
    }

    public void set_active(bool is_act)
    {
        for (int i = 0; i < this.tray.Length; i++)
        {
            if (is_act) this.tray[i].active();
            else this.tray[i].unActive();
        }
    }

    public void change_theme(Color32 color_a,Color32 color_b)
    {
        for(int i = 0; i < this.tray.Length; i++)
        {
            if (!this.tray[i].get_status_block())
            {
                if (this.tray[i].get_color_status_a())
                    this.tray[i].set_color_bk_a(color_a);
                else
                    this.tray[i].set_color_bk_b(color_b);
            }
        }
    }

}
