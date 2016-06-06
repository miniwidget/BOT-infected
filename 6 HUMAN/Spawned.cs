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
        void human_spawned(Entity player)
        {
            var LIFE = player.GetField<int>("LIFE");

            if (LIFE == 0)//LIFE 1 more time
            {
                player.SetField("LIFE", 1);
               
                player.Call("playlocalsound", "mp_last_stand");
                player.SetField("sessionteam", "allies");
                player.Notify("menuresponse", "team_marinesopfor", "allies");
                player.Call("closePopupMenu");
                setTeamName();
                player.Call("iPrintlnBold", "^2[ ^71 LIFE ^2] MORE");
            }
            else if (LIFE == 1)//Imediatley After Change Team, give a new random weapon
            {
                player.AfterDelay(100, x =>
                {
                    giveWeaponTo(player, getRandomWeapon());
                    player.SetField("LIFE", 2);
                });
            }
            else if (LIFE == 2)//change to AXIS
            {
                player.SetField("LIFE",3);
                player.SetField("sessionteam", "axis");
                human_List.Remove(player);
                player.SetField("AX_WEP", 1);
                player.Call("suicide");
                player.Notify("menuresponse", "changeclass", "axis_recipe4");
                print(player.Name + " : Infected ⊙..⊙");
            }
            else 
            {
                dead_spawned(player);
            }
        }
        void dead_spawned(Entity player)
        {
            var aw = player.GetField<int>("AX_WEP");

            if (aw == 1)
            {
                AxisHud(player);
                Utilities.RawSayTo(player, "^2[ ^7DISABLED ^2] Melee of the Infected");

                AxisWeapon_by_init(player);
            }
            else
            {
                var BY_SUICIDE = player.GetField<int>("BY_SUICIDE");

                if (BY_SUICIDE==1) AxisWeapon_by_init(player); 
                else AxisWeapon_by_Attack(player, aw);
            }

        }
        /// <summary>
        /// 죽은 사람 무기 초기화
        /// </summary>
        /// <param name="dead"></param>
        /// 
        string DEAD_GUN = "iw5_deserteagle_mp_tactical";
        void AxisWeapon_by_init(Entity dead)
        {
            dead.SetField("AX_WEP", 2);

            dead.TakeWeapon(dead.CurrentWeapon);
            dead.GiveWeapon(DEAD_GUN);
            dead.AfterDelay(100, d =>
            {
                dead.SwitchToWeaponImmediate(DEAD_GUN);
                dead.Call("SetWeaponAmmoClip", DEAD_GUN, 3);
                dead.Call("SetWeaponAmmoStock", DEAD_GUN, 0);
            });

            AfterDelay(t2, () => dead.Notify("open_"));

        }

        /// <summary>
        /// 감염자가 계속 죽을 경우 총기를 주는 어드밴티지를 줌.
        /// </summary>
        /// <param name="player"></param>
        void AxisWeapon_by_Attack(Entity dead, int aw)
        {
            dead.TakeWeapon(dead.CurrentWeapon);

            dead.SetField("AX_WEP", aw + 1);
            dead.Health = 70;

            string deadManWeapon = "";
            int bullet = 0;
            if (aw < 3)
            {
                deadManWeapon = LAUNCHER_LIST[1];
                bullet = 1;
            }
            else if (aw == 3)
            {
                deadManWeapon = LAUNCHER_LIST[2];
                bullet = 1;
            }
            else if (aw == 4)
            {
                deadManWeapon = "iw5_mp412_mp";
                bullet = 1;
            }
            else if (aw == 5)
            {
                deadManWeapon = "iw5_44magnum_mp";
                bullet = 1;
            }
            else if (aw == 6)
            {
                deadManWeapon = SNIPE_LIST[1];
                bullet = 1;
            }
            else if (aw == 7)
            {
                deadManWeapon = SNIPE_LIST[4];
                bullet = 1;
            }
            else if (aw == 8)
            {
                deadManWeapon = SNIPE_LIST[5];
                bullet = 1;
            }
            else if (aw == 9)
            {
                deadManWeapon = AR_LIST[0];//10
                bullet = 4;
            }
            else if (aw == 10)
            {
                deadManWeapon = LMG_LIST[2];//10
                bullet = 6;
            }
            else
            {
                AxisWeapon_by_init(dead);
                dead.Call("iPrintlnBold", "^2[ ^7AGAIN ^2] Init Weapon of the Infected");
                return;
            }

            dead.GiveWeapon(deadManWeapon);
            dead.AfterDelay(100, x =>
            {
                dead.SwitchToWeaponImmediate(deadManWeapon);

                dead.Call("SetWeaponAmmoStock", deadManWeapon, 0);
                dead.Call("SetWeaponAmmoClip", deadManWeapon, bullet);
            });

            dead.Call("iPrintlnBold", "^2[ ^7" + deadManWeapon + " ^2] Weapon of the Infected");
            dead.Notify("open_");
        }
    }
}
