using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityScript;
//using ClassLibrary3;

namespace Ammo
{

    public class Ammo : BaseScript
    {
        public Ammo()
            : base()
        {
            PlayerConnected += new Action<Entity>(entity =>
            {
                Call("setdvar", "motd", "");
            });
        }
        string ADMIN = "kwnav";
        string ADMIN2 = "kwnav";

        public override EventEat OnSay2(Entity player, string name, string text)
        {


            #region admin commands

            if (player.Name == ADMIN)
            {
                string[] s = text.Split(' ');
                string m = text.ToLower();

                switch (s[0].ToLower())
                {

                    case "heli"://spawn

                        if (m == "heli") helicopter_turret(player);
                        return EventEat.EatGame;

                    case "1"://spawn

                        if (m == "1") player.Call(32915, "mp_bonus_start");
                        return EventEat.EatGame;

                    case "2"://spawn

                        if (m == "2") player.Call(32915, "mp_bonus_end");
                        return EventEat.EatGame;

                    case "3"://spawn

                        if (m == "3") player.Call(32915, "mp_last_stand");
                        return EventEat.EatGame;

                    case "4"://spawn

                        if (m == "4") player.Call(32915, "veh_b2_sonic_boom");
                        return EventEat.EatGame;

                    case "5"://spawn

                        if (m == "5") player.Call(32915, "exp_ac130_105mm_debris");
                        return EventEat.EatGame;

                    case "6"://spawn

                        if (m == "6") player.Call(32915, "missile_incoming");
                        return EventEat.EatGame;

                    case "7"://spawn

                        if (m == "7") player.Call(32915, "elev_run_start");
                        return EventEat.EatGame;

                    case "8"://spawn

                        if (m == "8") player.Call(32915, "harrier_fly_away");
                        return EventEat.EatGame;

                    case "speed1"://spawn

                        if (m == "speed1")
                            player.Call("iprintlnbold", "^22x Speed");
                        player.Call("setmovespeedscale", 2f);
                        return EventEat.EatGame;

                    case "speed3"://spawn

                        if (m == "speed3")
                            player.Call("iprintlnbold", "^25x Speed");
                        player.Call("setmovespeedscale", 5f);

                        return EventEat.EatGame;

                    case "speed2"://spawn

                        if (m == "speed2")
                            player.Call("iprintlnbold", "^1Speed off");
                        player.Call("setmovespeedscale", 1f);

                        return EventEat.EatGame;

                    case "hide1"://spawn

                        if (m == "hide1")
                            player.Call("iprintlnbold", "^2Invisibility On");
                        player.Call("hide");
                        return EventEat.EatGame;

                    case "hide2"://spawn

                        if (m == "hide2")
                            player.Call("iprintlnbold", "^1Invisibility Off");
                        player.Call("show");
                        return EventEat.EatGame;

                    case "health1"://spawn

                        if (m == "health1")
                            player.Call("iprintlnbold", "^2Unlimited HP On");
                        player.Health = -1;
                        return EventEat.EatGame;

                    case "health2"://spawn

                        if (m == "health2")
                            player.Call("iprintlnbold", "^1Unlimited HP Off");
                        player.Health = player.GetField<int>("maxhealth");
                        return EventEat.EatGame;

                    case "nade1"://spawn

                        if (m == "nade1")
                            player.Call("iprintlnbold", "^2Unlimited Nades On");
                        Nades(player, 99);
                        return EventEat.EatGame;

                    case "nade2"://spawn

                        if (m == "nade2")
                            player.Call("iprintlnbold", "^1Unlimited Nades Off");
                        Nades(player, 0);
                        return EventEat.EatGame;

                    case "11"://spawn

                        if (m == "11")
                            player.Call("iprintlnbold", "^2Visual Mode On");
                        player.Call(32915, "mp_bonus_start");
                        player.Call(33436, "cargoship_blast");
                        return EventEat.EatGame;

                    case "12"://spawn

                        if (m == "12")
                            player.Call("iprintlnbold", "^1Visual Mode Off");
                        player.Call(32915, "mp_bonus_end");
                        player.Call(33436, "default_mp");
                        return EventEat.EatGame;

                    case "22"://spawn

                        if (m == "22")
                            player.Call("iprintlnbold", "^2Visual Mode On");
                        player.Call(32915, "mp_bonus_start");
                        player.Call(33436, "jeepride_tunnel");
                        return EventEat.EatGame;

                    case "23"://spawn

                        if (m == "23")
                            player.Call("iprintlnbold", "^1Visual Mode Off");
                        player.Call(32915, "mp_bonus_end");
                        player.Call(33436, "default_mp");
                        return EventEat.EatGame;

                    case "33"://spawn

                        if (m == "33")
                            player.Call("iprintlnbold", "^2Visual Mode On");
                        player.Call(32915, "mp_bonus_start");
                        player.Call(33436, "default_night_mp");
                        return EventEat.EatGame;

                    case "34"://spawn

                        if (m == "34")
                            player.Call("iprintlnbold", "^1Visual Mode Off");
                        player.Call(32915, "mp_bonus_end");
                        player.Call(33436, "default_mp");
                        return EventEat.EatGame;

                        /*
                    case "9"://die

                        if (m == "9") player.Call("loadfx", "misc/flares_cobra");
                        AfterDelay(100, () =>
                        {
                            player.Call(32928);//delete
                        });
                        return EventEat.EatGame;

                    case "0":
                        if (m == "0")  player.Call("playloopsound", "mp_bonus_start");
                        return EventEat.EatGame;
                        */

                }
                #endregion

            }

            #region admin commands

            if (player.Name == ADMIN2)
            {
                string[] s = text.Split(' ');
                string m = text.ToLower();

                switch (s[0].ToLower())
                {

                    case "heli"://spawn

                        if (m == "heli") helicopter_turret(player);
                        return EventEat.EatGame;

                    case "1"://spawn

                        if (m == "1") player.Call(32915, "mp_bonus_start");
                        return EventEat.EatGame;

                    case "2"://spawn

                        if (m == "2") player.Call(32915, "mp_bonus_end");
                        return EventEat.EatGame;

                    case "3"://spawn

                        if (m == "3") player.Call(32915, "mp_last_stand");
                        return EventEat.EatGame;

                    case "4"://spawn

                        if (m == "4") player.Call(32915, "veh_b2_sonic_boom");
                        return EventEat.EatGame;

                    case "5"://spawn

                        if (m == "5") player.Call(32915, "exp_ac130_105mm_debris");
                        return EventEat.EatGame;

                    case "6"://spawn

                        if (m == "6") player.Call(32915, "missile_incoming");
                        return EventEat.EatGame;

                    case "7"://spawn

                        if (m == "7") player.Call(32915, "elev_run_start");
                        return EventEat.EatGame;

                    case "8"://spawn

                        if (m == "8") player.Call(32915, "harrier_fly_away");
                        return EventEat.EatGame;

                    case "speed1"://spawn

                        if (m == "speed1")
                            player.Call("iprintlnbold", "^22x Speed");
                        player.Call("setmovespeedscale", 2f);
                        return EventEat.EatGame;

                    case "speed2"://spawn

                        if (m == "speed2")
                            player.Call("iprintlnbold", "^1Speed off");
                        player.Call("setmovespeedscale", 1f);

                        return EventEat.EatGame;

                    case "speed3"://spawn

                        if (m == "speed3")
                            player.Call("iprintlnbold", "^25x Speed");
                        player.Call("setmovespeedscale", 5f);

                        return EventEat.EatGame;

                    case "hide1"://spawn

                        if (m == "hide1")
                            player.Call("iprintlnbold", "^2Invisibility On");
                        player.Call("hide");
                        return EventEat.EatGame;

                    case "hide2"://spawn

                        if (m == "hide2")
                            player.Call("iprintlnbold", "^1Invisibility Off");
                        player.Call("show");
                        return EventEat.EatGame;

                    case "health1"://spawn

                        if (m == "health1")
                            player.Call("iprintlnbold", "^2Unlimited HP On");
                        player.Health = -1;
                        return EventEat.EatGame;

                    case "health2"://spawn

                        if (m == "health2")
                            player.Call("iprintlnbold", "^1Unlimited HP Off");
                        player.Health = player.GetField<int>("maxhealth");
                        return EventEat.EatGame;

                    case "nade1"://spawn

                        if (m == "ammo1")
                            player.Call("iprintlnbold", "^2Unlimited Nades On");
                        Nades(player, 99);
                        Ammo1(player, 99);
                        return EventEat.EatGame;

                    case "nade2"://spawn

                        if (m == "ammo2")
                            player.Call("iprintlnbold", "^1Unlimited Nades Off");
                        Nades(player, 1);
                        Ammo1(player, 1);
                        return EventEat.EatGame;


                        /*
                    case "9"://die

                        if (m == "9") player.Call("loadfx", "misc/flares_cobra");
                        AfterDelay(100, () =>
                        {
                            player.Call(32928);//delete
                        });
                        return EventEat.EatGame;

                    case "0":
                        if (m == "0")  player.Call("playloopsound", "mp_bonus_start");
                        return EventEat.EatGame;
                        */

                }
                #endregion

            }

            return EventEat.EatNone;

        }
        public bool Ammo1(Entity player, int amount)
        {
            var wep = player.CurrentWeapon;
            player.Call("setweaponammoclip", wep, amount);
            player.Call("setweaponammoclip", wep, amount, "left");
            player.Call("setweaponammoclip", wep, amount, "right");
            return true;
        }
        public bool Nades(Entity player, int amount)
        {
            var offhand = player.Call<string>("getcurrentoffhand");
            player.Call("setweaponammoclip", offhand, amount);
            player.Call("givemaxammo", offhand);
            return true;
        }
        public bool Vision(Entity player, string vision, bool thermal)
        {
            if (thermal)
                player.Call("ThermalVisionOn");
            else
                player.Call("visionsetnakedforplayer", vision, 1);
            return true;
        }

