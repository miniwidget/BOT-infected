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
                    bool value;
                    int i;
                    float f;

                    while (!set.EndOfStream)
                    {
                        string line = set.ReadLine();
                        if (line.StartsWith("//") || line.Equals(string.Empty)) continue;

                        string[] split = line.Split('=');
                        if (split.Length < 1) continue;

                        string type = split[0];
                        switch (type)
                        {
                            case "SERVER_NAME": SERVER_NAME = split[1]; break;
                            case "ADMIN_NAME": ADMIN_NAME = split[1]; break;
                            case "TEAMNAME_ALLIES": TEAMNAME_ALLIES = split[1]; break;
                            case "TEAMNAME_AXIS": TEAMNAME_AXIS = split[1]; break;
							case "WELLCOME_MESSAGE": WELLCOME_MESSAGE = split[1];break;

                            case "INFECTED_TIMELIMIT": if (float.TryParse(split[1], out f)) INFECTED_TIMELIMIT = f; break;
                            case "PLAYERWAIT_TIME": if (float.TryParse(split[1], out f)) PLAYERWAIT_TIME = f; break;
                            case "MATCHSTART_TIME": if (float.TryParse(split[1], out f)) MATCHSTART_TIME = f; break;
                            case "SEARCH_TIME": if (int.TryParse(split[1], out i)) SEARCH_TIME = i; break;
                            case "FIRE_TIME": if (int.TryParse(split[1], out i)) FIRE_TIME = i; break;
                            case "BOT_DELAY_TIME": if (int.TryParse(split[1], out i)) BOT_DELAY_TIME = i; break;

                            case "TEST_": if (bool.TryParse(split[1], out value)) TEST = value; break;
                            case "DEPLAY_BOT_": if (bool.TryParse(split[1], out value)) DEPLAY_BOT_ = value; break;
                            case "USE_ADMIN_SAFE_": if (bool.TryParse(split[1], out value)) USE_ADMIN_SAFE_ = value; break;
                            case "SUICIDE_BOT_": if (bool.TryParse(split[1], out value)) SUICIDE_BOT_ = value; break;
                        }
                    }
                }
            }
            #endregion


            char[] numChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            PlayerConnecting += (player) =>
            {
                if (PREMATCH_DONE) return;
                string name = player.Name;
                if (name.StartsWith("bot"))
                {
                    Call("kick", player.EntRef);
                    if (TEST) print(name + "KICKED★");
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
                        if (TEST) print("BOT" + player.EntRef + " kicked before PMCH");
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
                Server_SetDvar();
                Server_Hud();
                if (DEPLAY_BOT_) deplayBOTs();
                //setCooling();
            });

            OnNotify("game_ended", (level) =>
            {
                GAME_ENDED_ = true;
            });
        }

        public override void OnExitLevel()
        {
            if (NEXT_MAP == null) NEXT_MAP = "mp_dome";
            AfterDelay(100, () =>
            {
                Utilities.ExecuteCommand("map" + NEXT_MAP);
            });
        }

    }
}

