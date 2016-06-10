using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;

namespace Infected
{
    public partial class Infected
    {
        #region field
        string
            SERVER_NAME, ADMIN_NAME,
            TEAMNAME_ALLIES, TEAMNAME_AXIS,
            MAP_NAME, NEXT_MAP,
            WELLCOME_MESSAGE;

        bool
            USE_ADMIN_SAFE_,Disable_Melee_,
            DEPLAY_BOT_, SUICIDE_BOT_, OVERFLOW_BOT_,

           PREMATCH_DONE, GAME_ENDED_, HUMAN_CONNECTED_;

        float
            INFECTED_TIMELIMIT, PLAYERWAIT_TIME, MATCHSTART_TIME;

        int
            t0 = 100, t1 = 1000, t2 = 2000, t3 = 3000, 
            SEARCH_TIME, FIRE_TIME, BOT_DELAY_TIME, BOT_SETTING_NUM, FIRE_DIST,
            PLAYER_LIFE;

        bool isSurvivor(Entity player) { return player.GetField<string>("sessionteam") == "allies"; }
        char[] numChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        Random rnd = new Random();
        Entity ADMIN, First_Infed_Player;
        string[] BOTs_CLASS = { "axis_recipe1", "axis_recipe2", "axis_recipe3", "class0", "class1", "class2", "class4", "class5", "class6", "class6" };
        #endregion

        #region Bots side

        /// <summary>
        /// BOT SET class for custom fields set
        /// </summary>
        class B_SET
        {
            public Entity target { get; set; }
            public int death { get; set; }
            public bool fire { get; set; }
            public bool search { get; set; }
            public bool temp_fire { get; set; }
            public string wep { get; set; }
        }
        List<B_SET> B_FIELD = new List<B_SET>();
        Dictionary<int, int> BOT_ID = new Dictionary<int, int>();
        List<Entity> BOTs_List = new List<Entity>();
        #endregion

        #region Human side
        /// <summary>
        /// HUMAN PLAYER SET class for custom fields set
        /// </summary>
        class H_SET
        {
            int att = 0;
            public int SIRENCERorHB
            {
                get
                {
                    this.att++;
                    if (this.att > 2) this.att = 0;
                    return this.att;
                }
            }
            public int PERK { get; set; }
            public int AX_WEP { get; set; }
            public bool BY_SUICIDE { get; set; }
            public int LIFE { get; set; }
        }
        List<H_SET> H_FIELD = new List<H_SET>();
        Dictionary<int, int> H_ID = new Dictionary<int, int>();
        List<Entity> human_List = new List<Entity>();
        #endregion


        #region server side
        /// <summary>
        /// server side dvar
        /// </summary>
        void Server_SetDvar()
        {
            string ENTIRE_MAPLIST = "mp_aground_ss|mp_alpha|mp_boardwalk|mp_exchange|mp_burn_ss|mp_bootleg|mp_bravo|mp_courtyard_ss|mp_carbon|mp_cement|mp_interchange|mp_hillside_ss|mp_crosswalk_ss|mp_dome|mp_nola|mp_hardhat|mp_italy|mp_moab|mp_restrepo_ss|mp_lambeth|mp_meteora|mp_six_ss|mp_mogadishu|mp_overwatch|mp_aground_ss|mp_paris|mp_plaza2|mp_morningwood|mp_burn_ss|mp_qadeem|mp_radar|mp_courtyard_ss|mp_roughneck|mp_seatown|mp_hillside_ss|mp_shipbreaker|mp_terminal_cls|mp_nola|mp_underground|mp_village|mp_park|";
            MAP_NAME = Call<string>("getdvar", "mapname");
            var map_list = ENTIRE_MAPLIST.Split('|').ToList();
            int index = map_list.IndexOf(MAP_NAME);
            if (index == 35) index = 0;

            int[] smallMap = { 0, 4, 7, 11, 14, 18, 21, 24, 28, 31, 34, 37 };
            int[] largeMap = { 3, 10, 17, 27, 40 };
            if (smallMap.Contains(index))
            {
                PLAYER_LIFE = 2;
                FIRE_DIST = 600;
            }
            else if (largeMap.Contains(index))
            {
                PLAYER_LIFE = 1;
                FIRE_DIST = 750;
            }
            else
            {
                PLAYER_LIFE = 1;
                FIRE_DIST = 680;
            }
            NEXT_MAP = map_list[index + 1];
            Call("setdvar", "sv_nextmap", NEXT_MAP);

            if (TEST_)
            {
                MATCHSTART_TIME = PLAYERWAIT_TIME = 1;

                Utilities.ExecuteCommand("seta g_password \"0\"");
                print("■ ■ TEST MODE ■ ■");
            }
            else
            {

                Utilities.ExecuteCommand("seta g_password \"\"");
                writrMAP();
            }

            Call("setdvar", "scr_game_playerwaittime", PLAYERWAIT_TIME);
            Call("setdvar", "scr_game_matchstarttime", MATCHSTART_TIME);
            Call("setdvar", "scr_game_allowkillcam", "0");
            Call("setdvar", "scr_infect_timelimit", INFECTED_TIMELIMIT);
            //Call("setdvar", "scr_player_respawndelay", "2.5f");
            //Call("setdvar", "scr_player_maxhealth", "");
            //Call("setdvar","scr_player_healthregentime","");
            //Call("setdvar", "g_gametype", GAMETYPE);

            Utilities.ExecuteCommand("sv_hostname " + SERVER_NAME);

        }
        void writrMAP()
        {
            string content = NEXT_MAP + ",bot_infected,1";
            string path = @"admin\default.dspl";

            if (TEST_)
            {
                path = path.Replace("default", "test_defalt");
            }
            File.WriteAllText(path, content);

            print("현재맵 : " + MAP_NAME + " & " + "다음맵 : " + NEXT_MAP);
        }
        void setTeamName()
        {
            Call("setdvar", "g_TeamName_Allies", TEAMNAME_ALLIES);
            Call("setdvar", "g_TeamName_Axis", TEAMNAME_AXIS);
        }
        #endregion

