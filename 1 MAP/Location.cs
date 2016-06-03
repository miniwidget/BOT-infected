using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;

namespace Infected
{
    public class L //Location
    {
        static Random rnd = Infected.rnd;
        static Parameter p1, p2, p3, p4, i1;
        static bool setParam(string m)
        {
            switch (m)
            {
                case "mp_alpha":
                    i1 = new Parameter(new Vector3(-1472.533f, 3557.656f, 138.125f));
                    break;
                case "mp_boardwalk":
                    i1 = new Parameter(new Vector3(-1007.327f, 3720.143f, 127.125f));
                    break;
                case "mp_bootleg":
                    p1 = new Parameter(new Vector3(-705.4134f, -76.42394f, 112.1544f));
                    p2 = new Parameter(new Vector3(-665.0373f, -238.3017f, 95.92446f));
                    p3 = new Parameter(new Vector3(-88.82451f, -1795.069f, 0.1249f));
                    p4 = new Parameter(new Vector3(-217.319f, -1759.981f, 0.0401f));
                    i1 = new Parameter(new Vector3(-301.0647f, -79.91943f, -67.875f));
                    break;
                case "mp_bravo":
                    i1 = new Parameter(new Vector3(-1853.57f, 668.0859f, 1060.652f));
                    break;
                case "mp_carbon":
                    i1 = new Parameter(new Vector3(-1581.039f, -4353.35f, 3861.876f));
                    break;
                case "mp_cement":
                    i1 = new Parameter(new Vector3(-4057.628f, -61.3646f, 296.125f));
                    break;

                case "mp_dome":
                    p1 = new Parameter(new Vector3(1218.14f, -81.6358f, -395.0756f));
                    p2 = new Parameter(new Vector3(-732.5061f, 140.6772f, -416.937f));
                    p3 = new Parameter(new Vector3(-351.4386f, 1334.207f, -287.9406f));
                    p4 = new Parameter(new Vector3(-317.2463f, 1536.084f, -281.9745f));
                    i1 = new Parameter(new Vector3(170.4399f, -59.56112f, 6543.455f));
                    break;
                case "mp_exchange":
                    p1 = new Parameter(new Vector3(2552.403f, 1229.354f, 79.74294f));
                    p2 = new Parameter(new Vector3(1260.27f, 1280.783f, 58.59225f));
                    p3 = new Parameter(new Vector3(-28.25132f, 1053.669f, 80.34425f));
                    p4 = new Parameter(new Vector3(-1200.114f, 1155.459f, 65.12894f));
                    i1 = new Parameter(new Vector3(-1711.739f, 2216.595f, 2327.625f));
                    break;
                case "mp_hardhat":
                    p1 = new Parameter(new Vector3(104.7347f, 826.6396f, 436.125f));
                    p2 = new Parameter(new Vector3(1468.704f, 1216.154f, 371.125f));
                    p3 = new Parameter(new Vector3(540.8942f, 2877.293f, 135.814f));
                    p4 = new Parameter(new Vector3(1499.658f, 1973.991f, 646.125f));
                    i1 = new Parameter(new Vector3(-985.4795f, -986.3607f, 290.9416f));
                    break;
                case "mp_interchange":
                    p1 = new Parameter(new Vector3(2867.118f, -2624.666f, 281.3506f));
                    p2 = new Parameter(new Vector3(4550.596f, -3622.802f, 391.425f));
                    p3 = new Parameter(new Vector3(7042.998f, -3362.816f, 420.4325f));
                    p4 = new Parameter(new Vector3(5449.588f, -4839.432f, 491.8913f));
                    i1 = new Parameter(new Vector3(5025.027f, -3411.375f, 381.2672f));
                    break;
                case "mp_italy":
                    i1 = new Parameter(new Vector3(1105.824f, 1926.78f, 1215.081f));
                    break;

                case "mp_lambeth":
                    p1 = new Parameter(new Vector3(223.0231f, 2724.446f, -241.7563f));
                    p2 = new Parameter(new Vector3(1284.009f, 2620.539f, -273.3667f));
                    p3 = new Parameter(new Vector3(2836.841f, 2470.033f, -214.637f));
                    p4 = new Parameter(new Vector3(2043.692f, 738.6119f, -308.0637f));
                    i1 = new Parameter(new Vector3(-840.1872f, -57.74454f, -73.875f));
                    break;
                case "mp_meteora":
                    i1 = new Parameter(new Vector3(-2231.761f, 2036.846f, 1552.125f));
                    break;
                case "mp_moab":
                    i1 = new Parameter(new Vector3(1113.51f, 1908.329f, 170.0522f));
                    break;

                case "mp_mogadishu":
                    p1 = new Parameter(new Vector3(-301.5547f, 3194.984f, 92.125f));
                    p2 = new Parameter(new Vector3(585.5217f, 3209.992f, 88.125f));
                    p3 = new Parameter(new Vector3(839.4898f, 3149.931f, 88.64301f));
                    p4 = new Parameter(new Vector3(-593.6973f, 1668.741f, -19.33768f));
                    i1 = new Parameter(new Vector3(1417.62f, -1315.583f, -45.09239f));
                    break;
                case "mp_morningwood":
                    i1 = new Parameter(new Vector3(-257.4955f, 428.5023f, 1091.143f));
                    break;
                case "mp_nola":
                    i1 = new Parameter(new Vector3(-1900.517f, -188.4618f, 8.124999f));
                    break;
                case "mp_overwatch":
                    p1 = new Parameter(new Vector3(-53.46009f, 1699.545f, 12864.13f));
                    p2 = new Parameter(new Vector3(-100.9972f, 886.66f, 12864.13f));
                    p3 = new Parameter(new Vector3(-1150.938f, -197.8107f, 12672.13f));
                    p4 = new Parameter(new Vector3(975.3053f, 519.643f, 12672.13f));
                    i1 = new Parameter(new Vector3(1118.287f, 513.1091f, 12672.13f));
                    break;
                case "mp_paris":
                    p1 = new Parameter(new Vector3(712.5516f, 179.9518f, 368.125f));
                    p2 = new Parameter(new Vector3(1467.308f, 681.6006f, 29.125f));
                    p3 = new Parameter(new Vector3(-390.5896f, -951.1304f, 38.92109f));
                    p4 = new Parameter(new Vector3(1933.161f, -310.8245f, 424.125f));
                    i1 = new Parameter(new Vector3(1949.322f, 1784.76f, -20.57607f));
                    break;
                case "mp_park":
                    i1 = new Parameter(new Vector3(1557.315f, -514.9321f, -45.94517f));
                    break;
                case "mp_plaza2":
                    p1 = new Parameter(new Vector3(-1.886f, 1290.721f, 616.077f));
                    p2 = new Parameter(new Vector3(339.825f, 935.809f, 648.125f));
                    p3 = new Parameter(new Vector3(262.078f, 35.642f, 739.125f));
                    p4 = new Parameter(new Vector3(751.781f, 1728.524f, 728.125f));
                    i1 = new Parameter(new Vector3(475.7262f, -1955.127f, 706.625f));
                    break;
                case "mp_qadeem":
                    i1 = new Parameter(new Vector3(-2371.043f, 1092.436f, 284.125f));
                    break;
                case "mp_radar":
                    p1 = new Parameter(new Vector3(-7490.877f, 757.002f, 2982.376f));
                    p2 = new Parameter(new Vector3(-10599.95f, 2962.548f, 2041.281f));
                    p3 = new Parameter(new Vector3(-10599.95f, 2962.548f, 2041.281f));
                    p4 = new Parameter(new Vector3(-8723.434f, 6015.048f, 1431.801f));
                    i1 = new Parameter(new Vector3(-3831.612f, 1656.025f, 1262.874f));

                    break;
                case "mp_roughneck":
                    i1 = new Parameter(new Vector3(-842.5134f, -2019.124f, 198.5567f));
                    break;
                case "mp_seatown":
                    i1 = new Parameter(new Vector3(-2247.698f, -1533.783f, 288.125f));
                    break;
                case "mp_shipbreaker":
                    p1 = new Parameter(new Vector3(-301.4881f, -211.6516f, 807.2462f));
                    p2 = new Parameter(new Vector3(150.1878f, -951.7475f, 636.586f));
                    p3 = new Parameter(new Vector3(1484.838f, -2765.512f, 662.1161f));
                    p4 = new Parameter(new Vector3(578.8755f, -2432.673f, 751.625f));
                    i1 = new Parameter(new Vector3(206.3026f, -856.7799f, 617.8029f));
                    break;
                case "mp_underground":
                    i1 = new Parameter(new Vector3(348.6256f, 1499.014f, -147.875f));
                    break;

                case "mp_terminal_cls":
                    p1 = new Parameter(new Vector3(1188.399f, 3880.078f, 40.125f));
                    p2 = new Parameter(new Vector3(294.7188f, 5788.46f, 744.125f));
                    p3 = new Parameter(new Vector3(2645.808f, 4343.474f, 512.125f));
                    p4 = new Parameter(new Vector3(1043.014f, 6075.584f, 240.125f));
                    i1 = new Parameter(new Vector3());
                    break;
            }
            return first = false;
        }
        static bool first=true;

