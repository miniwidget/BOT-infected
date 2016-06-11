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
        #region isFirstInfected
        bool HUMAN_FIRST_SPAWNED = true;
        bool isFirstInfected
        {
            get
            {
                HUMAN_FIRST_SPAWNED = false;

                if (BOTs_List.Count == 0) return false;

                foreach (Entity bot in BOTs_List)
                {
                    if (!isSurvivor(bot)) return false;//감염된 봇이 있는 경우
                }
                //감염된 봇이 없는 경우
                return true;
            }
        }
        #endregion

        #region human_spawned
        void human_spawned(Entity player)//LIFE 1 or 2
        {

            H_SET H = H_FIELD[player.EntRef];

            if (HUMAN_FIRST_SPAWNED) if (isFirstInfected) H.LIFE = -1;

            var LIFE = H.LIFE;
            if (LIFE > -1)//3 2
            {
                if (!H.RESPAWN)
                {
                    player.Call("iPrintlnBold", "^2[ ^7" + LIFE + " LIFE ^2] MORE");
                    H.LIFE -= 1;
                    H.RESPAWN = true;
                    player.Call("playlocalsound", "mp_last_stand");
                    player.SetField("sessionteam", "allies");
                    player.Notify("menuresponse", "team_marinesopfor", "allies");
                    player.Call("closePopupMenu");
                    setTeamName();
                }
                else
                {
                    H.PERK = 2;
                   
                    player.AfterDelay(100, x =>
                    {
                        giveWeaponTo(player, getRandomWeapon());
                        H.RESPAWN = false;
                    });
                }
            }
            else if (LIFE == -1)//change to AXIS
            {
                H.LIFE = -2;
                H.AX_WEP = 1;

                player.SetField("sessionteam", "axis");
                human_List.Remove(player);
                player.AfterDelay(100, p =>
                {

                    player.Call("suicide");
                    player.Notify("menuresponse", "changeclass", "axis_recipe4");
                    print(player.Name + " : Infected ⊙..⊙");
                });
            }
            else
            {
                var aw = H.AX_WEP;
                if (aw == 1)
                {
                    player.Call("iPrintlnBold", "^2[ ^7DISABLED ^2] Melee of the Infected");
                    H.PERK = 50;
                    AxisHud(player);
                    AxisWeapon_by_init(player);
                }
                else
                {

                    if (!H.BY_SUICIDE)
                    {
                        H.AX_WEP += 1;
                        AxisWeapon_by_Attack(player, H.AX_WEP);
                    }
                    else
                    {
                        AxisWeapon_by_init(player);
                    }
                }
            }

        }
        #endregion

        /// <summary>
        /// 죽은 사람 무기 초기화
        /// </summary>
        /// <param name="dead"></param>
        void AxisWeapon_by_init(Entity dead)
        {
            string DEAD_GUN = "iw5_deserteagle_mp_tactical";

            H_FIELD[dead.EntRef].AX_WEP = 2;
            dead.TakeWeapon(dead.CurrentWeapon);
            dead.GiveWeapon(DEAD_GUN);
            dead.AfterDelay(100, d =>
            {
                dead.SwitchToWeaponImmediate(DEAD_GUN);
                dead.Call("SetWeaponAmmoClip", DEAD_GUN, 3);
                dead.Call("SetWeaponAmmoStock", DEAD_GUN, 0);
            });

            dead.AfterDelay(t2, d => dead.Notify("open_"));
        }

        /// <summary>
        /// 감염자가 계속 죽을 경우 총기를 주는 어드밴티지를 줌.
        /// </summary>
        /// <param name="player"></param>
        void AxisWeapon_by_Attack(Entity dead, int aw)
        {
            dead.TakeWeapon(dead.CurrentWeapon);
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
            else if (aw < 7)
            {
                deadManWeapon = "iw5_44magnum_mp";
                bullet = 1;
            }
            else if (aw == 7)
            {
                deadManWeapon = SN_LIST[1];
                bullet = 1;
            }
            else if (aw == 8)
            {
                deadManWeapon = "iw5_mp412_mp";
                bullet = 1;
            }
            else if (aw == 9)
            {
                deadManWeapon = SN_LIST[4];
                bullet = 1;
            }
            else if (aw == 10)
            {
                deadManWeapon = "iw5_44magnum_mp";
                bullet = 2;
            }
            else if (aw == 11)
            {
                deadManWeapon = SN_LIST[5];
                bullet = 1;
            }
            else if (aw == 12)
            {
                deadManWeapon = LAUNCHER_LIST[1];
            }
            else if (aw == 13)
            {

                deadManWeapon = AR_LIST[0];//10
                bullet = 6;
            }
            else if (aw == 14)
            {
                deadManWeapon = LM_LIST[2];//10
                bullet = 6;
            }
            else if (aw == 15)
            {
                deadManWeapon = "iw5_44magnum_mp";
                bullet = 2;
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
