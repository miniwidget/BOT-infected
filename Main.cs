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

        public Infected()
        {
            #region 세팅 불러오기
            string setFile = "admin\\Infected_SET.txt";
            if (File.Exists(setFile))
            {
                using (StreamReader set = new StreamReader(setFile))
                {
                    bool b;
                    int i;
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

                            case "TEST_": if (bool.TryParse(value, out b)) TEST_ = b; break;
                            case "DEPLAY_BOT_": if (bool.TryParse(value, out b)) DEPLAY_BOT_ = b; break;
                            case "USE_ADMIN_SAFE_": if (bool.TryParse(value, out b)) USE_ADMIN_SAFE_ = b; break;
                            case "SUICIDE_BOT_": if (bool.TryParse(value, out b)) SUICIDE_BOT_ = b; break;
                            case "Disable_Melee_": if (bool.TryParse(value, out b)) Disable_Melee_ = b; break;
                        }
                    }

                    if (TEST_) SERVER_NAME = "^2BOT ^7TEST";
                }
            }
            #endregion

            Server_SetDvar();

            char[] numChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            PlayerConnecting += (player) =>
            {
                if (PREMATCH_DONE) return;
                string name = player.Name;
                if (name.StartsWith("bot"))
                {
                    Call("kick", player.EntRef);
                    if (TEST_) print(name + "KICKED★");
                }
            };
            PlayerConnected += (player) =>
            {
                string name = player.Name;

                if (name.StartsWith("bot"))
                {
                    if (!PREMATCH_DONE)
                    {
                        Call("kick", player.EntRef);
                        if (TEST_) print("BOT" + player.EntRef + " kicked before PMCH");
                        return;
                    }
                    Bot_Connected(player);
                }

                else Inf_PlayerConnected(player);
            };

            PlayerDisconnected += Inf_PlayerDisConnected;

            OnNotify("prematch_done", () =>
            {
                PREMATCH_DONE = true;
                Server_Hud();
                if (DEPLAY_BOT_) deplayBOTs();
            });

            OnNotify("game_ended", (level) =>
            {
                GAME_ENDED_ = true;
                AfterDelay(20000, () =>
                {
                    Utilities.ExecuteCommand("map_rotate");
                });
            });
        }

        //public override void OnExitLevel()
        //{
            //Utilities.ExecuteCommand("map " + NEXT_MAP);
        //}

    }
}