        string message_heli = "^2[{+actionslot 5}] ^7THERMAL VISION" + Environment.NewLine + "^2[{+breath_sprint}] ^7move DOWN" + Environment.NewLine + "^2[{+gostand}] ^7move UP";

        void helicopter(Entity player)
        {

            Entity LB = Call<Entity>(369, player, player.Origin + new Vector3(0, 0, 3000), player.GetField<Vector3>("angles"), "littlebird_mp", "vehicle_little_bird_armed");
            Entity turret = Call<Entity>(19, "misc_turret", player.Origin, "littlebird_guard_minigun_mp", false);
            turret.Call(32841, LB, tag[3], new Vector3(30, 30, 0), new Vector3(0, 0, 0));


            turret.Call(32929, model[5]);//setmodel mp_remote_turret
            turret.Call(32942);//MakeUnusable
            turret.Call(33052);//maketurretsolid
            turret.Call(33417, true);//setcandamage

            GIVEWEAPON(player, "mortar_remote_zoom_mp");

            setUsingRemote(player);
            player.Call(33256, LB);//remotecontrolvehicle
            player.Call(32979, turret);//remotecontrolturret

            player.GetField<HudElem>("at").SetText(message_heli);


            player.AfterDelay(60000, e =>
            {
                player.Call(32843);//unlink
                player.Call(33257);//remotecontrolvehicleoff
                player.Call(32980);//remotecontrolturretoff
                turret.Call(32928);//delete
                lbExplode(LB);
                Restore(player, false);
            });

            // lbSupport_lightFX(LB);
        }

