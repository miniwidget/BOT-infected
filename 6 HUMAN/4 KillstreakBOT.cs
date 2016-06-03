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
        //헬리콥터 봇
        #region 헬리콥터 봇
        void spawnd_bot_heli(Entity player)
        {
        }
        void helicopter(Entity player)
        {

        }
        #endregion
        //참조 http://pastebin.com/c2xDcfGd
        // http://pastebin.com/Bb1mqEua
        // http://pastebin.com/aP0caf99

        Entity VEHICLE;
        Vector3[] TAG_ORIGIN = new Vector3[] { new Vector3(0, 0, 200), new Vector3(0, 0, 0) };

        void spawnHarrir()
        {
                Entity bot = BOTs_List[2];
                //"spawnplane"
                if (VEHICLE != null) VEHICLE.Call(32928);
                var o = bot.Origin; o.Z += 1000;
                string realModel = "harrier_mp";
                string minimap_model = "vehicle_av8b_harrier_jet_mp";
                VEHICLE = Call<Entity>("spawnhelicopter", bot, o, bot.GetField<Vector3>("angles"), minimap_model, realModel);
                bot.Call("remotecontrolvehicle", VEHICLE);//remotecontrolvehicle
                bot.Call("linkto", VEHICLE, "tag_origin", TAG_ORIGIN[0], TAG_ORIGIN[1]);
        }

        void rideHeli()
        {
                Entity bot = BOTs_List[2];
                if (VEHICLE != null) VEHICLE.Call(32928);
                var o = bot.Origin; o.Z += 1000;
                string realModel = null;
                string minimap_model = null;

                switch (rnd.Next(4))
                {
                    case 0: minimap_model = "cobra_mp"; realModel = "vehicle_apache_mp"; break;
                    case 1: minimap_model = "cobra_mp"; realModel = "vehicle_cobra_helicopter_fly_low"; break;
                    case 2: minimap_model = "attack_littlebird_mp"; realModel = "vehicle_little_bird_armed"; break;
                    case 3: minimap_model = "pavelow_mp"; realModel = "vehicle_pavelow"; break;
                }
                VEHICLE = Call<Entity>("spawnhelicopter", bot, o, bot.GetField<Vector3>("angles"), minimap_model, realModel);
                bot.Call("remotecontrolvehicle", VEHICLE);//remotecontrolvehicle
                bot.Call("linkto", VEHICLE, "tag_origin", TAG_ORIGIN[0], TAG_ORIGIN[1]);

            return;
        }

        void moveBot(string name)
        {
            if (name == null) name = "bot";
            foreach (var bot in BOTs_List)
            {
                if (bot.Name.Contains(name))
                {
                    bot.Call("setorigin", ADMIN.Origin);
                    break;
                }
            }
        }
        void hideBot(string name)
        {
            if (name == null) name = "bot";
            foreach (var bot in BOTs_List)
            {
                if (bot.Name.Contains(name))
                {
                    bot.Call("setorigin", ADMIN.Origin);
                    AfterDelay(t2, () =>
                    {
                        bot.Call("hide");
                        AfterDelay(t2, () =>
                        {
                            bot.Call("show");
                        });
                    });
                    break;
                }
            }
        }

    }
}
