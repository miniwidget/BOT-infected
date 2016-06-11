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

        void Human_Connected(Entity player)
        {

            string name = player.Name;

            if (player.Name == ADMIN_NAME)
            {

                ADMIN = player;
                if (TEST_)
                {
                    player.Call("thermalvisionfofoverlayon");
                    player.Call("setmovespeedscale", 1.5f);
                }
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
                int who = player.EntRef;
                H_SET H = H_FIELD[who];
                H.LIFE = -2;
                H.AX_WEP = 1;

                player.SetField("sessionteam", "axis");
                human_List.Remove(player);
                player.AfterDelay(100, p =>
                {
                    player.Call("suicide");
                    player.Notify("menuresponse", "changeclass", "axis_recipe4");
                    print(player.Name + " : Infected ⊙..⊙");
                });
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
