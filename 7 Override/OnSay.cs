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
        void selectWeaponTo(Entity player, string type, int i)
        {

        }
        public override void OnSay(Entity player, string name, string text)
        {
            if (player.Name == ADMIN_NAME)
            {
                if (!AdminCommand(text)) return;
            }
            if (GAME_ENDED_) return;


            var texts = text.ToLower().Split(' ');
            var length = texts.Length;
            bool survivor = isSurvivor(player);

            if (length == 1)
            {
                #region Public Say
                switch (texts[0])
                {
                    case "nextmap": AfterDelay(t0, () => Utilities.RawSayTo(player, "^2NEXT MAP : " + NEXT_MAP)); break;
                    //case "register":
                    //    File.AppendAllText("scripts\\welcome\\" + "member" + ".txt", Environment.NewLine + "member:" + player.Name + ",");
                    //    AfterDelay(t1, () => Utilities.RawSayTo(player, "^2[ ^7" + player.Name + " ^2] register confirmed"));
                    //    break;

                    case "infoa": ShowInfoA(player); return;
                    case "infow": ShowInfoW(player); return;

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
                    case "help": Utilities.RawSayTo(player, "information is in ^7[^2github.com/miniwidget/BOT-infected^7]"); return;
                }
                #endregion

                if (!survivor) return;

                #region Allies Say

                switch (texts[0])
                {
                    case "ap": giveWeaponTo(player, AP()); return;
                    case "ag": giveWeaponTo(player, AG()); return;

                    case "ar": giveWeaponTo(player, AR()); return;
                    case "sm": giveWeaponTo(player, SM()); return;
                    case "lm": giveWeaponTo(player, LM()); return;
                    case "sn": giveWeaponTo(player, SN()); return;
                    case "sg": giveWeaponTo(player, SG()); return;

                    case "rpg": giveWeaponTo(player, "rpg_mp"); return;
                    case "smaw": giveWeaponTo(player, "iw5_smaw_mp"); return;

                    case "m320": giveWeaponTo(player, "m320_mp"); return;
                    case "xm25": giveWeaponTo(player, "xm25_mp"); return;

                }
                #endregion

            }else if (length == 2)
            {
                if (!survivor) return;
                var tx = texts[1];if (tx.Length != 1) return;
                var value = tx[0];if (!numChar.Contains(value)) return;
                int i = int.Parse(value.ToString());

                switch (texts[0])
                {
                    case "ap": giveWeaponTo(player, AP(i)); return;
                    case "ag": giveWeaponTo(player, AG(i)); return;

                    case "ar": giveWeaponTo(player, AR(i)); return;
                    case "sm": giveWeaponTo(player, SM(i)); return;
                    case "lm": giveWeaponTo(player, LM(i)); return;
                    case "sn": giveWeaponTo(player, SN(i)); return;
                    case "sg": giveWeaponTo(player, SG(i)); return;
                }

            }

        }

    }
}
