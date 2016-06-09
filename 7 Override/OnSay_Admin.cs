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
        #region related with BOT
        void KickBOTsAll()
        {
            for (int i = 17; i > 0; i--)
            {
                Entity ent = Entity.GetEntity(i);
                
                if (ent == null) continue;
                if (ent.Call<string>("getguid").Contains("bot"))
                {
                    Call("kick", i);
                }
            }
        }

        void DeployBOTsByNUM(int num)
        {
            for (int i = 0; i < num; i++)
            {
                addBot();
            };

            Utilities.RawSayAll(num + " ^2BOTs ^7deployed");
        }

        void addBot()
        {
            var bot = Utilities.AddTestClient();
            if (bot == null) return;
            bot.TakeAllWeapons();
            var weapon = SN();
            bot.GiveWeapon(weapon);
            bot.SwitchToWeapon(weapon);
            bot.SwitchToWeaponImmediate(weapon);
        }
        void moveBot(string name)
        {
            if (name == null) name = "bot";
            foreach (var bot in BOTs_List)
            {
                if (bot.Name.Contains(name))
                {
                    bot.Call("setorigin", ADMIN.Origin);
                    break;
                }
            }
        }

        /* void giveWeaponToBot()
        {
            Entity bot = BOTs_List[rnd.Next(BOTs_List.Count)];
            bot.TakeAllWeapons();
            var weapon = SN();
            bot.GiveWeapon(weapon);
            bot.SwitchToWeapon(weapon);
            //bot.SwitchToWeaponImmediate(weapon);
            bot.Call("setorigin", ADMIN.Origin);
        }
        */

        /* void hideBot(string name)
        {
            if (name == null) name = "bot";
            foreach (var bot in BOTs_List)
            {
                if (bot.Name.Contains(name))
                {
                    bot.Call("setorigin", ADMIN.Origin);
                    AfterDelay(t2, () =>
                    {
                        bot.Call("hide");
                        AfterDelay(t2, () =>
                        {
                            bot.Call("show");
                        });
                    });
                    break;
                }
            }
        }
        */

        #endregion

        #region related with KILL or KICK
        void Die(string message)
        {
            Char[] delimit = { ' ' };
            String[] split = message.Split(delimit);
            if (split.Length == 1)
            {
                sayToAdmin("die [player's name]");
            }

            else if (split.Length > 1)
            {
                for (int i = 0; i < 18; i++)
                {
                    Entity player = Call<Entity>("getEntByNum", i);
                    if (player != null && player.IsPlayer)
                    {
                        if (player.Name.Contains(message.Split(' ')[1]))
                        {
                            AfterDelay(100, () => player.Call("suicide"));
                        }

                    }

                }
            }
        }

        void Magic(string message)
        {
            Char[] delimit = { ' ' }; String[] split = message.Split(delimit);

            if (split.Length == 1) sayToAdmin("magic [player's name]");

            else if (split.Length > 1)
            {
                for (int i = 0; i < 18; i++)
                {
                    Entity player = Call<Entity>("getEntByNum", i);
                    if (player != null && player.IsPlayer)
                    {
                        var targetPos = player.Origin;
                        var startPos = ADMIN.Origin;

                        startPos.Z = startPos.Z + 1000;
                        if (player.Name.Contains(message.Split(' ')[1]))
                        {
                            Entity rocket = Call<Entity>("magicbullet", "uav_strike_projectile_mp", startPos, targetPos, ADMIN);
                        }

                    }
                }
            }
        }

        void Kick(string message)
        {
            Char[] delimit = { ' ' };
            String[] split = message.Split(delimit);
            if (split.Length == 1)
            {
                sayToAdmin("warn [player's name]");
            }

            else if (split.Length > 1)
            {
                for (int i = 0; i < 17; i++)
                {
                    Entity player = Call<Entity>("getEntByNum", i);
                    if (player != null && player.IsPlayer)
                    {
                        if (player.Name.Contains(message.Split(' ')[1]))
                        {
                            AfterDelay(100, () => Utilities.RawSayAll("Kick ^2" + player.Name + " ^7executed"));
                            AfterDelay(100, () => Utilities.ExecuteCommand("dropclient " + player.EntRef));
                        }

                    }

                }
            }
        }

        #endregion

        #region BUFF
        bool specMode;
        void changeSpecMode()
        {
            specMode = !specMode;
            if (specMode)
            {
                ADMIN.Call("allowspectateteam", "freelook", true);
                ADMIN.SetField("sessionstate", "spectator");
            }
            else
            {
                ADMIN.Call("allowspectateteam", "freelook", false);
                ADMIN.SetField("sessionstate", "playing");
            }

        }
        #endregion

        #region 관리자 커맨드 메서드

        void script(string name, bool load)
        {
            if (load)
            {
                Utilities.ExecuteCommand("loadScript " + name + ".dll");
            }
            else
            {
                Utilities.ExecuteCommand("unloadScript " + name + ".dll");
            }
        }
        void print(object s)
        {
            Log.Write(LogLevel.None, "{0}", s.ToString());
        }
        void sayToAdmin(string m)
        {
            AfterDelay(t0, () => Utilities.RawSayTo(ADMIN, m));
        }
        void executeAfter(int delay, string command)
        {
            AfterDelay(delay, () => Utilities.ExecuteCommand(command));
        }
        void executeAfter(int delay, string command, string sayAll)
        {
            Utilities.RawSayAll(sayAll);
            AfterDelay(delay, () => Utilities.ExecuteCommand(command));
        }
        void executeAfter(int delay, string command, Action act)
        {
            act.Invoke();
            AfterDelay(delay, () => Utilities.ExecuteCommand(command));
        }
        
        #endregion

        bool AdminCommand(string text)
        {

            switch (text)
            {
                case "pos": moveBot(null); return false;
                case "ab": addBot(); return false;
                case "kb": Utilities.RawSayAll("^2Kickbots ^7executed"); KickBOTsAll(); return false;

                case "s": changeSpecMode(); return false;
                case "1": ADMIN.Call("thermalvisionfofoverlayon"); return false;
                case "2": ADMIN.Call("thermalvisionfofoverlayoff"); return false;
                case "safe": USE_ADMIN_SAFE_ = !USE_ADMIN_SAFE_; sayToAdmin("ADMIN SAFE : " + USE_ADMIN_SAFE_); return false;

                case "fr": KickBOTsAll(); executeAfter(t2, "fast_restart", "^2RESTART MAP ^7executed"); return false;
                case "mr": 
                case "nm": KickBOTsAll(); executeAfter(t2, "map " + NEXT_MAP, "^2NEXT MAP ^7executed"); return false;
                case "restart": KickBOTsAll(); executeAfter(t2, "map_restart", "^2RESTART MAP ^7executed"); return false;

                case "weapon": ADMIN.Call("iprintln", ADMIN.CurrentWeapon); return false;
                case "wm": writrMAP();return false;
                case "lts": executeAfter(t1, "loadscript test\\TEST.dll", "^2Load Test Script"); executeAfter(t1, "fast_restart", "^2BEGIN ^7RESTART"); return false;
                case "ults": executeAfter(t1, "unloadscript test\\TEST.dll", "^1Un Load Test Script"); executeAfter(t1, "fast_restart", "^2BEGIN ^7RESTART");return false;
            }

            var t = text.Split(' ');
            if (t.Length > 1)
            {
                var txt = t[0];
                var value = t[1];
                switch (txt)
                {
                    case "pos": moveBot(value); break;

                    case "bot": DeployBOTsByNUM(int.Parse(value)); return false;
                    case "die": Die(text); return false;
                    case "k": Kick(text); return false;
                    case "magic": Magic(text); return false;
                    case "so": ADMIN.Call("playlocalsound", value); return false;
                    case "map": executeAfter(t2, "map " + value, "^2Map changed^7 to " + value + " executed"); return false;

                    case "l":
                        script(value, true);
                        executeAfter(t2, "fast_restart", "Loadscript ^2" + value + " ^7executed");
                        return false;

                    case "ul":
                        script(value, false);
                        executeAfter(t2, "fast_restart", "UnLoadscript ^2" + value + " ^7executed");
                        return false;
                }
            }

            return true;
        }

    }
}