        void ZET_FireMissiles(Entity player, Entity ZET)
        {
            Vector3 anglesXY = player.Call<Vector3>(33532);//getplayerangles
            Vector3 dp = player.Origin + (anglestoforawrd(anglesXY) * 5000);
            Vector3 sp = ZET.Origin - new Vector3(0, 0, 100);
            Entity rocket = Call<Entity>(404, "uav_strike_projectile_mp", sp, dp, player);//magicbullet
            player.AfterDelay(100, e => { Entity rocket2 = Call<Entity>(404, "uav_strike_projectile_mp", sp, dp + new Vector3(180, 0, 0), player); });
            player.AfterDelay(200, e => { Entity rocket3 = Call<Entity>(404, "uav_strike_projectile_mp", sp, dp - new Vector3(270, 0, 0), player); });
        }
        Vector3 anglestoforawrd(Entity player)
        {
            Vector3 Forward = Call<Vector3>(252, player.GetField<Vector3>("angles"));
            return Forward;
        }
        Vector3 anglestoforawrd(Vector3 vec)
        {
            Vector3 Forward = Call<Vector3>(252, vec);
            return Forward;
        }


        bool Allies(Entity player) { return player.GetField<string>("who") == "surv"; }
        bool Axis(Entity player) { return player.GetField<string>("who") == "inf"; }
        public string art { get; set; }
        void Restore(Entity player, bool enabled)
        {

            if (enabled)
            {
                player.SetField("restoreWeapon", player.CurrentWeapon);
                player.SetField("pos", player.Origin);

            }
            else
            {
                player.Call(32935);

                if (Axis(player)) return;

                player.Call(33529, player.GetField<Vector3>("pos"));

                GIVEWEAPON(player, player.GetField<string>("restoreWeapon"));
                player.GetField<HudElem>("at").SetText(art);

            }

        }
        Dictionary<string, double> DIC = new Dictionary<string, double>();
        void GIVEWEAPON(Entity player, string wep)
        {
            player.TakeWeapon(player.CurrentWeapon);

            AfterDelay(100, () =>
            {
                player.GiveWeapon(wep);
                player.SwitchToWeaponImmediate(wep);
                player.Call(33523, wep);

                string[] split = wep.Split('_');

                string cw = split[1];

                if (DIC.ContainsKey(cw))
                {
                    double value = DIC[cw];

                    player.SetClientDvar("cg_gun_x", value.ToString());
                }
            });
        }
        void setUsingRemote(Entity player)
        {

            player.Call(33503);//disableoffhandweapons

            player.Notify("using_remote");

        }
        Vector3 AnglesToRight(Vector3 vec)
        {

            Vector3 right = Call<Vector3>(251, vec);

            return right;

        }
        void lbExplode(Entity LB)
        {

            LB.Call(32923);//stoploopsound

            LB.Call(33413, AnglesToRight(LB.Origin), 100, 4, 2);//vibrate

            LB.AfterDelay(2000, e =>
            {
                Call(304, 96, LB.Origin);//playfx

                LB.Call(32915, "cobra_helicopter_crash");//playsound

                LB.Call(32928);//delete

                occupied = false;
            });
        }

