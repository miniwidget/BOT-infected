using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InfinityScript;
namespace Infected
{
    public class MapEdit : BaseScript
    {
        public static void print(string format, params object[] p)
        {
            Log.Write(LogLevel.All, format, p);
        }
        public MapEdit()
            : base()
        {setting();}
#region setting
        private void setting ()
        {
            _mapname = Call<string>("getdvar", "mapname");
            Entity care_package = Call<Entity>("getent", "care_package", "targetname");
            _airdropCollision = Call<Entity>("getent", care_package.GetField<string>("target"), "targetname");
             loadMapEdit(_mapname); 
        }
        private void loadMapEdit(string mapname)

        { 
            try
            {
               StreamReader map = new StreamReader("scripts\\maps\\" + _mapname + ".txt");
                
                while (!map.EndOfStream)
                {
                    string line = map.ReadLine();
                    if (line.StartsWith("//") || line.Equals(string.Empty))
                    {
                        continue;
                    }
                    string[] split = line.Split(':');
                    if (split.Length < 1)
                    {
                        continue;
                    }
                    string type = split[0];
                    switch (type)
                    {

                        case "ramp":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            Ramp(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        case "ramp_v":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            Ramp_v(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        case "ramp_trans":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            Ramp_trans(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        case "HMramp":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            HMRamp(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        //case "HLramp":
                        //    split = split[1].Split(';');
                        //    if (split.Length < 2) continue;
                        //    HLRamp(parseVec3(split[0]), parseVec3(split[1]));
                        //    break;

                        case "elevator":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            Elevator(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        case "HiddenTP":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            HiddenTP(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        case "wall":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            Wall(parseVec3(split[0]), parseVec3(split[1]));
                            break;

                        case "model1":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            MODEL1(parseVec3(split[0]));
                            break;
                        case "model2":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            MODEL2(parseVec3(split[0]));
                            break;
                        case "model3":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            MODEL3(parseVec3(split[0]), parseVec3(split[1]));
                            break;
                        case "model4":
                            split = split[1].Split(';');
                            if (split.Length < 2) continue;
                            MODEL4(parseVec3(split[0]), parseVec3(split[1]));
                            break;

                        default:
                            print("Unknown MapEdit Entry {0}... ignoring", type);
                            break;
                    }

                }
                
            }
            catch (Exception e)
            {
                print("error loading mapedit for map {0}: {1}", mapname, e.Message);
            }
        }
        private void Ramp(Vector3 top, Vector3 bottom)
        {

            float distance = top.DistanceTo(bottom);
            int blocks = (int)Math.Ceiling(distance / 30);//30     
            Vector3 A = new Vector3((top.X - bottom.X) / blocks, (top.Y - bottom.Y) / blocks, (top.Z - bottom.Z) / blocks);
            Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(top - bottom));
            Vector3 angles = new Vector3(temp.Z, temp.Y + 90, temp.X);//90
            for (int b = 0; b <= blocks; b++)
            {

                var origin = bottom + (A * b);
                Entity ent = Call<Entity>("spawn", "script_model", new Parameter(origin));
                ent.Call("setmodel", MAPMODEL.getRampModel(_mapname));
                ent.SetField("angles", new Parameter(angles));
                ent.Call(33353, _airdropCollision); // clonebrushmodeltoscriptmodel
            }

        }
        private void Ramp_v(Vector3 top, Vector3 bottom)
        {

            float distance = top.DistanceTo(bottom);
            int blocks = (int)Math.Ceiling(distance / 30);//30     
            Vector3 A = new Vector3((top.X - bottom.X) / blocks, (top.Y - bottom.Y) / blocks, (top.Z - bottom.Z) / blocks);
            Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(top - bottom));
            Vector3 angles = new Vector3(temp.Z, temp.Y + 90, temp.X + 90);//90
            for (int b = 0; b <= blocks; b++)
            {

                var origin = bottom + (A * b);
                Entity ent = Call<Entity>("spawn", "script_model", new Parameter(origin));
                ent.Call("setmodel", MAPMODEL.getRampModel(_mapname));
                ent.SetField("angles", new Parameter(angles));
                ent.Call(33353, _airdropCollision); // clonebrushmodeltoscriptmodel
            }

        }
        private void Ramp_trans(Vector3 top, Vector3 bottom)
        {

            float distance = top.DistanceTo(bottom);
            int blocks = (int)Math.Ceiling(distance / 60);//30     
            Vector3 A = new Vector3((top.X - bottom.X) / blocks, (top.Y - bottom.Y) / blocks, (top.Z - bottom.Z) / blocks);
            Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(top - bottom));
            Vector3 angles = new Vector3(temp.Z, temp.Y + 90, temp.X);//90
            for (int b = 0; b <= blocks; b++)
            {

                var origin = bottom + (A * b);
                Entity ent = Call<Entity>("spawn", "script_model", new Parameter(origin));
                //ent.Call("setmodel", "");
                ent.SetField("angles", new Parameter(angles));
                ent.Call(33353, _airdropCollision); // clonebrushmodeltoscriptmodel
            }

        }
        private void HMRamp(Vector3 end, Vector3 start)
        {

            float distance = end.DistanceTo(start);
            int blocks = (int)Math.Ceiling(distance / 60);//30     
            Vector3 A = new Vector3((end.X - start.X) / blocks, (end.Y - start.Y) / blocks, (end.Z - start.Z) / blocks);
            Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(end - start));
            Vector3 BA = new Vector3(temp.Z, temp.Y, temp.X);//90
            for (int b = 0; b <= blocks; b++)
            {
                var origin = start + (A * b);
                var angles = BA;
                Entity ent = Call<Entity>("spawn", "script_model", new Parameter(origin));
                //ent.Call("setmodel", MAPMODEL.getRampModel(_mapname));
                ent.SetField("angles", new Parameter(angles));
                ent.Call(33353, _airdropCollision); // clonebrushmodeltoscriptmodel
            }

        }

        private Entity Wall(Vector3 start, Vector3 end)
        {
            float D = new Vector3(start.X, start.Y, 0).DistanceTo(new Vector3(end.X, end.Y, 0));
            float H = new Vector3(0, 0, start.Z).DistanceTo(new Vector3(0, 0, end.Z));
            int blocks = (int)Math.Round(D / 75, 0);
            int height = (int)Math.Round(H / 60, 0);

            Vector3 C = end - start;
            Vector3 A = new Vector3(C.X / blocks, C.Y / blocks, C.Z / height);
            float TXA = A.X / 4;
            float TYA = A.Y / 4;
            Vector3 angle = Call<Vector3>("vectortoangles", new Parameter(C));
            angle = new Vector3(0, angle.Y, 90);
            Entity center = Call<Entity>("spawn", "script_origin", new Parameter(new Vector3(
                (start.X + end.X) / 2, (start.Y + end.Y) / 2, (start.Z + end.Z) / 2)));
            for (int h = 0; h < height; h++)
            {
                Entity crate = spawnWall((start + new Vector3(TXA, TYA, 10) + (new Vector3(0, 0, A.Z) * h)), angle);
                crate.Call("enablelinkto");
                crate.Call("linkto", center);
                for (int i = 0; i < blocks; i++)
                {
                    crate = spawnWall(start + (new Vector3(A.X, A.Y, 0) * i) + new Vector3(0, 0, 10) + (new Vector3(0, 0, A.Z) * h), angle);
                    crate.Call("enablelinkto");
                    crate.Call("linkto", center);
                }
                crate = spawnWall(new Vector3(end.X, end.Y, start.Z) + new Vector3(TXA, TYA, 10) + (new Vector3(0, 0, A.Z) * h), angle);
                crate.Call("enablelinkto");
                crate.Call("linkto", center);
            }
            return center;
        }
        private Entity spawnWall(Vector3 origin, Vector3 angles)
        {
            Entity ent = Call<Entity>("spawn", "script_model", new Parameter(origin));
            ent.Call("setmodel", MAPMODEL.getWallModel(_mapname));
            ent.SetField("angles", new Parameter(angles));
            ent.Call(33353, _airdropCollision); // clonebrushmodeltoscriptmodel
            return ent;
        }

        private void Elevator(Vector3 enter, Vector3 exit)
        {
            Entity flag = Call<Entity>("spawn", "script_model", new Parameter(enter));
            flag.Call("setModel", MAPMODEL.getAlliesFlagModel(_mapname));

            OnInterval(100, () =>
            {
                foreach (Entity player in getPlayers())
                {
                    if (player.Origin.DistanceTo(enter) <= 50)
                    {
                        player.Call("setorigin", new Parameter(exit));
                    }
                }
                return true;
            });
        }
        private void HiddenTP(Vector3 enter, Vector3 exit)
        {
            //Entity flag = Call<Entity>("spawn", "script_model", new Parameter(enter));
            //flag.Call("setModel", "");
            //Entity flag2 = Call<Entity>("spawn", "script_model", new Parameter(exit));
            //flag2.Call("setModel", "");

            /*
             * Entity flag = Call<Entity>("spawn", "script_model", new Parameter(enter));
            flag.Call("setModel", "weapon_scavenger_grenadebag");
            Entity flag2 = Call<Entity>("spawn", "script_model", new Parameter(exit));
            flag2.Call("setModel", "weapon_oma_pack");
             */

            OnInterval(100, () =>
            {
                foreach (Entity player in getPlayers())
                {
                    if (player.Origin.DistanceTo(enter) <= 50)
                    {
                        player.Call("setorigin", new Parameter(exit));
                    }
                }
                return true;
            });
        }

        private void MODEL1(Vector3 m1)
        {
            Entity flag = Call<Entity>("spawn", "script_model", new Parameter(m1));
            flag.Call("setModel", MAPMODEL.getMODEL1(_mapname));

        }
        private void MODEL2(Vector3 m2)
        {
            Entity flag = Call<Entity>("spawn", "script_model", new Parameter(m2));
            flag.Call("setModel", MAPMODEL.getMODEL2(_mapname));

        }
        private void MODEL3(Vector3 start, Vector3 end)
        {
            // float distance = start.DistanceTo(end);
            Vector3 A = new Vector3(start.X, start.Y, start.Z);
            Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(start - end));
            Vector3 BA = new Vector3(temp.Z, temp.Y, temp.X);

            //spawnMODELwithAngle(A, BA);
            Entity ent = Call<Entity>("spawn", "script_model", new Parameter(A));
            ent.Call("setmodel", MAPMODEL.getMODEL3(_mapname));
            ent.SetField("angles", new Parameter(BA));

        }
        private void MODEL4(Vector3 start, Vector3 end)
        {
            // float distance = start.DistanceTo(end);
            Vector3 A = new Vector3(start.X, start.Y, start.Z);
            Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(start - end));
            Vector3 BA = new Vector3(temp.Z, temp.Y, temp.X);

            //spawnMODELwithAngle(A, BA);
            Entity ent = Call<Entity>("spawn", "script_model", new Parameter(A));
            ent.Call("setmodel", MAPMODEL.getMODEL4(_mapname));
            ent.SetField("angles", new Parameter(BA));

        }

