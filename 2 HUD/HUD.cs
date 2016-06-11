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

        void Server_Hud()
        {
            //HudElem staticBG = HudElem.NewHudElem();
            //staticBG.HorzAlign = "fullscreen";
            //staticBG.VertAlign = "fullscreen";
            //staticBG.SetShader("black", 640, 480);
            //staticBG.Foreground = true;
            //staticBG.HideWhenInMenu = false;
            //staticBG.Alpha = 0.7f;
            //staticBG.Call("fadeovertime", 5f);
            //staticBG.Alpha = 0f;

            //HudElem START = HudElem.CreateServerFontString("objective", 1.8f);
            //START.SetPoint("CENTER", "CENTER", 0, 100);
            //START.Foreground = true;
            //START.HideWhenInMenu = false;
            //START.Alpha = 0.8f;
            //START.SetField("glowcolor", new Vector3(0.8f, 1f, 0.8f));
            //START.GlowAlpha = 0.2f;
            //START.Call("setpulsefx", 100, 4000, 2000);
            //START.SetText(SERVER_NAME);
            //AfterDelay(t3 * 2, () => { staticBG.Call("destroy"); START.Call("destroy"); });

            HudElem INFO1 = HudElem.CreateServerFontString("hudbig", 0.8f);
            INFO1.X = 240;
            INFO1.Y = 3;
            //INFO1.Y = -10;
            INFO1.Alpha = 0.7f;
            INFO1.HideWhenInMenu = true;
            INFO1.SetText(SERVER_NAME);

            //AfterDelay(t2, () =>
            //{
            //    INFO1.Call("moveovertime", 2);
            //    INFO1.Y = 3;
            //    INFO1.Alpha = 0.7f;
            //});
        }

        void AlliesHud(Entity player,string offhand)
        {
            HudElem allies_info_hud = HudElem.CreateFontString(player, "hudbig", 0.4f);
            allies_info_hud.X = 740;
            allies_info_hud.Y = -10;
            allies_info_hud.AlignX = "right";
            allies_info_hud.HideWhenInMenu = true;
            allies_info_hud.Alpha = 0f;
            allies_info_hud.SetText("ATTACHMENT ^2INFOA\n^7WEAPONINFO ^2INFOW");

            HudElem allies_weap_hud = HudElem.CreateFontString(player, "hudbig", 0.5f);
            allies_weap_hud.X = 700;
            allies_weap_hud.Y = 150;
            allies_weap_hud.AlignX = "right";
            allies_weap_hud.HideWhenInMenu = true;
            allies_weap_hud.Foreground = false;
            allies_weap_hud.Alpha = 0f;
            allies_weap_hud.SetText("^7type following\n^2ap ^74 akimbo pistol\n^2ag ^76 akimbo gun\n^2ar ^79 assau riffle\n^2sm ^76 sub m_gun\n^2lm ^75 light m_gun\n^2sg ^74 shot gun\n^2sn ^76 snipe gun\n\n^7bind key at option\n^2[{+movedown}] ^7viewscope\n^2[{+prone}] ^7attatchment\n^2[{+stance}] ^7" + offhand);

            allies_info_hud.Alpha = 0.8f; allies_weap_hud.Alpha = 0.6f;
            allies_weap_hud.Call("moveovertime", 2f);
            allies_weap_hud.X = 740;

            allies_info_hud.Call("moveovertime", 2f);
            allies_info_hud.Y = 5;

            player.OnNotify("CLOSE", e =>
            {
                allies_info_hud.Call("destroy"); allies_weap_hud.Call("destroy");
            });
        }

        void AxisHud(Entity player)
        {
            player.Notify("CLOSE");
            //player.Notify("CLOSE_perk");
            //human_List.Remove(player);
            

            HudElem axis_weap_hud = HudElem.CreateFontString(player, "hudbig", 0.5f);
            axis_weap_hud.X = 740;
            axis_weap_hud.Y = 150;
            axis_weap_hud.AlignX = "right";
            axis_weap_hud.HideWhenInMenu = true;
            axis_weap_hud.Foreground = false;
            axis_weap_hud.Alpha = 0f;
            axis_weap_hud.SetText("^7type following\n\n^2infow ^7weapon info\n^2sc ^7 suicide\n^2riot ^7 riotshield\n^2stinger ^7stinger\n\n^7bind key at option\n\n^2[{+movedown}] ^7 throwingknife\n^2[{+prone}] ^7 bouncingbetty\n^2[{+stance}] ^7 claymore");

            player.OnNotify("open_", entity => axis_weap_hud.Alpha = 0.6f);
            player.OnNotify("close_", entity => axis_weap_hud.Alpha = 0f);
            player.OnNotify("CLOSE_", entity => axis_weap_hud.Call("destroy"));

            AfterDelay(t3, () => player.Notify("open_"));
        }
        
    }
}