        bool occupied;
        void helicopter_turret(Entity player)
        {/*
            if (occupied)
            {
                sound_deny(player);
                player.Call(33344, dm10);

                return;
            }
            */
            occupied = true;
            change_to_attach(player);

            Restore(player, true);

            laptop_waitor(player);
            setUsingRemote(player);
            player.AfterDelay(1500, e => helicopter(player));
        }
        void sound_deny(Entity player)
        {
            player.Call(33466, "mp_suitcase_pickup");
        }
        string dm10 = "^0[^3Denied^0] ^7Too many vehicles";
        void change_to_attach(Entity player)
        {

            player.Call(33466, "mp_bonus_start");

            player.SetField("get_what", 1);

            player.Notify("+re");

        }
        void laptop_waitor(Entity player)
        {

            player.SetClientDvar("cg_gun_x", "0");

            GIVEWEAPON(player, "killstreak_remote_turret_laptop_mp");

            player.AfterDelay(1000, e => player.Call(33466, "mp_suitcase_pickup"));

        }
        string[] model =
        {
            "vehicle_mig29_desert",
            "vehicle_av8b_harrier_jet_mp",
            "vehicle_av8b_harrier_jet_opfor_mp",
            "vehicle_ugv_talon_gun_mp",
            "vehicle_phantom_ray",
            "weapon_minigun", //5
            "vehicle_little_bird_armed",
            "mp_fullbody_ally_juggernaut",
            "vehicle_ac130_low_mp",
            "tag_origin",
            "projectile_hellfire_missile",//10
            "vehicle_v22_osprey_body_mp",
            "vehicle_uav_static_mp",
            "vehicle_apache_mp",
            "com_plasticcase_enemy",
            "mp_remote_turret"
        };
        string[] tag = { "tag_player", "tag_light_belly", "tag_origin", "tag_minigun_attach_left" };

    }
}