        private Entity[] getPlayers()
        {
            List<Entity> players = new List<Entity>();
            for (int i = 0; i < 17; i++)
            {
                Entity entity = Call<Entity>("getentbynum", i);
                if (entity != null)
                {
                    if (entity.IsPlayer)
                    {
                        players.Add(entity);
                    }
                }
            }
            return players.ToArray();
        }
        private Vector3 parseVec3(string vec3)
        {
            vec3 = vec3.Replace(" ", string.Empty);
            if (!vec3.StartsWith("(") && !vec3.EndsWith(")")) throw new IOException("Malformed MapEdit File!");
            vec3 = vec3.Replace("(", string.Empty);
            vec3 = vec3.Replace(")", string.Empty);
            String[] split = vec3.Split(',');
            if (split.Length < 3) throw new IOException("Malformed MapEdit File!");
            return new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
        }
        private Entity _airdropCollision;
        private string _mapname;
        float startx; float starty; float startz;
        float endx; float endy; float endz;
        bool creating = false;
#endregion 
        #region onsay
        public override void OnSay(Entity player, string name, string text)
        {
            if (player.Name == "TeachMeEnglish")//&&player.GetField<string>("map") == "map1")
            {
                #region MAP edit
                //string[] msg = text.Split(' ');
                if (text.StartsWith("t "))
                {
                    player.Call("iprintlnbold", "^2TEXT added as ^7" + text.Split(' ')[1]);
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "//" + text.Split(' ')[1]);
                }
                if (text.StartsWith("m1 "))
                {
                    player.Call("iprintlnbold", "^2MODEL1 point ended");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;


                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "model1: (" + startx + "," + starty + "," + startz + ") ;" + text.Split(' ')[1]);
                }
                if (text.StartsWith("m2 "))
                {
                    player.Call("iprintlnbold", "^2MODEL2 point ended");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;

                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "model2: (" + startx + "," + starty + "," + startz + ") ;" + text.Split(' ')[1]);
                }
                if (text.StartsWith("m3 ") && !creating)
                {
                    player.Call("iprintlnbold", "^2MODEL3_withAngle point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("m3 ") && creating)
                {
                    player.Call("iprintlnbold", "^2MODEL3 point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "model3: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + endz + ");" + text.Split(' ')[1]);
                }
                if (text.StartsWith("m4 ") && !creating)
                {
                    player.Call("iprintlnbold", "^2MODEL4_withAngle point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("m4 ") && creating)
                {
                    player.Call("iprintlnbold", "^2MODEL4 point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "model4: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + endz + ");" + text.Split(' ')[1]);
                }

                if (text.StartsWith("w ") && !creating)
                {
                    player.Call("iprintlnbold", "^2WALL 1.0m point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("w ") && creating)
                {
                    player.Call("iprintlnbold", "^2WALL 1.0m point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "wall: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + startz + 100 + ");//1.0m Wall" + text.Split(' ')[1]);
                }
                if (text.StartsWith("w2 ") && !creating)
                {
                    player.Call("iprintlnbold", "^2WALL 1.5m point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("w2 ") && creating)
                {
                    player.Call("iprintlnbold", "^2WALL 1.5m point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "wall: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + startz + 150 + ");//1.5m Wall" + text.Split(' ')[1]);
                }
                //
                if (text.StartsWith("r ") && !creating)
                {
                    player.Call("iprintlnbold", "^2RAMP point startd");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z - 25;
                    creating = true;
                }
                else if (text.StartsWith("r ") && creating)
                {
                    player.Call("iprintlnbold", "^2RAMP point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z - 25;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "ramp: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + endz + ");" + text.Split(' ')[1]);
                }
                if (text.StartsWith("tr ") && !creating)
                {
                    player.Call("iprintlnbold", "^2RAMP point startd");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z - 25;
                    creating = true;
                }
                else if (text.StartsWith("tr ") && creating)
                {
                    player.Call("iprintlnbold", "^2RAMP point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z - 25;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "ramp_trans: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + endz + ");" + text.Split(' ')[1]);
                }
                if (text.StartsWith("hmr ") && !creating)
                {
                    player.Call("iprintlnbold", "^2Horizontal Middle RAMP ^7point startd");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("hmr ") && creating)
                {
                    player.Call("iprintlnbold", "^2Horizontal Middle RAMP ^7point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "HMramp: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + startz + ");" + text.Split(' ')[1]);
                }
                if (text.StartsWith("hlr ") && !creating)
                {
                    player.Call("iprintlnbold", "^2Horizontal Long RAMP ^7point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("hlr ") && creating)
                {
                    player.Call("iprintlnbold", "^2Horizontal Long RAMP ^7point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "HLramp: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + startz + ");" + text.Split(' ')[1]);
                }


                if (text.StartsWith("tp ") && !creating)
                {
                    player.Call("iprintlnbold", "^2ELEVATOR point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("tp ") && creating)
                {
                    player.Call("iprintlnbold", "^2ELEVATOR point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "elevator: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + endz + ");" + text.Split(' ')[1]);
                }
                if (text.StartsWith("htp ") && !creating)
                {
                    player.Call("iprintlnbold", "^2Hidden TELEPORT point started");
                    startx = player.Origin.X;
                    starty = player.Origin.Y;
                    startz = player.Origin.Z;
                    creating = true;
                }
                else if (text.StartsWith("htp ") && creating)
                {
                    player.Call("iprintlnbold", "^2Hidden TELEPORT point ended");
                    endx = player.Origin.X;
                    endy = player.Origin.Y;
                    endz = player.Origin.Z;
                    creating = false;
                    File.AppendAllText("scripts\\maps\\" + _mapname + ".txt", Environment.NewLine + "HiddenTP: (" + startx + "," + starty + "," + startz + ") ; (" + endx + "," + endy + "," + endz + ");" + text.Split(' ')[1]);
                }

                if (text.StartsWith("model "))
                {


                    Entity ent = Call<Entity>("spawn", "script_model", new Parameter(player.Origin));

                    ent.Call("setmodel", text.Split(' ')[1]);
                    ent.SetField("angles", new Parameter(player.GetField<Vector3>("angles")));
                    AfterDelay(20000, () =>
                    {

                        ent.Call("delete");

                    });

                }

                #endregion

            }
        }
        #endregion


    }
}


#region stuff
//private void stairN(Entity player)//to North
//{

//    int num = 3;
//    int x = 50;int z = 50;

//    int x_ = -5 + x * num;
//    int z_ = -30 + z * num;
//    int xx = 80; 

//    Vector3 A = new Vector3(player.Origin.X, player.Origin.Y, player.Origin.Z);
//    Vector3 B = new Vector3(player.Origin.X - 90, player.Origin.Y, player.Origin.Z);

//    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(A - B));
//    Vector3 angles = new Vector3(temp.Z - 45, temp.Y, temp.X);//90
//    for (int i = 0; i < num; i++)
//    {


//        Entity ent = Call<Entity>("spawn", "script_model", new Parameter(new Vector3(player.Origin.X + (x * i), player.Origin.Y, player.Origin.Z + (z * i))));
//        ent.Call("setmodel", "com_plasticcase_friendly");
//        ent.SetField("angles", new Parameter(angles));
//        ent.Call(33353, _airdropCollision);

//        Entity ent2 = Call<Entity>("spawn", "script_model", new Parameter(new Vector3((player.Origin.X + x_)+(xx*i), player.Origin.Y, player.Origin.Z + z_)));
//        ent2.Call("setmodel", "com_plasticcase_friendly");
//        ent2.Call(33353, _airdropCollision);
//    }

//}
//private void stairS(Entity player)//to South
//{


//    int j = 50;
//    int k = 50;
//    Vector3 A = new Vector3(player.Origin.X, player.Origin.Y, player.Origin.Z);
//    Vector3 B = new Vector3(player.Origin.X + 90, player.Origin.Y, player.Origin.Z);

//    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(A - B));
//    Vector3 angles = new Vector3(temp.Z - 45, temp.Y, temp.X);//90
//    for (int i = 0; i < 3; i++)
//    {


//        Entity ent = Call<Entity>("spawn", "script_model", new Parameter(new Vector3(player.Origin.X + (k * -i), player.Origin.Y, player.Origin.Z + (j * i))));
//        ent.Call("setmodel", "com_plasticcase_friendly");
//        ent.SetField("angles", new Parameter(angles));
//        ent.Call(33353, _airdropCollision);
//    }
//}
//private void stairE(Entity player)//to East
//{


//    int j = 50;
//    int k = 50;
//    Vector3 A = new Vector3(player.Origin.X, player.Origin.Y, player.Origin.Z);
//    Vector3 B = new Vector3(player.Origin.X, player.Origin.Y + 90, player.Origin.Z);

//    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(A - B));
//    Vector3 angles = new Vector3(temp.Z - 45, temp.Y, temp.X);//90
//    for (int i = 0; i < 3; i++)
//    {


//        Entity ent = Call<Entity>("spawn", "script_model", new Parameter(new Vector3(player.Origin.X, player.Origin.Y + (k * -i), player.Origin.Z + (j * i))));
//        ent.Call("setmodel", "com_plasticcase_friendly");
//        ent.SetField("angles", new Parameter(angles));
//        ent.Call(33353, _airdropCollision);
//    }
//}
//private void stairW(Entity player)//to West
//{


//    int j = 50;
//    int k = 50;
//    Vector3 A = new Vector3(player.Origin.X, player.Origin.Y, player.Origin.Z);
//    Vector3 B = new Vector3(player.Origin.X, player.Origin.Y - 90, player.Origin.Z);

//    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(A - B));
//    Vector3 angles = new Vector3(temp.Z - 45, temp.Y, temp.X);//90
//    for (int i = 0; i < 3; i++)
//    {


//        Entity ent = Call<Entity>("spawn", "script_model", new Parameter(new Vector3(player.Origin.X, player.Origin.Y + (k * i), player.Origin.Z + (j * i))));
//        ent.Call("setmodel", "com_plasticcase_friendly");
//        ent.SetField("angles", new Parameter(angles));
//        ent.Call(33353, _airdropCollision);
//    }
//}//to West
//private void stair_v(Entity player)//to North
//{

//    int num = 3;
//    int x = 50; int z = 50;

//    int x_ = -5 + x * num;
//    int z_ = -30 + z * num;
//    int xx = 80;

//    Vector3 A = new Vector3(player.Origin.X, player.Origin.Y, player.Origin.Z);
//    Vector3 B = new Vector3(player.Origin.X, player.Origin.Y, player.Origin.Z);

//    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(A - B));
//    Vector3 angles = new Vector3(temp.Z - 45, temp.Y+90, temp.X);//90
//    for (int i = 0; i < num; i++)
//    {


//        Entity ent = Call<Entity>("spawn", "script_model", new Parameter(new Vector3(player.Origin.X + (x * i), player.Origin.Y, player.Origin.Z + (z * i))));
//        ent.Call("setmodel", "com_plasticcase_friendly");
//        ent.SetField("angles", new Parameter(angles));
//        ent.Call(33353, _airdropCollision);

//        Entity ent2 = Call<Entity>("spawn", "script_model", new Parameter(new Vector3((player.Origin.X + x_) + (xx * i), player.Origin.Y, player.Origin.Z + z_)));
//        ent2.Call("setmodel", "com_plasticcase_friendly");
//        ent2.Call(33353, _airdropCollision);
//    }
//}
//private void loacation()
//{
//    foreach (Entity player in getPlayers())
//    {
//        if (player.Name.Contains("bot"))
//            LOCATION.BotsPosition(player, _mapname);

//        else
//        {
//            LOCATION.InitialPosition(player, _mapname);
//            if (player.Name=="TeachMeEnglish")
//            player.SetField("map", "map1");
//        }
//    }
//}
//private void loacation2()
//{
//    foreach (Entity player in getPlayers())
//    {
//        if (player.Name.Contains("bot"))
//            LOCATION.BotsPosition2(player, _mapname);

//        else
//        {
//            LOCATION.InitialPosition2(player, _mapname);

//        }
//    }
//}
#endregion
#region stuff
//private void loadMapEdit2(string mapname)

//{ 
//    try
//    {
//       StreamReader map = new StreamReader("scripts\\maps2\\" + _mapname + ".txt");

//        while (!map.EndOfStream)
//        {
//            string line = map.ReadLine();
//            if (line.StartsWith("//") || line.Equals(string.Empty))
//            {
//                continue;
//            }
//            string[] split = line.Split(':');
//            if (split.Length < 1)
//            {
//                continue;
//            }
//            string type = split[0];
//            switch (type)
//            {

//                case "ramp":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    Ramp(parseVec3(split[0]), parseVec3(split[1]));
//                    break;
//                case "ramp_trans":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    Ramp_trans(parseVec3(split[0]), parseVec3(split[1]));
//                    break;
//                case "HMramp":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    HMRamp(parseVec3(split[0]), parseVec3(split[1]));
//                    break;
//                case "HLramp":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    HLRamp(parseVec3(split[0]), parseVec3(split[1]));
//                    break;

//                case "elevator":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    Elevator(parseVec3(split[0]), parseVec3(split[1]));
//                    break;
//                case "HiddenTP":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    HiddenTP(parseVec3(split[0]), parseVec3(split[1]));
//                    break;
//                case "wall":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    Wall(parseVec3(split[0]), parseVec3(split[1]));
//                    break;

//                case "model1":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    MODEL1(parseVec3(split[0]));
//                    break;
//                case "model2":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    MODEL2(parseVec3(split[0]));
//                    break;
//                case "model3":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    MODEL3(parseVec3(split[0]), parseVec3(split[1]));
//                    break;
//                case "model4":
//                    split = split[1].Split(';');
//                    if (split.Length < 2) continue;
//                    MODEL4(parseVec3(split[0]), parseVec3(split[1]));
//                    break;

//                default:
//                    UTIL.print("Unknown MapEdit Entry {0}... ignoring", type);
//                    break;
//            }

//        }

//    }
//    catch (Exception e)
//    {
//        UTIL.print("error loading mapedit for map {0}: {1}", mapname, e.Message);
//    }
//}

#endregion
#region stuff
//if (Use_double_mapEdit)
//{
//    Random _rng = new Random();
//    switch (_rng.Next(2))
//    {
//        case 0:
//            loadMapEdit(_mapname);
//            loacation();
//            Call("iPrintlnBold", "MAP1");

//            HudElem INFO1 = HudElem.CreateServerFontString("hudbig", 0.8f);
//            INFO1.X = 395;
//            INFO1.Y = 3;
//            INFO1.HideWhenInMenu = true;
//            INFO1.SetText("1");
//            INFO1.Alpha = 0.5f;
//            break;

//        case 1:
//            loadMapEdit2(_mapname);
//            loacation2();
//            Call("iPrintlnBold", "MAP2");

//            HudElem INFO2 = HudElem.CreateServerFontString("hudbig", 0.8f);
//            INFO2.X = 395;
//            INFO2.Y = 3;
//            INFO2.HideWhenInMenu = true;
//            INFO2.SetText("2");
//            INFO2.Alpha = 0.5f;
//            break;
//    }
//}
//else

#endregion
#region stuff
//private void HLRamp(Vector3 end, Vector3 start)
//{

//    float distance = end.DistanceTo(start);
//    int blocks = (int)Math.Ceiling(distance / 80);//30     
//    Vector3 A = new Vector3((end.X - start.X) / blocks, (end.Y - start.Y) / blocks, (end.Z - start.Z) / blocks);
//    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(end - start));
//    Vector3 BA = new Vector3(temp.Z, temp.Y, temp.X);//90
//    for (int b = 0; b <= blocks; b++)
//    {
//        var origin = start + (A * b);
//        var angles = BA;
//        Entity ent = Call<Entity>("spawn", "script_model", new Parameter(origin));
//        ent.Call("setmodel", MAPMODEL.getRampModel(_mapname));
//        ent.SetField("angles", new Parameter(angles));
//        ent.Call(33353, _airdropCollision); // clonebrushmodeltoscriptmodel
//    }

//}
#endregion
//stairN(player); //stairS(player); stairW(player); stairE(player);
//stair_v(player); //bool Use_double_mapEdit =false;

