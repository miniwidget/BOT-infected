﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;

namespace Infected
{
    public partial class Infected
    {
        #region primary
        string getRandomWeapon()
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
            player.AfterDelay(100, x => player.SwitchToWeaponImmediate(weapon));
        }

        int[] WEAPON_INDEX = { 3, 5, 9, 5, 4, 3, 5 };

        string AP() { return AP_LIST[rnd.Next(WEAPON_INDEX[0])] + AP_ATTACHMENT[rnd.Next(2)]; }
        string AG() { return AG_LIST[rnd.Next(WEAPON_INDEX[1])]; }
        string AR() { return AR_LIST[rnd.Next(WEAPON_INDEX[2])] + AR_ATTACHMENT[rnd.Next(3)] + AR_VIEWER[rnd.Next(5)] + CAMO_LIST[rnd.Next(11)]; }
        string SM() { return SM_LIST[rnd.Next(WEAPON_INDEX[3])] + CAMO_LIST[rnd.Next(11)]; }
        string LM() { return LM_LIST[rnd.Next(WEAPON_INDEX[4])] + AR_ATTACHMENT[rnd.Next(3)] + AR_VIEWER[rnd.Next(5)] + CAMO_LIST[rnd.Next(11)]; }
        string SG() { return SG_LIST[rnd.Next(WEAPON_INDEX[5])] + CAMO_LIST[rnd.Next(11)]; }
        string SN() { return SN_LIST[rnd.Next(6)] + SN_ATTACHMENT[rnd.Next(3)] + CAMO_LIST[rnd.Next(11)]; }

        string AP(int i) { if (i > WEAPON_INDEX[0]) i = 0; return AP_LIST[i] + AP_ATTACHMENT[rnd.Next(2)]; }
        string AG(int i) { if (i > WEAPON_INDEX[1]) i = 0; return AG_LIST[i]; }
        string AR(int i) { if (i > WEAPON_INDEX[2]) i = 0; return AR_LIST[i] + AR_ATTACHMENT[rnd.Next(3)] + AR_VIEWER[rnd.Next(1, 6)] + CAMO_LIST[rnd.Next(11)]; }
        string SM(int i) { if (i > WEAPON_INDEX[3]) i = 0; return SM_LIST[i] + CAMO_LIST[rnd.Next(11)]; }
        string LM(int i) { if (i > WEAPON_INDEX[4]) i = 0; return LM_LIST[i] + AR_ATTACHMENT[rnd.Next(3)] + AR_VIEWER[rnd.Next(1, 6)] + CAMO_LIST[rnd.Next(11)]; }
        string SG(int i) { if (i > WEAPON_INDEX[5]) i = 0; return SG_LIST[i] + CAMO_LIST[rnd.Next(11)]; }
        string SN(int i) { if (i > WEAPON_INDEX[6]) i = 0; return SN_LIST[i] + SN_ATTACHMENT[rnd.Next(3)] + CAMO_LIST[rnd.Next(11)]; }

        #endregion

        #region weapon list
        string[] AP_LIST = new[] { "iw5_fmg9_mp_akimbo", "iw5_skorpion_mp_akimbo", "iw5_mp9_mp_akimbo", "iw5_g18_mp_akimbo", };
        string[] AG_LIST = new[] { "iw5_mp412_mp_akimbo", "iw5_p99_mp_akimbo", "iw5_44magnum_mp_akimbo", "iw5_usp45_mp_akimbo", "iw5_fnfiveseven_mp_akimbo", "iw5_deserteagle_mp_akimbo" };
        string[] AR_LIST = new[] { "iw5_ak47_mp_gp25", "iw5_m16_mp_gl", "iw5_m4_mp_gl", "iw5_fad_mp_m320", "iw5_acr_mp_m320", "iw5_type95_mp_m320", "iw5_mk14_mp_m320", "iw5_scar_mp_m320", "iw5_g36c_mp_m320", "iw5_cm901_mp_m320", };
        string[] SM_LIST = new[] { "iw5_mp5_mp_hamrhybrid_rof_silencer", "iw5_m9_mp_hamrhybrid_rof_silencer", "iw5_p90_mp_hamrhybrid_rof_silencer", "iw5_pp90m1_mp_hamrhybrid_rof_silencer", "iw5_ump45_mp_hamrhybrid_rof_silencer", "iw5_mp7_mp_rof_silencer_hamrhybrid", };
        string[] LM_LIST = new[] { "iw5_m60_mp_grip", "iw5_mk46_mp_grip", "iw5_pecheneg_mp_grip", "iw5_sa80_mp_grip", "iw5_mg36_mp_grip" };
        string[] SG_LIST = new[] { "iw5_spas12_mp", "iw5_aa12_mp", "iw5_striker_mp", "iw5_1887_mp", "iw5_usas12_mp", };
        string[] SN_LIST = new[] { "iw5_dragunov_mp_dragunovscopevz_xmags", "iw5_msr_mp_msrscopevz_xmags", "iw5_barrett_mp_barrettscopevz_xmags", "iw5_rsass_mp_rsassscopevz_xmags", "iw5_as50_mp_as50scopevz_xmags", "iw5_l96a1_mp_l96a1scopevz_xmags", };
        string[] LAUNCHER_LIST = new[] { "stinger_mp", "m320_mp", "xm25_mp", "javelin_mp", };

