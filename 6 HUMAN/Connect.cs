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

        void Inf_PlayerConnected(Entity player)
        {

            string name = player.Name;
           
            if (name.StartsWith("bot") )
            {
                int i = 0;
                bool isEndwithNum = int.TryParse(name[name.Length - 1].ToString(), out i);
                if (isEndwithNum)
                {
                    Bot_Connected(player);
                    return;
                }
            }

            if (player.Name == ADMIN_NAME)
            {

                ADMIN = player;
                if (TEST_)
                {
                    player.Call("thermalvisionfofoverlayon");
                    player.Call("setmovespeedscale", 1.5f);
                }
            }

            if (human_List.Count > 7)
            {
                Utilities.ExecuteCommand("dropclient " + player.EntRef + "SORRY. HUMAN SLOTS ARE OVER. SEE YOU NEXT TIME. [10 BOTS & 7 HUMANS]");
                return;
            }

            if (isSurvivor(player))
            {
                if (!HUMAN_CONNECTED_) HUMAN_CONNECTED_ = true;
                print(name + " connected ♥");
                Client_init_GAME_SET(player);
            }
            else
            {
                Utilities.ExecuteCommand("dropclient " + player.EntRef + "Enter the Server Next Round");
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
