using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infected._1_MAP
{
    public class MAPMODEL
    {
        public static string getMODEL1(string mapModel)
        {
            switch (mapModel)
            {
                case "mp_alpha": return "com_satellite_dish_big"; //
                case "mp_bravo": return "foliage_afr_tall_bush_01a"; // 
                case "mp_interchange": return "vehicle_bus_destructible_mp"; //wood_plank2 com_wallchunk_concretebase02 com_pallet_2 
                case "mp_paris": return "foliage_pacific_bushtree01"; //foliage_gardenflowers_red_group
                case "mp_hardhat": return "vehicle_pavelow"; //mp_fullbody_opforce_juggernaut
                case "mp_restrepo_ss": return "rst_antenna";
                case "mp_exchange": return "foliage_pacific_bamboo01";
                case "mp_mogadishu": return "me_transmitting_tower";
                case "mp_overwatch": return "ow_crane_cab_platform";
                case "mp_dome": return "";
                case "mp_bootleg": return "";
                case "mp_radar": return "";
                case "mp_underground": return "";
                case "mp_cement": return "";
                case "mp_hillside_ss": return "";
                case "mp_park": return "";
                case "mp_terminal_cls": return "";
                case "mp_roughneck": return "";
                case "mp_boardwalk": return "";
                case "mp_moab": return "";
                case "mp_nola": return "";
                case "mp_six_ss": return "";
                case "mp_crosswalk_ss": return "";
                case "mp_village": return "";
                case "mp_shipbreaker": return "";
                case "mp_seatown": return "";
                case "mp_aground_ss": return "";
                case "mp_courtyard_ss": return "";
                case "mp_morningwood": return "";
                case "mp_qadeem": return "";
                case "mp_burn_ss": return "";
                case "mp_lambeth": return "";
                case "mp_plaza2": return "";
                case "mp_meteora": return "";
                case "mp_carbon": return "";
                case "mp_italy": return "";

            }
            return "";
        }
        public static string getMODEL2(string mapModel)
        {
            switch (mapModel)
            {
                case "mp_aground_ss": return "com_plasticcase_friendly";
                case "mp_alpha": return "com_teddy_bear_sitting"; //com_teddy_bear_sitting
                case "mp_boardwalk": return "com_plasticcase_friendly";
                case "mp_bootleg": return "com_teddy_bear_sitting"; //com_teddy_bear_sitting
                case "mp_bravo": return "com_teddy_bear_sitting"; //com_teddy_bear_sitting
                case "mp_burn_ss":
                case "mp_carbon": return "com_plasticcase_friendly";
                case "mp_cement": return "com_plasticcase_friendly";
                case "mp_courtyard_ss": return "com_plasticcase_friendly";
                case "mp_crosswalk_ss": return "com_plasticcase_friendly";
                case "mp_dome": return "com_tower_crane";
                case "mp_exchange": return "foliage_tree_grey_oak_med_a";
                case "mp_hardhat": return "com_plasticcase_friendly";
                case "mp_hillside_ss": return "com_plasticcase_friendly";
                case "mp_interchange": return "com_powerline_tower_top2_broken2"; //wood_plank2 com_wallchunk_concretebase02 com_pallet_2 
                case "mp_italy": return "com_plasticcase_friendly";
                case "mp_lambeth": return "com_woodlog_24_96_a";
                case "mp_meteora": return "com_plasticcase_friendly";
                case "mp_moab": return "rst_river_birch_med_vanim";
                case "mp_mogadishu": return "foliage_afr_tall_bush_01a";
                case "mp_morningwood": return "com_plasticcase_friendly";
                case "mp_nola": return "com_plasticcase_friendly";
                case "mp_overwatch": return "ow_crane_scaffolding_tower";
                case "mp_paris": return "ac_prs_mon_eiffel_tower";
                case "mp_park": return "com_plasticcase_friendly";
                case "mp_plaza2": return "com_woodlog_24_96_a";
                case "mp_qadeem": return "com_plasticcase_friendly";
                case "mp_radar": return "com_plasticcase_friendly";
                case "mp_restrepo_ss": return "rst_river_birch_med_vanim";
                case "mp_roughneck": return "com_plasticcase_friendly";
                case "mp_seatown": return "com_plasticcase_friendly";
                case "mp_shipbreaker": return "com_plasticcase_friendly";
                case "mp_six_ss": return "com_plasticcase_friendly";
                case "mp_terminal_cls": return "com_plasticcase_friendly";
                case "mp_underground": return "com_plasticcase_friendly";
                case "mp_village": return "com_plasticcase_friendly";

            }
            return "";
        }
        public static string getMODEL3(string mapModel)
        {

            switch (mapModel)
            {
                case "mp_aground_ss": return "com_plasticcase_friendly";
                case "mp_alpha": return "com_teddy_bear_sitting"; //
                case "mp_boardwalk": return "com_plasticcase_friendly";
                case "mp_bootleg": return "com_teddy_bear_sitting"; //
                case "mp_bravo": return "com_teddy_bear_sitting"; //
                case "mp_burn_ss": return "com_plasticcase_friendly";
                case "mp_carbon": return "com_plasticcase_friendly";
                case "mp_cement": return "com_plasticcase_friendly";
                case "mp_courtyard_ss": return "com_plasticcase_friendly";
                case "mp_crosswalk_ss": return "com_plasticcase_friendly";
                case "mp_dome": return "bc_military_comm_tower";
                case "mp_exchange": return "foliage_tree_grey_oak_med_a";
                case "mp_hardhat": return "com_plasticcase_friendly";
                case "mp_hillside_ss": return "com_plasticcase_friendly";
                case "mp_interchange": return "com_powerline_tower"; //com_powerline_tower foliage_tree_pine_lg_b
                case "mp_italy": return "com_plasticcase_friendly";
                case "mp_lambeth": return "com_woodlog_24_96_a";
                case "mp_meteora": return "com_plasticcase_friendly";
                case "mp_moab": return "com_plasticcase_friendly";
                case "mp_mogadishu": return "com_tower_crane";
                case "mp_morningwood": return "com_plasticcase_friendly";
                case "mp_nola": return "com_plasticcase_friendly";
                case "mp_overwatch": return "com_plasticcase_friendly";
                case "mp_paris": return "ac_prs_mon_eiffel_tower";
                case "mp_park": return "com_plasticcase_friendly";
                case "mp_plaza2": return "com_plasticcase_friendly";
                case "mp_qadeem": return "com_plasticcase_friendly";
                case "mp_radar": return "com_plasticcase_friendly";
                case "mp_restrepo_ss": return "rst_river_birch_med_vanim";
                case "mp_roughneck": return "com_plasticcase_friendly";
                case "mp_seatown": return "com_plasticcase_friendly";
                case "mp_shipbreaker": return "com_plasticcase_friendly";
                case "mp_six_ss": return "com_plasticcase_friendly";
                case "mp_terminal_cls": return "com_plasticcase_friendly";
                case "mp_underground": return "com_plasticcase_friendly";
                case "mp_village": return "com_plasticcase_friendly";

            }
            return "";
        }
        public static string getMODEL4(string mapModel)
        {

            switch (mapModel)
            {
                case "mp_aground_ss": return "com_plasticcase_friendly";
                case "mp_alpha": return "com_teddy_bear_sitting"; //
                case "mp_boardwalk": return "com_plasticcase_friendly";
                case "mp_bootleg": return "com_teddy_bear_sitting"; //
                case "mp_bravo": return "com_teddy_bear_sitting"; //
                case "mp_burn_ss": return "com_plasticcase_friendly";
                case "mp_carbon": return "com_plasticcase_friendly";
                case "mp_cement": return "com_plasticcase_friendly";
                case "mp_courtyard_ss": return "com_plasticcase_friendly";
                case "mp_crosswalk_ss": return "com_plasticcase_friendly";
                case "mp_dome": return "com_plasticcase_friendly";
                case "mp_exchange": return "foliage_tree_grey_oak_med_a";
                case "mp_hardhat": return "com_plasticcase_friendly";
                case "mp_hillside_ss": return "com_plasticcase_friendly";
                case "mp_interchange": return "foliage_tree_pine_lg_b"; //com_powerline_tower foliage_tree_pine_lg_b

                case "mp_italy": return "com_plasticcase_friendly";
                case "mp_lambeth": return "com_woodlog_24_96_a";
                case "mp_meteora": return "com_plasticcase_friendly";
                case "mp_moab": return "com_plasticcase_friendly";
                case "mp_mogadishu": return "vehicle_blackhawk_static_damage_sas";
                case "mp_morningwood": return "com_plasticcase_friendly";
                case "mp_nola": return "com_plasticcase_friendly";
                case "mp_overwatch": return "com_plasticcase_friendly";
                case "mp_paris": return "ac_prs_mon_eiffel_tower";
                case "mp_park": return "com_plasticcase_friendly";
                case "mp_plaza2": return "com_plasticcase_friendly";
                case "mp_qadeem": return "com_plasticcase_friendly";
                case "mp_radar": return "com_plasticcase_friendly";
                case "mp_restrepo_ss": return "rst_river_birch_med_vanim";
                case "mp_roughneck": return "com_plasticcase_friendly";
                case "mp_seatown": return "com_plasticcase_friendly";
                case "mp_shipbreaker": return "com_plasticcase_friendly";
                case "mp_six_ss": return "com_plasticcase_friendly";
                case "mp_terminal_cls": return "com_plasticcase_friendly";
                case "mp_underground": return "com_plasticcase_friendly";
                case "mp_village": return "com_plasticcase_friendly";

            }
            return "";
        }
        public static string getAlliesFlagModel(string mapname)
        {
            switch (mapname)
            {
                case "mp_carbon":
                case "mp_exchange":
                case "mp_hardhat":
                case "mp_seatown":
                    return "prop_flag_seal";
                case "mp_alpha":
                case "mp_boardwalk":
                case "mp_cement":
                case "mp_dome":
                case "mp_hillside_ss":
                case "mp_interchange":
                case "mp_lambeth":
                case "mp_moab":
                case "mp_morningwood":
                case "mp_nola":
                case "mp_overwatch":
                case "mp_park":
                case "mp_qadeem":
                case "mp_radar":
                case "mp_restrepo_ss":
                case "mp_roughneck":
                case "mp_terminal_cls":
                    return "prop_flag_delta";
                case "mp_bootleg":
                case "mp_bravo":
                case "mp_mogadishu":
                case "mp_shipbreaker":
                case "mp_village":
                    return "prop_flag_pmc";
                case "mp_paris":
                    return "prop_flag_gign";
                case "mp_aground_ss":
                case "mp_courtyard_ss":
                case "mp_italy":
                case "mp_meteora":
                case "mp_plaza2":
                case "mp_underground":
                    return "prop_flag_sas";
            }
            return "prop_flag_delta";
        }
        public static string getRampModel(string mapname)
        {
            switch (mapname)
            {
                case "mp_alpha": return "com_stone_bench_b";
                case "mp_lambeth": return "com_woodlog_24_96_a";
                case "mp_bootleg":
                case "mp_dome":
                case "mp_interchange": return "com_pallet_2";
                case "mp_paris": return "foliage_grass_flowerplants_triangularclump";
                case "mp_carbon":
                case "mp_terminal_cls": return "ch_bunker_wires_01";
                case "mp_bravo": return "wood_plank1";
                case "mp_exchange": return "me_rebar02";//"me_rebar01";
                case "mp_mogadishu": return "ch_mattress_3";// wood_bundle //
                case "mp_overwatch": return "";
                case "mp_village": return "wood_plank2";//vehicle_btr80_d_hatch1";//com_woodlog_16_192_c  afr_woodfence01"; 
                case "mp_boardwalk":
                case "mp_cement":
                case "mp_hardhat":
                case "mp_italy":
                case "mp_meteora":
                case "mp_moab":
                case "mp_morningwood":
                case "mp_nola":
                case "mp_park":
                case "mp_plaza2":
                case "mp_qadeem":
                case "mp_radar":
                case "mp_roughneck":
                case "mp_seatown":
                case "mp_shipbreaker":
                case "mp_underground":

                case "mp_aground_ss":
                case "mp_burn_ss":
                case "mp_courtyard_ss":
                case "mp_crosswalk_ss":
                case "mp_hillside_ss":
                case "mp_six_ss":
                    return "com_plasticcase_friendly";
                case "mp_restrepo_ss":
                    return "rst_pallet_2_ns";
            }
            return "";
        }
        public static string getWallModel(string mapname)
        {
            switch (mapname)
            {
                case "mp_lambeth": return "com_woodlog_24_96_a";

                case "mp_alpha": return "com_stone_bench_b"; //

                case "mp_bootleg":
                case "mp_dome":
                case "mp_interchange":
                    return "com_pallet_2"; //wood_plank2 com_wallchunk_concretebase02 com_pallet_2 
                case "mp_paris": return "foliage_grass_flowerplants_triangularclump";

                case "mp_bravo": return "wood_plank1";
                //wood_plank1 pb_block_square
                case "mp_carbon": return "cargo_cage_wall_short";

                case "mp_mogadishu": return "ch_wood_fence_128a";//model paris_fence_construction_02
                case "mp_restrepo_ss": return "ch_wood_fence_128a";

                case "mp_exchange": return "com_plasticcase_seals";
                case "mp_hardhat":

                case "mp_plaza2":
                case "mp_radar":
                case "mp_underground":
                case "mp_cement":
                case "mp_hillside_ss":
                case "mp_overwatch":
                case "mp_park":
                case "mp_terminal_cls":
                case "mp_roughneck":
                case "mp_boardwalk":
                case "mp_moab":
                case "mp_nola":
                case "mp_six_ss":
                case "mp_crosswalk_ss":

                case "mp_village":
                case "mp_shipbreaker":
                case "mp_seatown":
                case "mp_aground_ss":
                case "mp_courtyard_ss":
                case "mp_meteora":
                case "mp_morningwood":
                case "mp_qadeem":
                case "mp_italy":
                case "mp_burn_ss":
                    return "com_plasticcase_friendly";
            }
            return "";
        }
    }
}
