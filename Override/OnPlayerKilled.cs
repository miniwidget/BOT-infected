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

        public override void OnPlayerDamage(Entity player, Entity inflictor, Entity attacker, int damage, int dFlags, string mod, string weapon, Vector3 point, Vector3 dir, string hitLoc)
        {

            //if (mod == "MOD_FALLING" && !isBOT(player)) player.Health += damage;

            //if (_Disable_Melee_of_Infected) if (mod == "MOD_MELEE" && isSurvivor(attacker)) player.Health += damage;//if (Enable_Auto_Melee_of_BOTs) { }

            if (attacker == player)
            {
                if (weapon == "rpg_mp") player.Health += damage;
            }

            if (USE_ADMIN_SAFE_) if (ADMIN != null) ADMIN.Health += damage;
        }

        public override void OnPlayerKilled(Entity killed, Entity inflictor, Entity attacker, int damage, string mod, string weapon, Vector3 dir, string hitLoc)
        {

            if (attacker == null || !attacker.IsPlayer) return;
            bool BotKilled = killed.Name.StartsWith("bot");

            if (mod == "MOD_SUICIDE" && BotKilled) return;

            bool BotAttker = attacker.Name.StartsWith("bot");
            if (!BotAttker)
            {
                var pc = attacker.GetField<int>("PERK");

                if (pc < 34 && weapon[2] == '5')//iw5
                {
                    attacker.SetField("PERK", pc + 1);

                    var i = pc % 3;
                    if (i == 0)
                    {
                        attacker.Call(33466, "mp_killstreak_radar");//playlocalsound
                        attacker.AfterDelay(100, a => Perk_Hud(attacker, pc / 3));
                    }
                }
                //Utilities.RawSayAll(attacker.Name + " ^2KILLED ^2/ ^7" + killed.Name + "!!!");
            }

            if (!BotKilled)//사람이 죽은 경우
            {
                if (BotAttker) // 봇이 사람을 죽인 경우
                {
                    AfterDelay(100, () =>    Utilities.RawSayAll("^1BAD Luck :) ^7" + killed.Name + " killed by " + attacker.Name));
                    int num = attacker.GetField<int>("tNum");
                    attacker.SetField("tNum", -1);
                    bot_search[num] = bot_fire[num] = false;
                }

                if (killed == attacker)
                    killed.SetField("byAttack", false);//공격으로 죽음
                else
                    killed.SetField("byAttack", true);//자살로 죽음
            }

        }


    }
}
