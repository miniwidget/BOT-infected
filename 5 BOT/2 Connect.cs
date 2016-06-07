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
        string[] BOTs_CLASS = { "axis_recipe1", "axis_recipe2", "axis_recipe3", "class0", "class1", "class2", "class4", "class5", "class6", "class6" };
        private void Bot_Connected(Entity bot)
        {
            var i = BOTs_List.Count;

            if (i >= 10)
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

            //IMPORTANT
            bot.SetField("bid", i);
            bot.SetField("tNum", -1);

            BOTs_List.Add(bot);

            if (i == 1) waitOnFirstInfected();
        }
        #endregion


        void waitOnFirstInfected()
        {
            print("■ waitOnFirstInfected");

            int failCount = 0;
            OnInterval(t2, () =>
            {
                if (failCount == 6)
                {
                    if (human_List.Count != 0 && BOTs_List.Count != 0)
                    {
                        foreach (Entity bot in BOTs_List) bot.Call("suicide");
                    }
                    else
                    {
                        print("■ ■ ■ ■ waitOnFirstInfected - FAIL : return false");
                        deplayBOTs_map_init(true, "SOMETHING WRONG HAPPEND.RESTART MAP in 5 seconds", 5);
                    }
                    return false;
                }
                for (int i = 0; i < 17; i++)
                {
                    Entity player = Call<Entity>("getentbynum", i);
                    if (player != null && player.IsPlayer)
                    {
                        if (player.GetField<string>("sessionteam") == "axis")//감염 시작
                        {

                            var name = player.Name;
                            if (name.StartsWith("bot"))//봇이 감염된 경우
                            {
                                first_Inf_BOT = player;

                                if (HUMAN_CONNECTED_)//사람이 접속한 경우
                                {
                                    BotSuicideExceptFirst();
                                }
                                else//사람이 아무도 없는 경우
                                {
                                    BotSuicideExceptFinal();
                                }
                            }
                            else
                            {
                                BotSuicideAll(player);//사람이 감염된 경우
                            }

                            if (SUICIDE_BOT_) //확인사살

                                AfterDelay(10000, () =>
                                {
                                    if (HUMAN_CONNECTED_)
                                    {
                                        int num = 0;
                                        foreach (Entity bot in BOTs_List)
                                        {
                                            if (!isSurvivor(bot)) num++;
                                        }
                                        if (num < 8)
                                        {
                                            foreach (Entity bot in BOTs_List)
                                            {
                                                bot.Call("suicide");
                                            }
                                            print("■ BOTs SUICIDE ALL executed");
                                        }
                                    }
                                });
                            return false;
                        }

                    }
                }

                failCount++;
                return true;
            });

        }


        /// <summary>
        /// 봇이 처음 감염된 경우
        /// </summary>
        void BotSuicideExceptFirst()
        {
            print("■ BotSuicideExceptFirst. HUMAN : " + human_List.Count + "\n■ Inf : " + first_Inf_BOT.Name);

            var max = BOTs_List.Count - 1;
            int i = 0;
            int fidx = BOTs_List.IndexOf(first_Inf_BOT);

            OnInterval(250, () =>
            {
                if (i == max)
                {
                    changeBotClass(first_Inf_BOT, fidx, true);
                    return false;
                }

                if (i != fidx)
                {
                    changeBotClass(BOTs_List[i], i, false);
                }

                i++;
                return true;
            });
        }

        /// <summary>
        /// 봇이 처음 감염된 경우 + 사람이 아무도 접속하지 않은 경우 = 대기상태를 만들기 위해 봇 2 마리를 살려 놓음
        /// </summary>
        void BotSuicideExceptFinal()
        {
            var max = BOTs_List.Count - 1;

            int fidx = BOTs_List.IndexOf(first_Inf_BOT);

            int lastidx = 0;
            if (fidx == 0) lastidx = 1; else lastidx = fidx - 1;

            int axis = 0, surv = 0;

            int i = 0;

            OnInterval(250, () =>
            {
                if (i == max)
                {
                    changeBotClass(first_Inf_BOT, fidx, true);
                    foreach (Entity bot in BOTs_List)
                    {
                        if (isSurvivor(bot)) surv++;
                        else axis++;
                    }
                    print("■ NO HUMAN.\n■ BOTs : " + (max + 1) + " AXIS: " + axis + " ALLIES: " + surv);
                    return false;
                }
                if (i == fidx || i == lastidx)
                {
                    //if (TEST_)
                    //{
                    //    if(i == fidx) print(BOTs_List[i].Name + "//FIRST_inf:" + i);
                    //    else print(BOTs_List[i].Name + "//LAST_inf:" + i);
                    //}
                    if (i != max) i++;
                    return true;// 살려 놓은 봇 , 첫 감염자 봇
                }

                changeBotClass(BOTs_List[i], i, false);
                //if (TEST_) print(BOTs_List[i].Name + "//" + i);
                i++;
                return true;
            });
        }

        /// <summary>
        /// 봇 다 죽임. 사람이 처음으로 감염된 경우
        /// </summary>
        void BotSuicideAll(Entity player)
        {
            print("■ BotSuicideAll. BOTs: " + BOTs_List.Count + " HUMAN : " + human_List.Count + "\n■ Inf : " + player.Name);

            var max = BOTs_List.Count - 1;
            int i = 0;
            OnInterval(250, () =>
            {
                if (i == max)
                {
                    return false;
                }
                if (i == 0)
                {
                    i++;
                    return true;
                }
                changeBotClass(BOTs_List[i], i, false);
                i++;
                return true;
            });
        }

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

    }
}
