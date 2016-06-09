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
 

        void tempFire(Entity bot, Entity human,string mod)
        {
            return;
        }

        public override void OnPlayerDamage(Entity player, Entity inflictor, Entity attacker, int damage, int dFlags, string mod, string weapon, Vector3 point, Vector3 dir, string hitLoc)
        {
            if (attacker == null || !attacker.IsPlayer) return;
            bool isBOT = player.Name.StartsWith("bot");

            if (isBOT)
            {
                if (attacker == player)
                {
                    if (weapon == "rpg_mp") player.Health += damage;
                    return;
                }

                //player.Call(33468, weapon, 0);
                //var ho = attacker.Origin; ho.Z -= 50;
                //Vector3 angle = Call<Vector3>(247, ho - player.Origin);//vectortoangles
                //player.Call(33531, angle);//SetPlayerAngles
                //player.Call(33468, weapon, 5);//setweaponammoclip

                return;
            }
            else
            {
                if (mod == "MOD_MELEE")
                {
                    if (Disable_Melee_)
                    {
                        if (!isBOT && isSurvivor(player)) player.Health += damage;
                    }
                    return;
                }
            }

            if (USE_ADMIN_SAFE_)
            {
                if (ADMIN != null && player == ADMIN) ADMIN.Health += damage;
            }
        }

        public override void OnPlayerKilled(Entity killed, Entity inflictor, Entity attacker, int damage, string mod, string weapon, Vector3 dir, string hitLoc)
        {

            if (attacker == null || !attacker.IsPlayer) return;
            bool BotKilled = killed.Name.StartsWith("bot");

            if (mod == "MOD_SUICIDE" && BotKilled) return;

            bool BotAttker = attacker.Name.StartsWith("bot");

           

            if (!BotAttker)
            {

                H_SET H = H_FIELD[H_ID[attacker.EntRef]];
                var pc = H.PERK;//  attacker.GetField<int>("PERK");

                if (pc < 34 && weapon[2] == '5')//iw5
                {
                    H.PERK += 1;
                    var i = H.PERK;

                    if (i > 2 && i % 3 == 0)
                    {
                        attacker.Call(33466, "mp_killstreak_radar");//playlocalsound
                        attacker.AfterDelay(100, a => Perk_Hud(attacker, i / 3));
                    }
                }

            }

            if (!BotKilled)//사람이 죽은 경우
            {
                if (BotAttker) // 봇이 사람을 죽인 경우
                {
                    Utilities.RawSayAll("^1BAD Luck :) ^7" + killed.Name + " killed by " + attacker.Name);
                    int id = BOT_ID[attacker.EntRef];
                    B_SET B = B_FIELD[id];
                    B.fire = false;
                    B.target = null;
                    return;
                }
                
                if (killed == attacker)
                    H_FIELD[H_ID[killed.EntRef]].BY_SUICIDE = true;//자살로 죽음
                else
                    H_FIELD[H_ID[killed.EntRef]].BY_SUICIDE = false;//공격으로 죽음
            }

        }


    }
}
