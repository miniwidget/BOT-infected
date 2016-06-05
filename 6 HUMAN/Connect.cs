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
            int i = 0;

            bool isEndwithNum = int.TryParse(name[name.Length - 1].ToString(), out i);

            if (name.StartsWith("bot") && isEndwithNum)
            {
                Bot_Connected(player);
                return;
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

            if (HUMAN_COUNT > 8)
            {
                Utilities.ExecuteCommand("dropclient " + player.EntRef + "SORRY. HUMAN SLOTS ARE OVER. SEE YOU NEXT TIME. [10 BOTS & 8 HUMANS]");
                return;
            }
            HUMAN_COUNT++;
            if (!HUMAN_CONNECTED_) HUMAN_CONNECTED_ = true;

            print(name + " connected ♥");

            //IMPORTANT
            foreach (string f in new string[] { "PERK", "AX_WEP", "byAttack", "FAIL_COUNT"})
            {
                player.SetField(f, 0);
            }

            if (isSurvivor(player))
            {
                Client_init_GAME_SET(player);
            }
            else
            {
                player.Call("suicide");
            }

            if (!PREMATCH_DONE)
            {
                string cls = "allies_recipe" + rnd.Next(1, 6);
                player.Call("suicide");
                player.Notify("menuresponse", "changeclass", cls);
                player.AfterDelay(500, p => player.SpawnedPlayer += () => human_spawned(player));
            }
            else player.SpawnedPlayer += () => human_spawned(player);
        }
        void Inf_PlayerDisConnected(Entity player)
        {
            // 봇 타겟리스트에서 접속 끊은 사람 제거
            if (human_List.Contains(player))
            {
                human_List.Remove(player);
                HUMAN_COUNT--;
            }
            if (HUMAN_COUNT == 0 && !GAME_ENDED_) HUMAN_CONNECTED_ = false;
        }

    }
}
