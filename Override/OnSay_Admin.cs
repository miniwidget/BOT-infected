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
        #region 관리자 커맨드 메서드
        Entity getBot(int num)
        {
            int j = 0;
            for (int i = 0; i < 17; i++)
            {
                Entity player = Call<Entity>("getEntByNum", i);
                if (player != null && player.IsPlayer)
                {

                    if (player.Name.StartsWith("bot"))
                    {
                        j++;
                        if (j == 1)
                        {
                            return player;
                        }
                    }
                }

            }
            return ADMIN;
        }
        void KickBOTsAll()
        {
            for (int i = 17; i > 0; i--)
            {
                Entity ent = Entity.GetEntity(i);
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
        void giveWeaponToBot()
        {
            Entity bot = BOTs_List[rnd.Next(BOTs_List.Count)];
            bot.TakeAllWeapons();
            var weapon = SN();
            bot.GiveWeapon(weapon);
            bot.SwitchToWeapon(weapon);
            //bot.SwitchToWeaponImmediate(weapon);
            bot.Call("setorigin", ADMIN.Origin);
        }
        void Die(string message)
        {
            Char[] delimit = { ' ' };
            String[] split = message.Split(delimit);
            if (split.Length == 1)
            {
                printToAdmin("die [player's name]");
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

            if (split.Length == 1) printToAdmin("magic [player's name]");

            else if (split.Length > 1)
            {
                //Admin.Call("allowspectateteam", "freelook", true);
                //Admin.SetField("sessionstate", "spectator");
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

                //AfterDelay(100, () =>
                //{
                //    Admin.Call("allowspectateteam", "freelook", false);
                //    Admin.SetField("sessionstate", "playing");
                //});
            }
        }

        void Kick(string message)
        {
            Char[] delimit = { ' ' };
            String[] split = message.Split(delimit);
            if (split.Length == 1)
            {
                printToAdmin("warn [player's name]");
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

        void PingKick(Entity player)
        {
            if (player.Ping >= 450)
            {
                player.Call("iPrintlnBold", "your ping is too high");

                AfterDelay(t5, () => Utilities.ExecuteCommand("dropclient " + player.EntRef));
            }
        }

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
        void printToAdmin(string m)
        {
            AfterDelay(t0, () => Utilities.RawSayTo(ADMIN, m));
        }
        void executeAfter(int delay, string command)
        {
            AfterDelay(delay, () => Utilities.ExecuteCommand(command));
        }
        void executeAfter(int delay, string command, string publicPrint)
        {
            Utilities.RawSayAll(publicPrint);
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
                case "pos": moveBot(null); break;
                case "ab": addBot();break;
                //case "har": spawnHarrir();break;
                //case "h": rideHeli(); break;
                case "s": changeSpecMode(); return false;
                case "safe":USE_ADMIN_SAFE_ = !USE_ADMIN_SAFE_;printToAdmin("ADMIN SAFE : " + USE_ADMIN_SAFE_); return false;
                //case "knife": DISABLE_MELEE_OF_INFECTED_ = !DISABLE_MELEE_OF_INFECTED_; Utilities.RawSayAll("^2Melee " + DISABLE_MELEE_OF_INFECTED_ + " ^7executed"); return false;

                //case "son": ADMIN.Call("setmovespeedscale", 1.5f); printToAdmin("^2Speed ^7x 1.5"); return false;
                //case "soff": ADMIN.Call("setmovespeedscale", 1f); printToAdmin("^2Speed ^7x 1.0"); return false;

                case "kb": Utilities.RawSayAll("^2Kickbots ^7executed"); KickBOTsAll(); return false;

                case "1": ADMIN.Call("thermalvisionfofoverlayon"); return false;
                case "2": ADMIN.Call("thermalvisionfofoverlayoff"); return false;

                case "restart": KickBOTsAll(); executeAfter(t2, "map_restart", "^2RESTART MAP ^7executed"); return false;
                case "fr": KickBOTsAll(); executeAfter(t2, "fast_restart", "^2RESTART MAP ^7executed"); return false;

                case "mr": KickBOTsAll(); executeAfter(t2, "map_rotate", "^2NEXT MAP ^7executed"); return false;
                case "nm": KickBOTsAll(); executeAfter(t2, "map " + NEXT_MAP, "^2NEXT MAP ^7executed"); return false;

                case "weapon": ADMIN.Call("iprintln", ADMIN.CurrentWeapon); return false;
                case "list": printToAdmin(ADMIN.Call<string>("getGuid") + "/" + ADMIN.GetField<string>("name")); return false;

            }
            var t = text.Split(' ');
            if (t.Length > 1)
            {
                var txt = t[0];
                switch (txt)
                {
                    case "pos": moveBot(t[1]); break;

                    case "bot": DeployBOTsByNUM(int.Parse(t[1])); return false;
                    case "die": Die(text); return false;
                    case "k": Kick(text); return false;
                    case "magic": Magic(text); return false;
                    case "so": ADMIN.Call("playlocalsound", t[1]); return false;
                    case "map": executeAfter(t2, "map " + t[1], "^2Map changed^7 to " + t[1] + " executed"); return false;

                    case "l":
                        script(t[1], true);
                        executeAfter(t2, "fast_restart", "Loadscript ^2" + t[1] + " ^7executed");
                        return false;

                    case "ul":
                        script(t[1], false);
                        Utilities.ExecuteCommand("unloadscript " + t[1] + ".dll");
                        executeAfter(t2, "fast_restart", "UnLoadscript ^2" + t[1] + " ^7executed");
                        return false;
                }
            }

            return true;
        }


    }
}
