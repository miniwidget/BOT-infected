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
            var aw = player.GetField<int>("AX_WEP");
            if (aw == 0)
            {
                var fail_count = player.GetField<int>("FAIL_COUNT");
                if (fail_count >1)
                {
                    AxisWeapon_by_init(player);
                    return;
                }
                player.SetField("AX_WEP", 1);
                player.SetField("FAIL_COUNT", fail_count+1);
                player.Call("suicide");
                player.Notify("menuresponse", "changeclass", "axis_recipe4");

            }
            else if (aw == 1)
            {
                AxisHud(player);
                //if (DISABLE_MELEE_OF_INFECTED_) Utilities.RawSayTo(player, "^2[ ^7DISABLED ^2] Melee of the Infected");

                AxisWeapon_by_init(player);

            }
            else
            {
                var byAttack = player.GetField<bool>("byAttack");

                if (byAttack) AxisWeapon_by_Attack(player, aw);
                else AxisWeapon_by_init(player);
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
            dead.TakeWeapon(dead.CurrentWeapon);
            //dead.SetPerk(P.PL[0], true, true);
            //dead.SetPerk(P.CL[0], true, true);
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
            if (aw <3)
            {
                deadManWeapon = _launcherList[0];
                bullet = 1;
            }
            else if (aw ==3)
            {
                deadManWeapon = _launcherList[1];
                bullet = 1;
            }
            else if (aw==4)
            {
                deadManWeapon = _launcherList[2];
                bullet = 1;
            }
            else if (aw ==5)
            {
                deadManWeapon = _launcherList[3];
                bullet = 1;
            }
            else if (aw == 6)
            {
                dead.Health = 50;
                deadManWeapon = _sniperList[1];
                bullet = 1;
            }
            else if (aw ==7)
            {
                dead.Health = 50;
                deadManWeapon = _sniperList[4];
                bullet = 1;
            }
            else if (aw == 8)
            {
                dead.Health = 50;
                deadManWeapon = _sniperList[5];
            }
            else if (aw == 9)
            {
                dead.Health = 50;
                deadManWeapon = _arList[10];//10
                bullet = 4;
            }
            else if (aw == 10)
            {
                dead.Health = 50;
                deadManWeapon = _lmgList[2];//10
                bullet = 6;
            }
            else
            {
                AxisWeapon_by_init(dead);
                dead.Call("iPrintlnBold", "^2[ ^7NO UPGRADE ^2] Weapon of the Infected");
                return;
            }

            dead.GiveWeapon(deadManWeapon);
            dead.SwitchToWeaponImmediate(deadManWeapon);
           
            dead.Call("SetWeaponAmmoStock", DEAD_GUN, 0);
            dead.Call("SetWeaponAmmoClip", DEAD_GUN, bullet);

            dead.Call("iPrintlnBold", "^2[ ^7" + deadManWeapon + " ^2] Weapon of the Infected");
            dead.Notify("open_");
            AfterDelay(2000, () => dead.Call("iPrintlnBold", "^2[ ^71 DEATH ^2] Remaining of Upgrade Weapon"));
        }


    }
}