        public static void InitialPosition(Entity player, string mapname)
        {
            if (first) setParam(mapname);
            switch (mapname)
            {
                case "mp_alpha":
                case "mp_boardwalk":
                case "mp_bootleg":
                case "mp_bravo":
                case "mp_carbon":
                case "mp_cement":
                case "mp_dome":
                case "mp_hardhat":
                case "mp_interchange":
                case "mp_italy":
                case "mp_lambeth":
                case "mp_meteora":
                case "mp_moab":
                case "mp_mogadishu":
                case "mp_morningwood":
                case "mp_nola":
                case "mp_overwatch":
                case "mp_paris":
                case "mp_park":
                case "mp_plaza2":
                case "mp_qadeem":
                case "mp_radar":
                case "mp_roughneck":
                case "mp_seatown":
                case "mp_shipbreaker":
                case "mp_underground":
                    player.Call("setorigin", i1);
                    break;

                case "mp_exchange":
                    switch (rnd.Next(4))
                    {

                        case 0:
                            player.Call("setorigin", new Parameter(new Vector3(-1711.739f, 2216.595f, 2327.625f)));
                            break;

                        case 1:
                            player.Call("setorigin", new Parameter(new Vector3(63.40273f, 3400.868f, 1694.125f)));
                            break;

                        case 2:
                            player.Call("setorigin", new Parameter(new Vector3(1356.891f, 2458.89f, 1551.125f)));
                            break;
                        case 3:
                            player.Call("setorigin", new Parameter(new Vector3(2718.034f, 2262.412f, 1463.125f)));
                            break;
                    }
                    break;
                default: break;
            }
        }

    }

}