        #region client side

        
        void Client_init_GAME_SET(Entity player)
        {
            int who = human_List.Count;
            human_List.Add(player);

            if (H_ID.ContainsKey(player.EntRef))
            {
                H_SET H = H_FIELD[H_ID[player.EntRef]];
                H.LIFE = PLAYER_LIFE;
                H.PERK = 0;
                H.AX_WEP = 0;
                H.BY_SUICIDE = false;
            }
            else
            {
                H_FIELD.Add(new H_SET() { LIFE = PLAYER_LIFE });
                H_ID.Add(player.EntRef, who);
            }
            #region SetClientDvar

            player.SetClientDvar("lowAmmoWarningNoAmmoColor2", "0 0 0 0");
            player.SetClientDvar("lowAmmoWarningNoAmmoColor1", "0 0 0 0");
            player.SetClientDvar("lowAmmoWarningNoReloadColor2", "0 0 0 0");
            player.SetClientDvar("lowAmmoWarningNoReloadColor1", "0 0 0 0");
            player.SetClientDvar("lowAmmoWarningColor2", "0 0 0 0");
            player.SetClientDvar("lowAmmoWarningColor1", "0 0 0 0");
            player.SetClientDvar("cg_drawBreathHint", "0");
            player.SetClientDvar("cg_fov", "75");
            player.SetClientDvar("cg_scoreboardpingtext", "1");
            player.SetClientDvar("cg_brass", "1");
            player.SetClientDvar("clientsideeffects", "1");
            //player.SetClientDvar("cl_maxpackets", "60");
            player.SetClientDvar("com_maxfps", "91");
            player.SetClientDvar("waypointIconHeight", "13");
            player.SetClientDvar("waypointIconWidth", "13");
            player.SetClientDvar("r_fog", "1");
            player.SetClientDvar("r_distortion", "1");
            player.SetClientDvar("r_dlightlimit", "4");
            player.SetClientDvar("fx_drawclouds", "1");
            player.SetClientDvar("snaps", "20");

            #endregion

            #region notifyonplayercommand

            player.Call("notifyonplayercommand", "+TAB", "+scores");
            player.Call("notifyonplayercommand", "-TAB", "-scores");
            player.Call("notifyonplayercommand", "HOLD_CROUCH", "+movedown");
            player.Call("notifyonplayercommand", "HOLD_PRONE", "+prone");
            player.Call("notifyonplayercommand", "HOLD_STANCE", "+stance");

            player.OnNotify("HOLD_CROUCH", ent =>//view scope
            {
                if (!isSurvivor(player))
                {
                    giveOffhandWeapon(player, "throwingknife");
                    return;
                }

                giveAttachScope(player);

            });

            player.OnNotify("HOLD_PRONE", ent =>//attachment silencer heartbeat,
            {
                if (!isSurvivor(player))
                {
                    giveOffhandWeapon(player, "bouncingbetty_mp");
                    return;
                }
                giveAttachHeartbeat(player,who);
            });

            string offhand = "";
            switch (rnd.Next(5))
            {
                case 0: offhand = "frag_grenade_mp"; break;
                case 1: offhand = "c4_mp"; break;
                case 2: offhand = "semtex_mp"; break;//OK
                case 3: offhand = "bouncingbetty_mp"; break;//OK
                case 4: offhand = "claymore_mp"; break;//OK
            }

            player.OnNotify("HOLD_STANCE", ent =>//offhand weapon
            {
                if (!isSurvivor(player))
                {
                    giveOffhandWeapon(player, "claymore_mp");
                    return;
                }
                giveOffhandWeapon(player, offhand);
            });
            #endregion

            giveOffhandWeapon(player, offhand);
            giveWeaponTo(player, getRandomWeapon());

            AlliesHud(player, offhand.Replace("_mp", "").ToUpper());

            player.Notify("menuresponse", "changeclass", "allies_recipe" + rnd.Next(1, 6));
            player.AfterDelay(500, p => player.SpawnedPlayer += () => human_spawned(player));

        }
        #endregion


    }
}
