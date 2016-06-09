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

        #region 일반
        //봇 스폰 시작
        private void spawned_bot(Entity bot)
        {
            int num = BOT_ID[bot.EntRef];
            if (num == -1)
            {
                removeBot(bot);
                return;
            }

            B_SET B = B_FIELD[num];
            B.target = null;
            B.fire = false;
            B.search = false;
            B.temp_fire = false;
            B.death += 1;

            bot.Call("hide");
            bot.Call("setmovespeedscale", 0f);
            bot.Health = -1;

            var weapon = bot.CurrentWeapon;
            bot.Call(33468, weapon, 0);//setweaponammoclip
            bot.Call(33469, weapon, 0);//setweaponammostock

            bot.AfterDelay(BOT_DELAY_TIME, b =>
            {
                B.fire = true;
                B.search = true;

                b.Call("show");
                b.Health = 100;
                if (num != 0) bot.Call("setmovespeedscale", 1f);
                else bot.Call("setmovespeedscale", 0.6f);

                start_bot_search(b, num, weapon, B);
            });

        }

        //봇 목표물 찾기 루프
        private void start_bot_search(Entity bot, int num, string weapon, B_SET B)
        {
            bool pause = false;

            int death = B.death;
            bot.OnInterval(SEARCH_TIME, b =>
            {
                if (GAME_ENDED_ || !B.search) return false;
                if (death != B.death) return false;

                if (!HUMAN_CONNECTED_)
                {
                    B.fire = false;
                    return true;
                }

                var target = B.target;

                if (target != null)//이미 타겟을 찾은 경우
                {
                    if (isSurvivor(target))
                    {
                        var POD = target.Origin.DistanceTo(bot.Origin);
                        if (POD < 600)//toDo : 멀리서 가격한 경우 추가할 것
                        {
                            pause = false;
                            return true;
                        }
                    }

                    B.target = null;
                    B.fire = false;
                    bot.Call(33468, weapon, 0);//setweaponammoclip
                }
                pause = true;

                //타겟 찾기 시작
                foreach (Entity human in human_List)
                {
                    var POD = human.Origin.DistanceTo(bot.Origin);

                    if (POD < 600)
                    {
                        B.target = human;
                        B.fire = true;
                        pause = false;
                        //int i = 0;
                        b.OnInterval(FIRE_TIME, bb =>
                        {
                            if (pause) return false;
                            if (!B.fire || GAME_ENDED_ || human == null) return false;
                            //print(bb.Name + bot_fire[num] + i++);
                            var ho = human.Origin; ho.Z -= 50;

                            Vector3 angle = Call<Vector3>(247, ho - bb.Origin);//vectortoangles
                            bb.Call(33531, angle);//SetPlayerAngles
                            bb.Call(33468, weapon, 5);//setweaponammoclip
                            return true;
                        });

                        return true;
                    }

                }

                return true;

            });

        }

        #endregion

        #region 느린 봇

        void removeBot(Entity bot)
        {
            print("■ IMPORTANT ■ " + bot.Name + "bid = -1");
            BOTs_List.Remove(bot);
            Call("kick", bot.EntRef);
        }
        void spawned_bot_slower(Entity bot)
        {
            int num = BOT_ID[bot.EntRef];
            if (num == -1)
            {
                removeBot(bot);
                return;
            }
            if (num == 1)
            {
                bot.Call(33468, "rpg_mp", 0);//setweaponammoclip
                bot.Call(33469, "rpg_mp", 0);//setweaponammostock
            }

            B_SET B = B_FIELD[num];
            B.target = null;
            B.fire = false;
            B.search = false;
            B.temp_fire = false;
            B.death += 1;

            bot.Call("hide");
            bot.Call("setmovespeedscale", 0f);
            bot.Health = -1;

            bot.AfterDelay(10000, b =>
            {
                B.fire = true;
                B.search = true;

                bot.Health = 100;
                b.Call("show");


                if (num == 1)
                {
                    bot.Call("setmovespeedscale", 0.7f);
                    start_bot_search_slower(b, num, 1500, B);
                }
                else
                {
                    bot.Call("setmovespeedscale", 1.5f);
                    start_bot_search_slower(b, num, 500, B);
                }
            });
        }
        private void start_bot_search_slower(Entity bot, int num, int fire_time, B_SET B)
        {
            bool pause = false;
            int death = B.death;

            bot.OnInterval(SEARCH_TIME, b =>
            {
                if (GAME_ENDED_ || !B.search) return false;
                if (death != B.death) return false;

                if (!HUMAN_CONNECTED_)
                {
                    B.fire = false;
                    return true;
                }

                var target = B.target;

                if (target != null)//이미 타겟을 찾은 경우
                {
                    if (isSurvivor(target))
                    {
                        var POD = target.Origin.DistanceTo(bot.Origin);
                        if (POD < 600)
                        {
                            pause = false;
                            return true;
                        }
                    }

                    B.target = null; //타겟과 거리가 멀어진 경우, 타겟 제거
                    B.fire = false;
                    if (num == 1) bot.Call(33468, "rpg_mp", 0);//setweaponammoclip
                }

                pause = true;
                //타겟 찾기 시작
                foreach (Entity human in human_List)
                {

                    var POD = human.Origin.DistanceTo(bot.Origin);

                    if (POD < 600)
                    {
                        B.target = human;
                        B.fire = true;
                        pause = false;
                        if (num == 1) human.Call(33466, "missile_incoming");

                        b.OnInterval(fire_time, bb =>
                        {
                            if (pause) return false;
                            if (!B.fire || GAME_ENDED_ || human == null) return false;

                            var ho = human.Origin; ho.Z -= 50;

                            Vector3 angle = Call<Vector3>(247, ho - bb.Origin);//vectortoangles
                            bb.Call(33531, angle);//SetPlayerAngles
                            if (num == 1) bb.Call(33468, "rpg_mp", 1);//setweaponammoclip
                            return true;
                        });

                        return true;
                    }

                }

                return true;

            });

        }

        #endregion

    }
}
