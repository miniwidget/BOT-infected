using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;
using System.Timers;

namespace Infected
{
    public partial class Infected
    {
        void ADMIN_BUFF()
        {
            ADMIN.Call("notifyonplayercommand", "SPECT", "+strafe");
            bool spect = false;
            ADMIN.OnNotify("SPECT", a =>
            {
                if (!spect)
                {
                    ADMIN.Call("allowspectateteam", "freelook", true);
                    ADMIN.SetField("sessionstate", "spectator");
                }
                else
                {
                    ADMIN.Call("allowspectateteam", "freelook", false);
                    ADMIN.SetField("sessionstate", "playing");
                }
                spect = !spect;
            });
            if (TEST_)
            {
                ADMIN.Call("thermalvisionfofoverlayon");
                ADMIN.Call("setmovespeedscale", 1.5f);
            }

        }
        void Human_Connected(Entity player)
        {

            string name = player.Name;

            if (player.Name == ADMIN_NAME)
            {
                ADMIN = player;
                ADMIN_BUFF();
            }

            var max = 18 - ( BOT_SETTING_NUM +1);
            if (human_List.Count > max)
            {
                Utilities.ExecuteCommand("dropclient " + player.EntRef + "\"SORRY. HUMAN SLOTS ARE OVER. SEE YOU NEXT TIME. [10 BOTS & 7 HUMANS]\"");
                return;
            }

            if (isSurvivor(player))
            {
                if (!HUMAN_CONNECTED_) HUMAN_CONNECTED_ = true;
                print(name + " connected ♥" );
                Client_init_GAME_SET(player);
            }
            else
            {
                //Utilities.ExecuteCommand("dropclient " + player.EntRef + " \"Join Next Round please\"");
                H_SET H = H_FIELD[player.EntRef];
                H.LIFE = -1;
            }

        }
        void Inf_PlayerDisConnected(Entity player)
        {
            if (human_List.Contains(player))// 봇 타겟리스트에서 접속 끊은 사람 제거
            {
                human_List.Remove(player);
                
            }
            if (human_List.Count == 0 && !GAME_ENDED_) HUMAN_CONNECTED_ = false;
        }

    }
}
