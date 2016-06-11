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
            if (GAME_ENDED_) return;

            int num = bot.EntRef;
            if (num == -1) return;
            
            B_SET B = B_FIELD[num];
            B.target = null;
            B.fire = false;
            B.temp_fire = false;
            B.death += 1;
            if (B.wep == null) B.wep = bot.CurrentWeapon;
            bot.Call("hide");
            bot.Call("setmovespeedscale", 0f);


            if (num != 0) bot.Health = -1;//Not Jugg bot

            var weapon = B.wep;
            bot.Call(33468, weapon, 0);//setweaponammoclip
            bot.Call(33469, weapon, 0);//setweaponammostock

            bot.AfterDelay(BOT_DELAY_TIME, b =>
            {
                if (GAME_ENDED_) return;
                B.fire = true;
                b.Call("show");

                if (num != 0)//GENERAL
                {
                    b.Health = 150;
                    bot.Call("setmovespeedscale", 1f);
                }
                else//JUGG
                {
                    bot.Call("setmovespeedscale", 0.6f);
                }

                start_bot_search(b, B);
            });
        }

        //봇 목표물 찾기 루프
        private void start_bot_search(Entity bot, B_SET B)
        {
            bool pause = false;
            int death = B.death;
            string weapon = B.wep;

            bot.OnInterval(SEARCH_TIME, b =>
            {
                if (death != B.death) return false;
                if (!HUMAN_CONNECTED_) return !(pause = false);

                var target = B.target;

                if (target != null)//이미 타겟을 찾은 경우
                {
                    if (human_List.Contains(target))
                    {
                        //if (TEST_) return true;
                        var POD = target.Origin.DistanceTo(bot.Origin);
                        if (POD < FIRE_DIST) return !(pause = false); 
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

                    if (POD < FIRE_DIST)
                    {
                        B.target = human;
                        B.fire = true;
                        pause = false;

                        b.OnInterval(FIRE_TIME, bb =>
                        {
                            if (pause || !B.fire ) return false;

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

        void spawned_bot_slower(Entity bot)
        {
            if (GAME_ENDED_) return;

            int num = bot.EntRef;
            if (num == -1) return;
            
            if (num == RPG_BOT_ENTREF)
            {
                bot.Call(33468, "rpg_mp", 0);//setweaponammoclip
                bot.Call(33469, "rpg_mp", 0);//setweaponammostock
            }

            B_SET B = B_FIELD[num];
            B.target = null;
            B.fire = false;
            B.temp_fire = false;
            B.death += 1;
            if (B.wep == null) B.wep = bot.CurrentWeapon;

            bot.Call("hide");
            bot.Call("setmovespeedscale", 0f);
            bot.Health = -1;


            bot.AfterDelay(10000, b =>
            {
                if (GAME_ENDED_) return;

                B.fire = true;

                bot.Health = 150;
                b.Call("show");


                if (num == RPG_BOT_ENTREF)//rpg bot
                {
                    bot.Call("setmovespeedscale", 0.7f);
                    start_bot_search_slower(b, B);
                }
                else//riot bot
                {
                    bot.Call("setmovespeedscale", 2f);
                }
            });
        }
        private void start_bot_search_slower(Entity bot, B_SET B)
        {
            bool pause = false;
            int death = B.death;

            bot.OnInterval(SEARCH_TIME, b =>
            {
                if (death != B.death) return false;
                if (!HUMAN_CONNECTED_) return !(pause = false);
                

                var target = B.target;
                if (target != null)//이미 타겟을 찾은 경우
                {
                    if (human_List.Contains(target))
                    {
                        //if (TEST_) return true;
                        var POD = target.Origin.DistanceTo(bot.Origin);
                        if (POD < FIRE_DIST) return !(pause = false);

                    }

                    B.target = null; //타겟과 거리가 멀어진 경우, 타겟 제거
                    B.fire = false;
                    bot.Call(33468, "rpg_mp", 0);//setweaponammoclip
                }

                pause = true;
                //B.rooping = true;
                //타겟 찾기 시작
                foreach (Entity human in human_List)
                {

                    var POD = human.Origin.DistanceTo(bot.Origin);

                    if (POD < FIRE_DIST)
                    {
                        B.target = human;
                        B.fire = true;
                        pause = false;
                        human.Call(33466, "missile_incoming");

                        b.OnInterval(1500, bb =>
                        {

                            if (pause || !B.fire ) return false;

                            var ho = human.Origin; ho.Z -= 50;

                            Vector3 angle = Call<Vector3>(247, ho - bb.Origin);//vectortoangles
                            bb.Call(33531, angle);//SetPlayerAngles
                            bb.Call(33468, "rpg_mp", 1);//setweaponammoclip
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
