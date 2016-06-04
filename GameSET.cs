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
            SERVER_NAME,
            ADMIN_NAME,
            TEAMNAME_ALLIES,
            TEAMNAME_AXIS,
            GAMETYPE = "infect",// [ war, dm, sd, sab, dom, koth, ctf, dd, tdef, conf, grnd, tjugg, jugg, gun, infect, oic ]
             OFF_HAND,
             MAP_NAME,
			 WELLCOME_MESSAGE,
             NEXT_MAP;
        bool
            TEST_ = false,

            USE_ADMIN_SAFE_ = false,
            DEPLAY_BOT_,
            SUICIDE_BOT_,
            Disable_Melee_,

            GAME_ENDED_,
            HUMAN_CONNECTED_,
            PREMATCH_DONE,
            OVERFLOW_BOT_;


        float
            INFECTED_TIMELIMIT,
            PLAYERWAIT_TIME,
            MATCHSTART_TIME;

            
        //mus = "mp_time_running_out_losing";


        int
            t0 = 100, t1 = 1000, t2 = 2000, t3 = 3000, t5 = 5000,
            SEARCH_TIME, FIRE_TIME, BOT_DELAY_TIME,
            FAIL_COUNT,
            HUMAN_COUNT;

        Entity ADMIN;

        public static Random rnd = new Random();

        #endregion

        void Client_init_GAME_SET(Entity player)
        {
            human_List.Add(player);

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
            player.SetClientDvar("cl_maxpackets", "60");
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

            OFF_HAND = player.Call<string>("getcurrentoffhand");

            player.Call("notifyonplayercommand", "+TAB", "+scores");
            player.Call("notifyonplayercommand", "-TAB", "-scores");
            player.Call("notifyonplayercommand", "+throw", "+frag");
            player.Call("notifyonplayercommand", "-throw", "-frag");
            player.Call("notifyonplayercommand", "prone", "+prone");
            player.Call("notifyonplayercommand", "stance", "+stance");
            player.Call("notifyonplayercommand", "+aa", "+scores");
            player.Call("notifyonplayercommand", "-aa", "-scores");

            player.OnNotify("prone", ent =>
            {
                if (isSurvivor(ent))
                {
                    switch (rnd.Next(2))
                    {
                        case 0: Silencer(ent); break;
                        case 1: Thermal(ent); break;
                    }
                }
                if (isSurvivor(ent))
                {
                    player.Call("setoffhandprimaryclass", "throwingknife");
                    player.GiveWeapon("throwingknife_mp");
                    player.Call("setweaponammoclip", "throwingknife_mp", 1);
                }

            });
            player.OnNotify("stance", ent =>
            {
                if (isSurvivor(player))
                {
                    switch (rnd.Next(3))
                    {
                        case 0:
                            player.GiveWeapon("c4_mp");
                            player.SwitchToWeapon("c4_mp");
                            player.Call("iPrintlnBold", "^2[ ^7DEPLOY ^2| ^7FIRE ^2] RIGHT  LEFT^7Mouse botton");
                            break;

                        case 1:
                            player.GiveWeapon("frag_grenade_mp");
                            player.SwitchToWeapon("frag_grenade_mp");
                            player.Call("iPrintlnBold", "^2[ ^7THROW ^2] ^2LEFT^7Mouse botton");
                            break;
                        case 2:
                            player.GiveWeapon("semtex_mp");

                            break;

                    }
                    switch (rnd.Next(2))
                    {
                        case 0:
                            player.GiveWeapon("bouncingbetty_mp");
                            player.Call("setweaponammoclip", "bouncingbetty_mp", 1);
                            break;

                        case 1:
                            player.GiveWeapon("claymore_mp");
                            player.Call("setweaponammoclip", "claymore_mp", 1);
                            break;
                    }
                }
            });

            player.OnNotify("fly", (ent) =>
            {
                ent.SetClientDvar("cg_thirdperson", "0");
                ent.SetClientDvar("camera_thirdPerson", "0");

                if (player.GetField<string>("sessionstate") != "spectator")
                {
                    player.Call("allowspectateteam", "freelook", true);
                    player.SetField("sessionstate", "spectator");
                }
                else
                {
                    player.Call("allowspectateteam", "freelook", false);
                    player.SetField("sessionstate", "playing");
                }
            });

            #endregion

            int standard_time = (int)(PLAYERWAIT_TIME + MATCHSTART_TIME) * 1000;
            AfterDelay(standard_time, () =>
            {
                AlliesHud(player);
            });

            AfterDelay(10000, () =>
            {
                if (isSurvivor(player)) player.Notify("textDestroy");
                else player.Notify("open");

            });

            string gun = "";
            int i = rnd.Next(7);
            switch (i)
            {
                case 0: gun = AP();break;
                case 1: gun = AG(); break;
                case 2: gun = AR(); break;
                case 3: gun = SM(); break;
                case 4: gun = LM(); break;
                case 5: gun = SG(); break;
                case 6: gun = SN(); break;
            }
            giveWeaponTo(player, gun);
        }
        void Server_SetDvar()
        {
            Call("setdvar", "motd", WELLCOME_MESSAGE);

            Call("setdvar", "g_TeamName_Allies", TEAMNAME_ALLIES);
            Call("setdvar", "g_TeamName_Axis", TEAMNAME_AXIS);

            Call("setdvar", "scr_game_playerwaittime", PLAYERWAIT_TIME);
            Call("setdvar", "scr_game_matchstarttime", MATCHSTART_TIME);
            Call("setdvar", "scr_player_respawndelay", "2.5f");
            Call("setdvar", "scr_game_allowkillcam", "0");
            Call("setdvar", "scr_infect_timelimit", INFECTED_TIMELIMIT);//Call("setdvar", "scr_player_maxhealth", "");Call("setdvar","scr_player_healthregentime","");
            Call("setdvar", "g_gametype", GAMETYPE);
            if (TEST_)
            {

            }
            readMap();

            if (TEST_)
            {
                Utilities.ExecuteCommand("sv_hostname test");
                Utilities.RawSayAll("^2TEST ^7MODE confirmed");
            }else
            {
                Utilities.ExecuteCommand("sv_hostname " + SERVER_NAME);
            }
        }
        void readMap()
        {
            string ENTIRE_MAPLIST = "mp_aground_ss|mp_alpha|mp_boardwalk|mp_bootleg|mp_bravo|mp_burn_ss|mp_carbon|mp_cement|mp_courtyard_ss|mp_crosswalk_ss|mp_dome|mp_exchange|mp_hardhat|mp_hillside_ss|mp_interchange|mp_italy|mp_lambeth|mp_meteora|mp_moab|mp_mogadishu|mp_morningwood|mp_nola|mp_overwatch|mp_paris|mp_park|mp_plaza2|mp_qadeem|mp_radar|mp_restrepo_ss|mp_roughneck|mp_seatown|mp_shipbreaker|mp_six_ss|mp_terminal_cls|mp_underground|mp_village";

            MAP_NAME = Call<string>("getdvar", "mapname");
            var map_list = ENTIRE_MAPLIST.Split('|').ToList();
            int index = map_list.IndexOf(MAP_NAME);
            if (index == 35) index = 0;

            NEXT_MAP = map_list[index+1];
            Call("setdvar", "sv_nextmap", NEXT_MAP);

            string content = NEXT_MAP +",bot_infected,1";
            File.WriteAllText(@"admin\default.dspl", content);

            print("현재맵 : " + MAP_NAME + " & " + "다음맵 : " + NEXT_MAP);
        }
    }
}
