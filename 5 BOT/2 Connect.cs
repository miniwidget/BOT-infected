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

        #region Bot_Connected
        private void Bot_Connected(Entity bot)
        {

            var i = BOTs_List.Count;

            if (i > BOT_SETTING_NUM)
            {
                OVERFLOW_BOT_ = true;
                Call("kick", bot.EntRef);
                return;
            }
            if (i == -1)
            {
                Call("kick", bot.EntRef);
                return;
            }

            bot.Notify("menuresponse", "changeclass", BOTs_CLASS[i]);

            B_FIELD.Add(new B_SET());
            BOT_ID.Add(bot.EntRef, i);
            BOTs_List.Add(bot);

            if (i == BOT_SETTING_NUM - 1) waitOnFirstInfected();
        }
        #endregion


        void waitOnFirstInfected()
        {
            //print("■ waitOnFirstInfected");

            string message = "If 1 user is on game and got infected, ^2RESTART  in 5 seconds";
            int failCount = 0;
            OnInterval(t2, () =>
            {
                if (failCount == 6)//in case, if over 12 sec, in a state that no one got infected. ※ Infected time is 8 sec.
                {
                    First_Infed_Player = BOTs_List[0];

                    if (HUMAN_CONNECTED_)//사람이 감염된 경우
                    {
                        if (human_List.Count == 1)

                            deplayBOTs_map_init(false, message, 5);
                        else
                            BotToAxisExceptLast();
                    }
                    else
                    {
                        BotToAxisExceptOne();
                    }

                    Server_Hud();

                    return false;
                }

                foreach (Entity player in Players)
                {
                    if (player == null || !player.IsPlayer) continue;
                    if (player.GetField<string>("sessionteam") == "axis")//감염 시작
                    {
                        First_Infed_Player = player;

                        if (player.Name.StartsWith("bot"))//봇이 감염된 경우
                        {
                            BotToAxisExceptOne();
                        }
                        else//사람이 감염된 경우
                        {
                            if (human_List.Count == 1)

                                deplayBOTs_map_init(false, message, 5);
                            else
                                BotToAxisExceptLast();
                        }

                        Server_Hud();

                        return false;
                    }

                }

                failCount++;
                return true;
            });

        }

        /// <summary>
        /// 봇이 처음 감염된 경우
        /// </summary>
        void BotToAxisExceptOne()
        {

            var max = BOTs_List.Count - 1;
            int i = 0;
            int fidx = BOTs_List.IndexOf(First_Infed_Player);
            int bot_ally = max;

            if (fidx == max) bot_ally -= 1;

            OnInterval(250, () =>
            {
                if (i == max)
                {
                    changeBotClass(First_Infed_Player, fidx, true);
                    return getTeamState();
                }
                if (i!= bot_ally)
                {
                    changeBotClass(BOTs_List[i], i, false);
                }

                i++;
                return true;
            });
        }

        /// <summary>
        /// 사람이 처음으로 감염된 경우
        /// </summary>
        void BotToAxisExceptLast()
        {
            var max = BOTs_List.Count - 1;

            int i = 0;
            OnInterval(250, () =>
            {
                if (i == max)
                {
                    return getTeamState();
                }

                changeBotClass(BOTs_List[i], i, false);
                i++;
                return true;
            });
        }


        /// <summary>
        /// 봇이 처음 감염된 경우 & 사람이 아무도 접속하지 않은 경우 = 대기상태를 만들기 위해 봇 1 마리를 살려 놓음
        /// </summary>


        void changeBotClass(Entity bot, int i, bool change)
        {

            if (i == 1 || i == 2)//1=RPG BOT 2=RIOT
            {
                bot.SpawnedPlayer += () => spawned_bot_slower(bot);
                spawned_bot_slower(bot);
            }
            else
            {
                bot.SpawnedPlayer += () => spawned_bot(bot);
                spawned_bot(bot);
            }

            if (!SUICIDE_BOT_) return;

            bot.Call("suicide");
            if (change) setTeamName();
        }

        bool getTeamState()
        {
            int alive = 0, max = BOTs_List.Count;
            foreach (Entity bot in BOTs_List)
            {
                if (isSurvivor(bot)) alive++;
            }
            print("■ BOTs: " + max + " AXIS: " + (max - alive) + " ALLIES: " + alive + " Inf : " + First_Infed_Player.Name);
            print("■ HUMANs: " +human_List.Count);
            return false;
        }

    }
}
