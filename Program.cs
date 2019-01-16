using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
//using AutoGRAPHService;
using Newtonsoft.Json;
using AutoGRAPHClass;

namespace AutoGRAPHSvcTest
{
    class Program
    {

        static void Main(string[] args)
        {
            

            AutoGRAPHClient autoGRAPHClient = new AutoGRAPHClient();            
            autoGRAPHClient.Login("douk771", "123456");
            int xe = 0;
            double ds;

            RSchema[] schemas = autoGRAPHClient.EnumSchemas();
            foreach (var x in schemas)
                Console.WriteLine("{2}) ID : {0} - Name : {1}", x.ID, x.Name, xe++);
            RSchema schema = schemas[171];
            //string schemaID = schemas[167].ID;
            Console.WriteLine("Schema Name : {0}", schema.Name);
            REnumDevices devices = autoGRAPHClient.EnumDevices(schema.ID); //164 стац коты, 168 студенов
                                                                           // foreach (var x in devices.Groups)
                                                                           //   Console.WriteLine("PID:{0} - ID:{1} - Name:{2}", x.ParentID, x.ID, x.Name);


            Console.WriteLine("Devices : ");
            foreach (var z in devices.Items)
                Console.WriteLine("ID:{0} PID:{1} Serial:{2} Name:{3} //{4}", z.ID, z.ParentID, z.Serial, z.Name, z.FixedLocation);

            Console.WriteLine("Drivers Name : ");
            REnumDrivers drivers = autoGRAPHClient.EnumDrivers(schema.ID);
            foreach (var x in drivers.Items)
                Console.WriteLine(x.Name);

            Console.WriteLine("GeoFence : ");
            REnumGeofences geofences = autoGRAPHClient.EnumGeoFences(schema.ID);
            foreach (var x in geofences.Items)
            {
                Console.WriteLine("{0} ", x.Name);
                //     foreach (var z in x.Properties)
                //       Console.WriteLine("\t{0} : {1}", z.Name, z.Value);
            }
           
            Guid[] IDs = new Guid[devices.Items.Count<RDeviceItem>()];
            //List<Guid[]> ids = new List<Guid[]>();

            //Dictionary<Guid,RParameters> parameters = autoGRAPHClient.EnumParameters(schema.ID,)
            int count = 0;
            foreach (var x in devices.Items)
            {
                Console.WriteLine("{0} {1}", x.Name, x.ID);
                IDs[count] = x.ID;
                // Console.WriteLine(IDs[count]);
                count++;
            }

            

            Dictionary<Guid, RParameters> parameters = autoGRAPHClient.EnumParameters(schema.ID, IDs);
            /* foreach(var x in parameters)
             {
                 Console.WriteLine("{0}", x.Key);
                 Console.WriteLine("Final Parameters:");
                 foreach (var z in x.Value.FinalParams)
                     Console.WriteLine(z.Name);
                 Console.WriteLine("Online Parameters: ");
                 foreach (var z in x.Value.OnlineParams)
                     Console.WriteLine(z.Name);
             }*/

            RDeviceStatus[] deviceStatuses = autoGRAPHClient.EnumStatuses(schema.ID);
            //foreach (var x in deviceStatuses)
            //  Console.WriteLine("{0} : {1} : {2}", x.ImageName, x.Name, x.Enabled);

            RDeviceInfo deviceInfo = autoGRAPHClient.GetDevicesInfo(schema.ID);
            Console.WriteLine(devices.Groups.Length);
            foreach (var x in deviceInfo.Stages)
            {
                Console.WriteLine("{0} : {1}", x.Name, x.Caption);
            }

            /*   Dictionary<Guid, ROnlineInfo> onlineInfo = autoGRAPHClient.GetOnlineInfo(schema.ID, "bf5372f9-c5ca-4885-a5d9-462141ec565e");
               foreach (var x in onlineInfo.Values)
               {
                   Console.WriteLine("{0}: {1}", x.Name, x.DT);
                   foreach (var z in x.Final)
                       Console.WriteLine("\t{0}:{1}", z.Key, z.Value);
               }
               */
            //string s = "d4dcf8fe-35bf-4289-8c58-56b28b8018e9,79f69171-0e03-4da2-b252-a7d95c9b2d44,e32f23a0-2862-453c-a4be-b7923939e71f,3d552177-7f47-4688-9fa2-a0a77a92fb91,fb1bf6c0-449d-4d0e-b14c-acbaabbd5741";
            Dictionary<Guid, RDataRanges> filesInfo = autoGRAPHClient.GetFilesInfo(schema.ID, IDs);


            foreach( var x in filesInfo.Values)
            {
                Console.WriteLine("{0} Last File date: {1} Count files {2}", x.Name, x.LastModified, x.Files.Count<RFile>());
                foreach(var z in x.Files)
                {
                    Console.WriteLine("\t{0}",z.File);
                }
            }
            // string s = autoGRAPHClient.DownloadString("http://web.intelnavi.ru/ServiceJSON/GetTrips?schemaID=" + schema.ID + "&IDs=14ff63c9-bf24-4067-b128-97eefbeb0524&SD=20180808-0000&ED=20180809-0000&tripSplitterIndex=0");
            //GetTrips?schemaID=" + schema.ID + "&IDs=14ff63c9-bf24-4067-b128-97eefbeb0524" + "&SD=20180808-0000" + "&ED=20180809-0000" + "&tripSplitterIndex = 0");

            Dictionary<Guid, RTrips> tripR = autoGRAPHClient.GetTrips(schema.ID);
            foreach (var x in tripR.Values)
            {
                Console.WriteLine("{0} ", x.Name);
                foreach (var z in x.Trips)
                {
                    Console.WriteLine("\t{0} - {1}", z.SD, z.ED);
                }

            }
            //JD8 14ff63c9-bf24-4067-b128-97eefbeb0524





            /*  foreach (var x in IDs)
                  Console.WriteLine(x);
              string s = autoGRAPHClient.DownloadString("http://web.intelnavi.ru/ServiceJSON/GetDataRanges?schemaID=" + schema.ID + "&IDs=d4dcf8fe-35bf-4289-8c58-56b28b8018e9" +
                  ",79f69171-0e03-4da2-b252-a7d95c9b2d44,e32f23a0-2862-453c-a4be-b7923939e71f,3d552177-7f47-4688-9fa2-a0a77a92fb91,fb1bf6c0-449d-4d0e-b14c-acbaabbd5741");
              Console.WriteLine(s.Length);
              */

            /*
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers["Content-Type"] = "application/json";
            var session = client.DownloadString("http://web.intelnavi.ru/ServiceJSON/Login?UserName=douk771&Password=123456");//.Trim('\"', '\"'); // метод GET
            Console.WriteLine(session);
            Console.WriteLine("******");
            // далее обычная работа с JSON
            client.Headers["AG-TOKEN"] = session;
            
            string JSONSchemasString = client.DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumSchemas");
            RSchema[] schemas = JsonConvert.DeserializeObject<RSchema[]>(JSONSchemasString);
            RSchemas[] schemas2 = JsonConvert.DeserializeObject<RSchemas[]>(JSONSchemasString);

            autoGRAPHClient.Login("douk771", "123456");
            string JSONSchemasString2 = client.DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumSchemas");
            RSchemas[] schemas21 = JsonConvert.DeserializeObject<RSchemas[]>(JSONSchemasString);

            foreach (var x in schemas)
                Console.WriteLine(x.Name);

            foreach (var x in schemas21)
                Console.WriteLine("ID : {0} - Name : {1}",x.ID, x.Name);

            */

            //Console.WriteLine(client.Site);
            Console.ReadKey();
            //077d5ce6-362e-4ba1-bff2-d88c96f2c048
          //  var car = client.DownloadString("http://web.intelnavi.ru/ServiceJSON//EnumDevices?schemaID=077d5ce6-362e-4ba1-bff2-d88c96f2c048");
            Console.WriteLine("******");
            //Console.WriteLine(car);

            Console.WriteLine("End!");
            Console.ReadKey();
            
            Console.WriteLine("------------");
            /*RSchema[] schemas;
            using (var f = new TimeCalc("EnumSchemas"))    // запрашиваем список доступных схем
                schemas = client.EnumSchemas();
            //foreach (var x in schemas)
              //  Console.WriteLine("{0} - {1}",x.ExtensionData, x.Name);
            Console.WriteLine(schemas[2].Name);
            Console.ReadKey();
            var currentSchema = schemas.FirstOrDefault(p => p.Name == schemas[2].Name).Name;
           // var car = svc.GetDevicesInfo(currentSchema);
            

          //  RDeviceInfo rDeviceInfo = svc.GetDevicesInfo(currentSchema);
          
                
              
            // currentDevices = devices.Items.Where(p => p.Serial == 9999999 || p.Serial == 9999998).Select(p => p.ID).ToArray(); // выбираем только приборы с номерами 9999998 и 9999999

            Console.WriteLine("@@@@@@@@@@@@@");
            Console.ReadKey();

            REnumDevices devices;
            Guid[] currentDevices = null;
            using (var f = new TimeCalc("EnumDevices"))
            {
                devices = client.EnumDevices(currentSchema); // запрашиваем список доступных в схеме приборов
                foreach (var item in devices.Items)
                    Console.WriteLine("\t" + item.Name);
                currentDevices = devices.Items.Where(p => p.Serial == 9999999 || p.Serial == 9999998).Select(p => p.ID).ToArray(); // выбираем только приборы с номерами 9999998 и 9999999
            }
            Console.ReadKey();
            /*using (var f = new TimeCalc("EnumGeoFences"))  // запрашиваем список доступных в схеме геозон
                svc.EnumGeoFences(currentSchema);

            using (var f = new TimeCalc("GetDevicesInfo"))
                svc.GetDevicesInfo(currentSchema);

            using (var f = new TimeCalc("EnumParameters")) // запрашиваем список параметров для ТС
                svc.EnumParameters(currentSchema, currentDevices);

            
            using (var f = new TimeCalc("GetOnlineInfo")) // запрашиваем онлайн-состояние ТС
            {
                var onlineInfo = svc.GetOnlineInfo(currentSchema, currentDevices);
                var serializer = new DataContractSerializer(typeof (ROnlineInfo));
                foreach (var oi in onlineInfo)
                {
                    var stm = new MemoryStream();
                    serializer.WriteObject(stm, oi.Value);
                    var fileName = "OnlineInfo_" + oi.Key + ".xml";
                    File.WriteAllBytes(fileName, stm.ToArray()); // записываем на диск в файл .xml сериализованное состояние
                    Console.WriteLine("OnlineInfo: {0}", fileName);
                }
            }

            using (var f = new TimeCalc("GetTrips")) // запрашиваем список рейсов за сегодня для ТС
            {
                var serializer = new DataContractSerializer(typeof (RTrips));
                var trips = svc.GetTrips(currentSchema, currentDevices, DateTime.Now.Date, DateTime.Now);
                foreach (var trip in trips)
                {
                    var stm = new MemoryStream();
                    serializer.WriteObject(stm, trip.Value);
                    var fileName = "Trips_" + trip.Key + ".xml";
                    File.WriteAllBytes(fileName, stm.ToArray()); // записываем на диск в файл .xml сериализованное состояние
                    Console.WriteLine("Trips: {0}", fileName);
                }
            }

            using (var f = new TimeCalc("GetTrack"))       // запрашиваем треки за последние 5 дней для всех ТС, с номерами начинающихся 9999998
            {
                var serializer = new DataContractSerializer(typeof(RTrackInfo[]));
                var tracks = svc.GetTrack(currentSchema, currentDevices, DateTime.Now.Date, DateTime.Now);
                foreach (var trip in tracks)
                {
                    var stm = new MemoryStream();
                    serializer.WriteObject(stm, trip.Value);
                    var fileName = "Tracks_" + trip.Key + ".xml";
                    File.WriteAllBytes(fileName, stm.ToArray()); // записываем на диск в файл .xml сериализованное состояние
                    Console.WriteLine("Tracks: {0}", fileName);
                }
            }*/
            Console.ReadLine();
        }
    }

    // простой класс для замера скорости выполнения блока
    class TimeCalc : IDisposable
    {
        readonly DateTime DT;
        readonly string name;
        internal TimeCalc(string name) { DT = DateTime.Now; this.name = name; }
        void IDisposable.Dispose() { Console.WriteLine(name + ": " + DateTime.Now.Subtract(DT).TotalSeconds.ToString("F3", CultureInfo.InvariantCulture)); }
    }
}