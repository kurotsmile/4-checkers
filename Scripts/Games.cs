using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Games : MonoBehaviour
{
    [Header("Obj Game")]
    public Carrot.Carrot carrot;
    public checkers checkers_managaer;
    public Ani_Obj ani;

    [Header("Panel Game")]
    public GameObject panel_home;
    public GameObject panel_play;
    public GameObject panel_tip;
    public Text txt_scores_home;

    [Header("Sounds")]
    public AudioSource[] sounds;

    [Header("Effect")]
    public GameObject[] effect_die_prefab;

    [Header("Shop")]
    public Sprite sp_icon_shop;
    public Sprite sp_icon_shop_watch_ads;
    public Sprite sp_icon_shop_buy;
    public Sprite[] sp_icon_item_shop;
    public string[] s_title_item_shop;
    public string[] s_tip_item_shop;
    public bool[] is_buy_item_shop;

    public Color32[] color_bk;
    public Color32[] color_a;
    public Color32[] color_b;

    private int player_scores = 0;
    private int index_color = 0;
    private int index_theme_buy_temp = 0;
    private Carrot.Carrot_Box box_shop;
    private Carrot.Carrot_Window_Msg box_msg_shop;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        this.carrot.Load_Carrot(this.check_exit_app);
        this.carrot.ads.set_act_Rewarded_Success(this.act_Rewarded_Success_Shop);
        this.carrot.shop.onCarrotPaySuccess += this.onCarrotPaySuccess_shop;

        this.panel_home.SetActive(true);
        this.panel_play.SetActive(false);
        this.panel_tip.SetActive(false);

        if(this.carrot.get_status_sound()) this.carrot.game.load_bk_music(this.sounds[0]);
        this.ani.play_ani_home();

        this.player_scores = PlayerPrefs.GetInt("player_scores",0);
        this.index_color = PlayerPrefs.GetInt("index_color",0);

        this.update_ui_score_player();
        this.change_color_theme(this.index_color);
    }

    private void check_exit_app()
    {
        if (this.panel_tip.activeInHierarchy)
        {
            this.btn_close_tip();
            this.carrot.set_no_check_exit_app();
        }else if (this.panel_play.activeInHierarchy)
        {
            this.btn_back_home();
            this.carrot.set_no_check_exit_app();
        }
    }

    public void btn_play(int type_mode)
    {
        if (type_mode!=0) this.carrot.ads.Destroy_Banner_Ad();

        this.carrot.play_sound_click();
        this.panel_play.SetActive(true);
        this.panel_home.SetActive(false);
        this.checkers_managaer.load_game_by_model(type_mode);
        this.ani.play_ani_gameplay();
        this.carrot.ads.show_ads_Interstitial();
    }

    public void btn_back_home()
    {
        if (this.checkers_managaer.get_type_mode() != 0) this.carrot.ads.create_banner_ads();
        this.carrot.play_sound_click();
        this.panel_home.SetActive(true);
        this.panel_play.SetActive(false);
        this.ani.play_ani_home();
        this.checkers_managaer.back_home();
        this.carrot.ads.show_ads_Interstitial();
    }

    public void btn_setting()
    {
        this.carrot.ads.show_ads_Interstitial();
        Carrot.Carrot_Box box_setting=this.carrot.Create_Setting();
        box_setting.set_act_before_closing(this.after_close_setting);
    }

    private void after_close_setting()
    {
        if (this.carrot.get_status_sound())
            this.sounds[0].Play();
        else
            this.sounds[0].Stop();
    }

    public void btn_show_tip()
    {
        this.carrot.play_sound_click();
        this.panel_tip.SetActive(true);
    }

    public void btn_close_tip()
    {
        this.carrot.play_sound_click();
        this.panel_tip.SetActive(false);
    }

    public void btn_user()
    {
        this.carrot.user.show_login();
    }

    public void btn_rate()
    {
        this.carrot.show_rate();
    }

    public void btn_share()
    {
        this.carrot.show_share();
    }

    public void btn_ranks()
    {
        this.carrot.game.Show_List_Top_player();
    }

    public void btn_carrot_app()
    {
        this.carrot.show_list_carrot_app();
    }

    public void btn_reset_game()
    {
        this.carrot.play_sound_click();
        this.checkers_managaer.on_reset();
        this.ani.play_ani_gameplay();
    }

    public void play_sound(int index_sound)
    {
        if(this.carrot.get_status_sound()) this.sounds[index_sound].Play();
    }

    public void btn_shop()
    {
        this.load_data_buy_shop_all_item();
        this.carrot.play_sound_click();
        this.box_shop = this.carrot.Create_Box("Shop");
        box_shop.set_icon(this.sp_icon_shop);
        box_shop.set_title("Shop");

        for (int i = 0; i < this.sp_icon_item_shop.Length; i++)
        {
            var index_c = i;
            Carrot.Carrot_Box_Item shop_item_theme = box_shop.create_item("item_shop_"+i);
            shop_item_theme.set_icon_white(this.sp_icon_item_shop[i]);
            shop_item_theme.set_title(this.s_title_item_shop[i]);
            shop_item_theme.set_tip(this.s_tip_item_shop[i]);
            shop_item_theme.set_act(() => this.act_item_shop(index_c));

            if (this.index_color != i&&this.is_buy_item_shop[i])
            {
                Carrot.Carrot_Box_Btn_Item btn_item_theme_ads = shop_item_theme.create_item();
                btn_item_theme_ads.set_color(this.carrot.color_highlight);
                btn_item_theme_ads.set_icon(this.sp_icon_shop_watch_ads);
                btn_item_theme_ads.set_act(() => this.act_watch_ads_shop(index_c));

                Carrot.Carrot_Box_Btn_Item btn_item_theme_buy = shop_item_theme.create_item();
                btn_item_theme_buy.set_color(this.carrot.color_highlight);
                btn_item_theme_buy.set_icon(this.sp_icon_shop_buy);
                btn_item_theme_buy.set_act(() => this.act_buy_item_shop(index_c));
            }
        }
    }

    private void act_watch_ads_shop(int index_theme)
    {
        if (this.box_msg_shop != null) this.box_msg_shop.close();
        this.index_theme_buy_temp = index_theme;
        this.carrot.ads.show_ads_Rewarded();
    }

    private void act_buy_item_shop(int index_theme)
    {
        if (this.box_msg_shop != null) this.box_msg_shop.close();
        this.index_theme_buy_temp = index_theme;
        PlayerPrefs.SetInt("is_buy_item_" + this.index_theme_buy_temp, 1);
        this.carrot.buy_product(2);
    }

    private void act_item_shop(int index_item_shop)
    {
        if (this.is_buy_item_shop[index_item_shop])
        {
            this.box_msg_shop = this.carrot.show_msg("Shop", "You can buy or watch ads to get this item!",Carrot.Msg_Icon.Question);
            this.box_msg_shop.add_btn_msg("Watch ads", () => this.act_watch_ads_shop(index_item_shop));
            this.box_msg_shop.add_btn_msg("Buy", () => this.act_buy_item_shop(index_item_shop));
            this.box_msg_shop.add_btn_msg("Cancel", this.act_cancel_msg_shop);
        }
        else
        {
            this.set_theme_ui_color(index_item_shop);
        }
    }

    private void act_cancel_msg_shop()
    {
        if (this.box_msg_shop != null) this.box_msg_shop.close();
        this.carrot.play_sound_click();
    }

    private void change_color_theme(int index_c)
    {
        this.panel_home.GetComponent<Image>().color = this.color_bk[index_c];
        this.panel_play.GetComponent<Image>().color = this.color_bk[index_c];
        this.checkers_managaer.color_a = this.color_a[index_c];
        this.checkers_managaer.color_b = this.color_b[index_c];
        this.checkers_managaer.load_theme();
        if (this.box_shop != null) this.box_shop.close();
    }

    public void create_effect(int index_effect,Vector3 pos)
    {
        GameObject obj_effect = Instantiate(this.effect_die_prefab[index_effect]);
        obj_effect.transform.SetParent(this.panel_play.transform);
        obj_effect.transform.localScale = new Vector3(1f, 1f, 1f);
        obj_effect.transform.position = pos;
        Destroy(obj_effect, 1.3f);
    }

    public void add_scores_player()
    {
        this.player_scores++;
        PlayerPrefs.SetInt("player_scores", this.player_scores);
        this.carrot.game.update_scores_player(this.player_scores);
        this.update_ui_score_player();
    }

    private void update_ui_score_player()
    {
        this.txt_scores_home.text = this.player_scores.ToString();
    }

    private void act_Rewarded_Success_Shop()
    {
        this.set_theme_ui_color(this.index_theme_buy_temp);
    }

    private void onCarrotPaySuccess_shop(string s_id)
    {
        if (s_id == this.carrot.shop.get_id_by_index(2))
        {
            this.set_theme_ui_color(this.index_theme_buy_temp);
            this.load_data_buy_shop_all_item();
        }
    }

    private void set_theme_ui_color(int index_color)
    {
        if (this.box_shop != null) this.box_shop.close();

        this.index_color = index_color;
        PlayerPrefs.SetInt("index_color", index_color);
        this.change_color_theme(index_color);
    }

    private void load_data_buy_shop_all_item()
    {
        for(int i = 1; i < this.is_buy_item_shop.Length; i++)
        {
            if (PlayerPrefs.GetInt("is_buy_item_" + i, 0) == 1) this.is_buy_item_shop[i] = false;
        }
    }

}
