using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;

namespace Infected
{
    public class SHOWINFO
    {
        public static void ShowInfoA(Entity ent)
        {
            if (ent.GetField<string>("sessionteam") != "axis")
                goto Survivors;

            ent.Call("iPrintlnBold", "TYPE  FOLLOWING");
            ent.AfterDelay(4000, Ent =>
            {
                Ent.Call("iPrintlnBold", "^2[ ^7LOC ^2]  TO LOCATE INITIAL POSITION");
                Ent.AfterDelay(4000, Ent2 =>
                {
                    Ent2.Call("iPrintlnBold", "^2[ ^7SC ^2]  TO SUICIDE");
                   
                });

            });

            return;
        Survivors:

            ent.Call("iPrintlnBold", "ATTACHMENT INFORMATION");
            ent.AfterDelay(4000, entity =>
            {
                entity.Call("iPrintlnBold", "^7BIND FOLLOWING KEYS  IF SHOW ^2UNBOUND");
                entity.AfterDelay(4000, ENt =>
                {
                    ENt.Call("iPrintlnBold", "OPTIONS -> CONTROLS->MOVEMENT->^2HOLDPRONE");
                    ENt.AfterDelay(4000, player =>
                    {
                        player.Call("iPrintlnBold", "OPTIONS -> CONTROLS->MOVEMENT->^2CHANGESTANCE");
                        player.AfterDelay(4000, _Ent =>
                        {
                            _Ent.Call("iPrintlnBold", "^2[ ^7[{+prone}] ^2]  SILENCER OR THERMAL");
                            _Ent.AfterDelay(4000, _Ent2 =>
                            {
                                _Ent2.Call("iPrintlnBold", "^2[ ^7[{+stance}] ^2]  C4  |  FRAG  |  SEMTEX");
                                _Ent2.AfterDelay(4000, _Ent3 =>
                                {
                                    _Ent3.Call("iPrintlnBold", "^2[ ^7LOC ^2]  TO LOCATE INITIAL POSITION");
                                    
                                });
                            });
                        });
                    });
                });
            });
        }
        public static void ShowInfoW(Entity ent)
        {
            if (ent.GetField<string>("sessionteam") != "axis")
                goto Survivors;

            ent.Call("iPrintlnBold", "TYPE  FOLLOWING");
            ent.AfterDelay(4000, Ent =>
            {
                Ent.Call("iPrintlnBold", "^2[ ^7RIOT ^2] TO GET RIOTSHIELD");
                Ent.AfterDelay(4000, Ent2 =>
                {
                    Ent2.Call("iPrintlnBold", "^2[ ^7STINGER ^2] TO GET STINGER");
                    Ent2.AfterDelay(4000, Ent3 =>
                    {
                        Ent3.Call("iPrintlnBold", "STINGER CAN BE USED IN 6 SECONDS");
                        Ent3.AfterDelay(4000, Ent4 =>
                        {
                            Ent4.Call("iPrintlnBold", "^7BIND FOLLOWING KEYS  IF SHOW ^2UNBOUND");
                            Ent4.AfterDelay(4000, Ent5 =>
                            {
                                Ent5.Call("iPrintlnBold", "OPTIONS -> CONTROLS->MOVEMENT->^2CHANGESTANCE");
                                Ent5.AfterDelay(4000, Ent6 =>
                                {
                                    Ent6.Call("iPrintlnBold", "OPTIONS -> CONTROLS->MOVEMENT->^2HOLDPRONE");
                                    Ent5.AfterDelay(4000, Ent7 =>
                                    {
                                        Ent7.Call("iPrintlnBold", "^2[ ^7[{+stance}] ^2] TO GET BETTY , CLAYMORE");
                                        Ent7.AfterDelay(4000, Ent8 =>
                                        {
                                            Ent8.Call("iPrintlnBold", "^2[ ^7[{+prone}] ^2] TO GET THROWING KNIFE");

                                        });
                                    });
                                });
                            });
                        });
                    });
                });

            });

            return;
        Survivors:
            ent.Call("iPrintlnBold", "WEAPON  INFORMATION");
            ent.AfterDelay(4000, entity =>
            {
                entity.Call("iPrintlnBold", "^7TYPE FOLLOWING TO GET WEAPONS");
                entity.AfterDelay(4000, ENt =>
                {
                    ENt.Call("iPrintlnBold", "^2AP ^7| ^2AK ^7| ^2AR ^7| ^2SM ^7| ^2MG ^7| ^2SN");//| holdstrafe | changestance ]");

                    ENt.AfterDelay(4000, player =>
                    {
                        player.Call("iPrintlnBold", "^2[ ^7AP ^2] TO GET AUTOMATIC PISTOL");

                        player.AfterDelay(4000, _Ent =>
                        {
                            _Ent.Call("iPrintlnBold", "^2[ ^7AK ^2] TO GET AKIMBO PISTOL");

                            _Ent.AfterDelay(4000, _Ent2 =>
                            {
                                _Ent2.Call("iPrintlnBold", "^2[ ^7AR ^2] TO GET ASSAULT RIFFLE");

                                _Ent2.AfterDelay(4000, _Ent3 =>
                                {
                                    _Ent3.Call("iPrintlnBold", "^2[ ^7SM ^2] TO GET SUB MACHINE GUN");
                                    _Ent3.AfterDelay(4000, _Ent4 =>
                                    {
                                        _Ent4.Call("iPrintlnBold", "^2[ ^7LM ^2] TO GET LIGHT MACHINE GUN");
                                        _Ent4.AfterDelay(4000, _Ent5 =>
                                        {
                                            _Ent5.Call("iPrintlnBold", "^2[ ^7SN ^2] TO GET SNIPE GUN");
                                            _Ent5.AfterDelay(4000, _Ent6 =>
                                            {
                                                _Ent6.Call("iPrintlnBold", "REMAININGS ARE AT RIGHT GUIDE MENU");

                                            });
                                        });
                                    });
                                });
                            });

                        });
                    });
                });
            });
        }
    }
}

