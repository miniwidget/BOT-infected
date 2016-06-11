using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;
using System.Diagnostics;
using System.Timers;

namespace Infected
{
    public partial class Infected
    {

        #region 게임 시작 후, 봇 불러오기 

        bool ClearBOTsInPlayers()
        {
            if (Players.Count > 0)//when fast_restart executed, remove pre allocated bots in Infinityscript
            {
                foreach (Entity p in Players)
                {
                    if (p.Name == "") BOTs_List.Add(p);
                }
                foreach (Entity p in BOTs_List)
                {
                    Players.Remove(p);
                }
                BOTs_List.Clear();
            }
            return true;
        }
        void deplayBOTs()
        {

            ClearBOTsInPlayers();

            int fail_count=0 , max = BOT_SETTING_NUM - 1;
     
            OnInterval(100, () =>
            {
                if (BOTs_List.Count > max || OVERFLOW_BOT_) return false;
                if (fail_count > 5)
                {
                    deplayBOTs_map_init(true, "SOMETHING WRONG HAPPEND.RESTART MAP in 5 seconds", 5);
                    return false;
                }

                Entity b = Utilities.AddTestClient();

                if (b == null)
                {
                    fail_count++;
                }

                return true;

            });
        }
        void deplayBOTs_map_init(bool wrong,string message,int sec)
        {
            if(wrong) KickBOTsAll();

            int speed = 25;
            int decayStart = 3000;
            int decayDuration = 1000;

            HudElem END = HudElem.CreateServerFontString("default", 2f);
            END.SetPoint("CENTER", "CENTER", 0, 100);
            END.Foreground = true;
            END.HideWhenInMenu = false;
            END.Alpha = 1f;
            END.Call("setpulsefx", speed, decayStart, decayDuration);
            END.SetText(message);

            HudElem staticBG = HudElem.NewHudElem();
            staticBG.HorzAlign = "fullscreen";
            staticBG.VertAlign = "fullscreen";
            staticBG.SetShader("black", 640, 480);
            staticBG.Foreground = true;
            staticBG.HideWhenInMenu = false;
            staticBG.Alpha = 0;
            staticBG.Call("fadeovertime", 1.5f);
            staticBG.Alpha = 1f;


            AfterDelay(sec*1000, () => Utilities.ExecuteCommand("fast_restart"));
        }
        #endregion

    }
}
