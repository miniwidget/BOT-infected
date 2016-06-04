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
        void giveWeaponTo(Entity player, string weapon)
        {
            player.TakeWeapon(player.CurrentWeapon);
            player.GiveWeapon(weapon);
            player.Call("givemaxammo", weapon);
            player.SwitchToWeaponImmediate(weapon);
        }
        public override void OnSay(Entity player, string name, string text)
        {
            if (player.Name == ADMIN_NAME)
            {
                if (!AdminCommand(text)) return;
            }

            if (GAME_ENDED_|| text.Split(' ').Length >1) return;
            text = text.ToLower();

            #region Public Say

            switch (text)
            {
                case "nextmap": AfterDelay(t0, () => Utilities.RawSayTo(player, "^2NEXT MAP : " + NEXT_MAP));break;
                //case "register":
                //    File.AppendAllText("scripts\\welcome\\" + "member" + ".txt", Environment.NewLine + "member:" + player.Name + ",");
                //    AfterDelay(t1, () => Utilities.RawSayTo(player, "^2[ ^7" + player.Name + " ^2] register confirmed"));
                //    break;

                case "infoa": SHOWINFO.ShowInfoA(player); return;
                case "infow": SHOWINFO.ShowInfoW(player); return;

                case "whoami":
                    AfterDelay(t1, () => Utilities.RawSayTo(player, "^2" + player.Name));
                    AfterDelay(t2, () => Utilities.RawSayTo(player, "^2" + player.GUID));
                    AfterDelay(t3, () => Utilities.RawSayTo(player, "^2" + player.IP));
                    break;

                case "sc": AfterDelay(100, () => player.Call("suicide")); return;

                case "riot":
                    player.GiveWeapon("riotshield_mp");
                    player.SwitchToWeaponImmediate("riotshield_mp");
                    return;

                case "javelin": giveWeaponTo(player, "javelin_mp"); return;
                case "stinger":
                    player.SetField("lastDroppableWeapon", player.CurrentWeapon);
                    player.GiveWeapon("stinger_mp");
                    player.SwitchToWeaponImmediate("stinger_mp");
                    AfterDelay(6000, () => player.TakeWeapon("stinger_mp"));
                    AfterDelay(6100, () => player.SwitchToWeaponImmediate(player.GetField<string>("lastDroppableWeapon")));
                    return;
            }
            #endregion


            if (isSurvivor(player))
            {
                #region Allies Say

                switch (text)
                {
                    //case "loc": L.InitialPosition(player, MAP_NAME); break;

                    //case "close": player.Notify("close"); return;
                    //case "open": player.Notify("open"); return;

                    case "ap": giveWeaponTo(player, AP()); return;
                    case "ag": giveWeaponTo(player, AG()); return;

                    case "ar": giveWeaponTo(player, AR()); return;
                    case "sm": giveWeaponTo(player, SM()); return;
                    case "lm": giveWeaponTo(player, LM()); return;
                    case "sn": giveWeaponTo(player, SN()); return;
                    case "sg": giveWeaponTo(player, SG()); return;

                    case "rpg": giveWeaponTo(player, "rpg_mp"); return;
                    case "smaw":giveWeaponTo(player, "iw5_smaw_mp"); return;

                    case "m320":giveWeaponTo(player, "m320_mp"); return;
                    case "xm25":giveWeaponTo(player, "xm25_mp"); return;

                    case "help":Utilities.RawSayTo(player, "Type first ^2infoa  infow"); return;
                }
                #endregion
            }
            else
            {
                if (text == "help")
                {
                    Utilities.RawSayTo(player, "ALL information is http://test.com");
                }
            }
        }

    }
}
