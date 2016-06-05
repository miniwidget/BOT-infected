using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;

namespace Infected
{
    public partial class Infected
    {
        string GetRandomWeapon()
        {
            int i = rnd.Next(7);
            switch (i)
            {
                case 0: return AP();
                case 1: return AG();
                case 2: return AR();
                case 3: return SM();
                case 4: return LM();
                case 5: return SG();
                case 6: return SN();
            }
            return AR();
        }
        void giveWeaponTo(Entity player, string weapon)
        {
            player.TakeWeapon(player.CurrentWeapon);
            player.GiveWeapon(weapon);
            player.Call("givemaxammo", weapon);
            player.AfterDelay(100,x=>player.SwitchToWeaponImmediate(weapon));
        }

        string AP() { return _autoPistolList[rnd.Next(4)]; }
        string AG() { return _akimboGunlList[rnd.Next(6)]; }
        string AR() { return _arList[rnd.Next(10)] + _camoList[rnd.Next(11)]; }
        string SM() { return _smgList[rnd.Next(6)] + _camoList[rnd.Next(11)]; }
        string LM() { return _lmgList[rnd.Next(5)] + _camoList[rnd.Next(11)]; }
        string SG() { return _sgList[rnd.Next(4)] + _camoList[rnd.Next(11)]; }
        string SN() { return _sniperList[rnd.Next(6)] + _camoList[rnd.Next(11)]; }

        string[] _autoPistolList = new[]{ "iw5_fmg9_mp_akimbo", "iw5_skorpion_mp_akimbo", "iw5_mp9_mp_akimbo", "iw5_g18_mp_akimbo", };
        string[] _akimboGunlList = new[] { "iw5_mp412_mp_akimbo","iw5_p99_mp_akimbo","iw5_44magnum_mp_akimbo","iw5_usp45_mp_akimbo","iw5_fnfiveseven_mp_akimbo","iw5_deserteagle_mp_akimbo" };

        string[] _arList = new[] 
       {
            "iw5_ak47_mp_gp25", "iw5_m16_mp_gl", "iw5_m4_mp_gl",
            "iw5_fad_mp_m320", "iw5_acr_mp_m320", "iw5_type95_mp_m320",
            "iw5_mk14_mp_m320", "iw5_scar_mp_m320", "iw5_g36c_mp_m320",
            "iw5_cm901_mp_m320",
        };
        
        string[] _smgList = new[] //6
        {
            "iw5_mp5_mp_hamrhybrid_rof_silencer", "iw5_m9_mp_hamrhybrid_rof_silencer", "iw5_p90_mp_hamrhybrid_rof_silencer",//hamrhybrid
            "iw5_pp90m1_mp_hamrhybrid_rof_silencer", "iw5_ump45_mp_hamrhybrid_rof_silencer", "iw5_mp7_mp_rof_silencer_hamrhybrid",
        };

        string[] _lmgList = new[] { "iw5_m60_mp_grip","iw5_mk46_mp_grip","iw5_pecheneg_mp_grip","iw5_sa80_mp_grip","iw5_mg36_mp_grip" };

        static string[] _sgList = new[] { "iw5_spas12_mp", "iw5_aa12_mp", "iw5_striker_mp", "iw5_1887_mp", "iw5_usas12_mp", };

        string[] _sniperList = new[] //6
        {
            "iw5_dragunov_mp_dragunovscopevz_xmags", "iw5_msr_mp_msrscopevz_xmags", "iw5_barrett_mp_barrettscopevz_xmags",
            "iw5_rsass_mp_rsassscopevz_xmags", "iw5_as50_mp_as50scopevz_xmags", "iw5_l96a1_mp_l96a1scopevz_xmags",
        };

        string[] _launcherList = new[] { "stinger_mp", "m320_mp", "xm25_mp", "javelin_mp", };
        string[] _camoList = new[] { "_camo01", "_camo02", "_camo03", "_camo04", "_camo05", "_camo06", "_camo07", "_camo08", "_camo09", "_camo10", "_camo11" };


