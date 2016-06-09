using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;
using System.Timers;

namespace Infected
{
    public partial class Infected : BaseScript
    {
        bool TEST_ = true;

        public Infected()
        {

            #region 세팅 불러오기

            string setFile = null;
            if (TEST_)
            {
                setFile = "admin\\test\\test_Infected_SET.txt";

            }
            else
            {
                setFile = "admin\\Infected_SET.txt";
            }

            int i;

            if (File.Exists(setFile))
            {
                using (StreamReader set = new StreamReader(setFile))
                {
                    bool b;
                    float f;

                    while (!set.EndOfStream)
                    {
                        string line = set.ReadLine();
                        if (line.StartsWith("//") || line.Equals(string.Empty)) continue;

                        string[] split = line.Split('=');
                        if (split.Length < 1) continue;

                        string name = split[0];
                        string value = split[1];
                        var comment = value.IndexOf("//");
                        if (comment != -1) value = value.Substring(0, comment);

                        switch (name)
                        {
                            case "SERVER_NAME": SERVER_NAME = value; break;
                            case "ADMIN_NAME": ADMIN_NAME = value; break;
                            case "TEAMNAME_ALLIES": TEAMNAME_ALLIES = value; break;
                            case "TEAMNAME_AXIS": TEAMNAME_AXIS = value; break;
                            case "WELLCOME_MESSAGE": WELLCOME_MESSAGE = value; break;

                            case "INFECTED_TIMELIMIT": if (float.TryParse(value, out f)) INFECTED_TIMELIMIT = f; break;
                            case "PLAYERWAIT_TIME": if (float.TryParse(value, out f)) PLAYERWAIT_TIME = f; break;
                            case "MATCHSTART_TIME": if (float.TryParse(value, out f)) MATCHSTART_TIME = f; break;
                            case "SEARCH_TIME": if (int.TryParse(value, out i)) SEARCH_TIME = i; break;
                            case "FIRE_TIME": if (int.TryParse(value, out i)) FIRE_TIME = i; break;
                            case "BOT_DELAY_TIME": if (int.TryParse(value, out i)) BOT_DELAY_TIME = i; break;
                            case "BOT_SETTING_NUM": if (int.TryParse(value, out i)) BOT_SETTING_NUM = i; break;

                            case "TEST_": if (!TEST_ && bool.TryParse(value, out b)) TEST_ = b; break;
                            case "DEPLAY_BOT_": if (bool.TryParse(value, out b)) DEPLAY_BOT_ = b; break;
                            case "USE_ADMIN_SAFE_": if (bool.TryParse(value, out b)) USE_ADMIN_SAFE_ = b; break;
                            case "SUICIDE_BOT_": if (bool.TryParse(value, out b)) SUICIDE_BOT_ = b; break;
                            case "Disable_Melee_": if (bool.TryParse(value, out b)) Disable_Melee_ = b; break;
                        }
                    }

                    if (TEST_) SERVER_NAME = "^2BOT ^7TEST";
                }
            }

            Server_SetDvar();

            #endregion

            PlayerConnecting += player =>
            {
                if (PREMATCH_DONE) return;//when game map_rotate, remove all bot
                if (player.Name.StartsWith("bot"))
                {
                    Call("kick", player.EntRef);
                }
            };

            PlayerConnected += player =>
            {
                if (player.Name.StartsWith("bot"))
                {
                    if (!PREMATCH_DONE)//when fast_restart executed, remove bot
                    {
                        Call("kick", player.EntRef);
                        return;
                    }
                    Bot_Connected(player);
                }
                else
                {
                    Human_Connected(player);
                }
            };

            OnNotify("prematch_done", () =>
            {
                PREMATCH_DONE = true;
                if (DEPLAY_BOT_) deplayBOTs();

                PlayerDisconnected += Inf_PlayerDisConnected;

                OnNotify("game_ended", (level) =>
                {
                    GAME_ENDED_ = true;
                    AfterDelay(20000, () => Utilities.ExecuteCommand("map_rotate"));
                });
            });
        }
    }
}