        string[] CAMO_LIST = new[] { "_camo01", "_camo02", "_camo03", "_camo04", "_camo05", "_camo06", "_camo07", "_camo08", "_camo09", "_camo10", "_camo11" };
        string[] AR_ATTACHMENT = new[] { "_silencer", "_heartbeat", "" };
        string[] AP_ATTACHMENT = new[] { "_silencer02", "" };
        string[] SN_ATTACHMENT = new[] { "_silencer03", "_heartbeat", "" };
        string[] AR_VIEWER = { "_acog", "_thermal", "_reflex", "_eotech", "" };

        string[] G_AR = { "ak47", "m16", "m4", "fad", "acr", "type95", "mk14", "scar", "g36c", "cm901" };
        string[] G_LM = { "m60", "mk46", "pecheneg", "sa80", "mg36" };
        string[] G_SN = { "dragunov", "msr", "barrett", "rsass", "as50", "l96a1", };

        List<string> ATT_Lists = new List<string>() { "_rof", "_acog", "_thermal", "_reflex", "_eotech" };
        List<H_SET> H_FIELD = new List<H_SET>();
        //string[] G_PISTOL = { "fmg9", "skorpion", "mp9", "g18" };
        //string[] G_GUN = { "mp412", "p99", "44magnum", "usp45", "fnfiveseven", "deserteagle" };
        //string[] G_SM = { "mp5", "m9", "p90", "pp90m1", "ump45", "mp7", };
        //string[] G_SG = { "spas12", "aa12", "striker", "1887", "usas12" };
        #endregion
        #region offhand
        void giveOffhandWeapon(Entity player, string weapon)
        {

            switch (weapon)
            {
                case "throwingknife_mp":
                    player.Call("SetOffhandPrimaryClass", "throwingknife");
                    player.GiveWeapon(weapon);
                    player.Call("setweaponammoclip", weapon, 1);

                    break;
                case "c4_mp":
                    player.Call("SetOffhandPrimaryClass", "c4");
                    player.Call("giveweapon", weapon);
                    player.Call("setweaponammoclip", "c4_mp", 2);
                    player.SwitchToWeaponImmediate(weapon);
                    player.Call("iprintlnbold", "^2[ ^7DEPLOY ^2| ^7FIRE ^2] RIGHT  LEFT^7Mouse botton");
                    break;

                case "frag_grenade_mp":
                    player.GiveWeapon(weapon);
                    player.SwitchToWeapon(weapon);
                    player.Call("iprintlnbold", "^2[ ^7THROW ^2] ^2LEFT^7Mouse botton");
                    break;

                case "semtex_mp":
                    player.GiveWeapon(weapon);
                    break;

                case "bouncingbetty_mp":
                    player.GiveWeapon(weapon);
                    player.Call("setweaponammoclip", weapon, 1);
                    break;

                case "claymore_mp":
                    player.GiveWeapon(weapon);
                    player.Call("setweaponammoclip", weapon, 1);
                    break;
            }
        }
        #endregion

        #region attachment viewscope
        string getWeaponType(string s)
        {
            string name = s.Split('_')[1];

            if (G_AR.Contains(name)) return "ar";
            if (G_LM.Contains(name)) return "lm";
            if (G_SN.Contains(name)) return "sn";
            //if (G_SM.Contains(name)) return "sg";
            //if (G_SG.Contains(name)) return "sg";
            //if (G_PISTOL.Contains(name)) return "ap";
            //if (G_GUN.Contains(name)) return "ag";

            return null;
        }

        void giveAttachScope(Entity player)
        {

            try
            {
                string CW = player.CurrentWeapon;
                var type = getWeaponType(CW);
                if (type == null || type == "sn")
                {
                    showMessage(player, "^2NOT APPLIED ^7FOR THIS WEAPON");
                    return;
                }

                string NEW_WEAP = null;
                int insertIdx = CW.IndexOf("mp") + 2;
                string sub = CW.Substring(insertIdx);

                int idx = 0;
                bool found = false;
                foreach (string s in ATT_Lists)//view 모두 삭제
                {
                    if (sub.Contains(s))
                    {
                        idx = ATT_Lists.IndexOf(s) + 1;
                        if (type == "ar")
                        {
                            if (idx == 5) idx = 0;
                        }
                        else
                        {
                            if (idx == 3) idx = 0;
                        }

                        NEW_WEAP = CW.Replace(s, ATT_Lists[idx]);
                        //print(NEW_WEAP);
                        found = true;
                        break;
                    }
                }
                //_X "_acog", "_thermal", "_reflex", "_eotech" 

                if (!found)
                {
                    NEW_WEAP = CW.Insert(insertIdx, "_acog");
                }
                giveWeaponTo(player, NEW_WEAP);
            }
            catch
            {

            }
        }
        #endregion

        #region attachment hertbeat
        void giveAttachHeartbeat(Entity player,int num)
        {
            try
            {
                string CW = player.CurrentWeapon;
                var type = getWeaponType(CW);
                if (type == null) return;

                string w = null;

                if (type == "ar" || type == "lm" || type == "sn")
                {
                    H_SET att = H_FIELD[num];
                    int i = att.SIRENCERorHB;

                    string hb = "_heartbeat";
                    string sl = "_silencer";
                    if (type == "sn") sl = "_silencer03";

                    w = CW.Replace(sl, "");
                    w = w.Replace(hb, "");

                    if (i == 1)//heartbeat
                    {
                        w = w.Insert(w.IndexOf("_camo"), hb);
                    }
                    else if (i == 2)//silencer
                    {
                        w = w.Insert(w.IndexOf("_camo"), sl);
                    }

                    giveWeaponTo(player, w);
                }
                else
                {
                    showMessage(player, "^2NOT APPLIED ^7FOR THIS WEAPON");
                }

            }
            catch
            {

            }
        }
        #endregion
    }
}