        void Silencer(Entity ent)
        {
            string wep = ent.CurrentWeapon;
            if (wep == string.Empty || wep == "none")
                return;
            string OldWep = ent.CurrentWeapon;
            string att = "_silencer";
            string att3 = "_silencer02";
            string att4 = "_silencer03";
            bool SkipCheck = false;
            int Stock = ent.Call<int>("getWeaponAmmoStock", wep);
            int Clip = ent.Call<int>("getWeaponAmmoClip", wep);
            ent.TakeWeapon(wep);
            if (wep.Contains(att))
            {
                int index = wep.IndexOf(att);
                if (index + 9 < wep.Length)
                {
                    char[] derp = wep.ToCharArray(index + 9, (wep.Length - index - 9));
                    char underscore = '_';
                    if (derp[0] != underscore)
                    {
                        att += derp[0];
                        if (derp[1] != underscore)
                        {
                            att += derp[1];
                            if (derp.Length > 2)
                                if (derp[2] != underscore)
                                    GameLog.Write("Error detachting silencer; Current weapon was: " + wep);
                        }
                    }
                }

                wep = wep.Replace(att, string.Empty);
                SkipCheck = true;
            }
            else
            {
                wep = wep + att3 + att4 + att;
            }
            ent.GiveWeapon(wep);
            ent.Call("switchtoweaponimmediate", wep);
            ent.Call("setWeaponAmmoClip", wep, Clip);
            ent.Call("setWeaponAmmoStock", wep, Stock);

            if (SkipCheck)
                return;
            ent.AfterDelay(150, player =>
            {
                if (player.CurrentWeapon != "none")
                    return;

                wep = OldWep + att4 + att + att3;
                player.GiveWeapon(wep);
                player.Call("switchtoweaponimmediate", wep);
                player.Call("setWeaponAmmoClip", wep, Clip);
                player.Call("setWeaponAmmoStock", wep, Stock);

                player.AfterDelay(100, derp =>
                {
                    if (derp.CurrentWeapon != "none")
                        return;

                    wep = OldWep + att + att3 + att4;
                    player.GiveWeapon(wep);
                    player.Call("switchtoweaponimmediate", wep);
                    player.Call("setWeaponAmmoClip", wep, Clip);
                    player.Call("setWeaponAmmoStock", wep, Stock);

                    derp.AfterDelay(100, e =>
                    {
                        if (derp.CurrentWeapon != "none")
                            return;
                        wep = OldWep;
                        e.GiveWeapon(wep);
                        e.Call("switchtoweaponimmediate", wep);
                        e.Call("setWeaponAmmoClip", wep, Clip);
                        e.Call("setWeaponAmmoStock", wep, Stock);
                    });
                });
            });
        }
        void Thermal(Entity ent)
        {
            string wep = ent.CurrentWeapon;
            if (wep == string.Empty || wep == "none")
                return;
            string OldWep = ent.CurrentWeapon;
            string att = "_thermal";
            string att3 = "_thermal";
            string att4 = "_heartbeat";
            bool SkipCheck = false;
            int Stock = ent.Call<int>("getWeaponAmmoStock", wep);
            int Clip = ent.Call<int>("getWeaponAmmoClip", wep);
            ent.TakeWeapon(wep);
            if (wep.Contains(att))
            {
                int index = wep.IndexOf(att);
                if (index + 9 < wep.Length)
                {
                    char[] derp = wep.ToCharArray(index + 9, (wep.Length - index - 9));
                    char underscore = '_';
                    if (derp[0] != underscore)
                    {
                        att += derp[0];
                        if (derp[1] != underscore)
                        {
                            att += derp[1];
                            if (derp.Length > 2)
                                if (derp[2] != underscore)
                                    GameLog.Write("Error detachting silencer; Current weapon was: " + wep);
                        }
                    }
                }

                wep = wep.Replace(att, string.Empty);
                SkipCheck = true;
            }
            else
            {
                wep = wep + att3 + att4 + att;
            }
            ent.GiveWeapon(wep);
            ent.Call("switchtoweaponimmediate", wep);
            ent.Call("setWeaponAmmoClip", wep, Clip);
            ent.Call("setWeaponAmmoStock", wep, Stock);

            if (SkipCheck)
                return;
            ent.AfterDelay(150, player =>
            {
                if (player.CurrentWeapon != "none")
                    return;

                wep = OldWep + att4 + att + att3;
                player.GiveWeapon(wep);
                player.Call("switchtoweaponimmediate", wep);
                player.Call("setWeaponAmmoClip", wep, Clip);
                player.Call("setWeaponAmmoStock", wep, Stock);

                player.AfterDelay(100, derp =>
                {
                    if (derp.CurrentWeapon != "none")
                        return;

                    wep = OldWep + att + att3 + att4;
                    player.GiveWeapon(wep);
                    player.Call("switchtoweaponimmediate", wep);
                    player.Call("setWeaponAmmoClip", wep, Clip);
                    player.Call("setWeaponAmmoStock", wep, Stock);

                    derp.AfterDelay(100, e =>
                    {
                        if (derp.CurrentWeapon != "none")
                            return;
                        wep = OldWep;
                        e.GiveWeapon(wep);
                        e.Call("switchtoweaponimmediate", wep);
                        e.Call("setWeaponAmmoClip", wep, Clip);
                        e.Call("setWeaponAmmoStock", wep, Stock);
                    });
                });
            });
        }

    }
}

