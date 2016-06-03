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
        //Entity JJUG_BOT,HELLI_BOT;

        /*
        1번 저거넛 봇
        2번 RPG 봇
        3번 리엇 봇
        4번 인간


        1. OK!!! session team / isAlive 상태 확인

        2. OK 사람이 죽었을 때, 탄알 3개만 줌

        3. 봇 클래스 변경 가능한지 확인
        
        4. OK survivor 킬스트렉 어설트 드론 을 공격용 헬기로 변경

        5. dead man 에게 헬기런쳐 지급

        6. OK 메뉴 총기 리스트 변경
        gameOpt commonOption.allowCustomClasses "1"
        gameOpt commonOption.forceRespawn "0"

                bool firstSpawn_HelliBOT = true;
        bool secondSpawn;

                void spawnd_Helli_BOT()
        {
            if (ADMIN == null) return;
            if (HELLI_BOT == null) return;
            if (!secondSpawn)
            {
                secondSpawn = true;
                return;
            }
            HELLI_BOT.Call("setmovespeedscale", 0);
            if (VEHICLE != null) VEHICLE.Call(32928);
            var o = HELLI_BOT.Origin; o.Z += 1000;
            string minimap_model = "attack_littlebird_mp";
            string realModel = "vehicle_little_bird_armed";
            VEHICLE = Call<Entity>("spawnhelicopter", HELLI_BOT, o, HELLI_BOT.GetField<Vector3>("angles"), minimap_model, realModel);
            //HELLI_BOT.Call("linkto", VEHICLE, "tag_origin", TAG_ORIGIN[0], TAG_ORIGIN[1]);

            Entity turret = Call<Entity>(19, "misc_turret", HELLI_BOT.Origin, "littlebird_guard_minigun_mp", false);
            turret.Call("linkto", VEHICLE, "tag_origin", new Vector3(30, 30, 0), new Vector3(0, 0, 0));
            turret.Call(32929, "mp_remote_turret");//setmodel mp_remote_turret
            turret.Call(32942);//MakeUnusable
            turret.Call(33052);//maketurretsolid
            turret.Call(33417, true);//setcandamage

            HELLI_BOT.Call("remotecontrolvehicle", VEHICLE);//remotecontrolvehicle
           AfterDelay(1000,()=> HELLI_BOT.Call("remotecontrolvehicle", turret));//remotecontrolvehicle

            if (firstSpawn_HelliBOT)
            {
                firstSpawn_HelliBOT = false;
                //bool control = true;
                OnInterval(250, () =>
                {
                    var ho = ADMIN.Origin; ho.Z -= 50;

                    Vector3 temp_ = Call<Vector3>(247, ho - HELLI_BOT.Origin);//vectortoangles
                    HELLI_BOT.Call(33531, temp_);//SetPlayerAngles

                    return true;
                });
            }
            //player.AfterDelay(60000, e =>
            //{
            //    player.Call(32843);//unlink
            //    player.Call(33257);//remotecontrolvehicleoff
            //    player.Call(32980);//remotecontrolturretoff
            //    turret.Call(32928);//delete
            //    lbExplode(LB);
            //    Restore(player, false);
            //});
        }
                Entity bot = null;
            bot.SpawnedPlayer += () => spawned_bot(bot);
            if (TEST) return;
            bot.AfterDelay(10000, p =>
            {
                if (bot.GetField<string>("sessionteam") == "axis")
                {
                    p.AfterDelay(5000, x => bot.Call("suicide"));
                }
                else
                {
                    if (!FINAL_BOT_FOUND && !HUMAN_CONNECTED_)
                    {
                        FINAL_BOT_FOUND = true;
                        return;
                    }
                    bot.Call("suicide");
                }

            });
                            //b.Call("setorigin", o);
                //b.Call("setmovespeedscale", 1);//setmovespeedscale

        */

    }
}
