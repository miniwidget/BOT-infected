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
        private List<Entity> human_List = new List<Entity>();
        List<Entity> BOTs_List = new List<Entity>();
        private bool[] bot_search = new bool[18];
        private bool[] bot_fire = new bool[18];
        private bool isSurvivor(Entity player) { return player.GetField<string>("sessionteam") == "allies"; }//sessionteam
        Entity first_Inf_BOT;

        #region 게임 시작 후, 봇 불러오기 //실패 시, 맵 다시 시작
        void deplayBOTs()
        {
            if (BOTs_List.Count >= 10) return;

            OnInterval(250, () =>
            {
                if (OVERFLOW_BOT_) return false;
                if (BOTs_List.Count > 10) return false;
                if (FAIL_COUNT > 5)
                {
                    deplayBOTs_map_init(true, "SOMETHING WRONG HAPPEND.RESTART MAP in 5 seconds",5);
                    return false;
                }
                Entity b = Utilities.AddTestClient();

                if (b == null)
                {
                    FAIL_COUNT++;
                    return true;
                }
                return true;

            });
        }
        void deplayBOTs_map_init(bool wrong,string message,int sec)//
        {
            if(wrong) KickBOTsAll();
            // Call("iPrintlnBold", "");
            HudElem staticBG = HudElem.NewHudElem();
            staticBG.HorzAlign = "fullscreen";
            staticBG.VertAlign = "fullscreen";
            staticBG.SetShader("black", 640, 480);
            staticBG.Foreground = true;
            staticBG.HideWhenInMenu = false;
            staticBG.Alpha = 0;
            staticBG.Call("fadeovertime", 5f);
            staticBG.Alpha = 1f;

            int transitiontime = 100;
            int duration = 5000;
            int decayduration = 2000;

            HudElem END = HudElem.CreateServerFontString("default", 2f);
            END.SetPoint("CENTER", "CENTER", 0, 100);
            END.Foreground = true;
            END.HideWhenInMenu = false;
            END.Alpha = 1f;
            END.Call("setpulsefx", transitiontime, duration, decayduration);
            END.SetText(message);

            AfterDelay(sec*1000, () => Utilities.ExecuteCommand("fast_restart"));
        }
        #endregion

    }
}
