using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Drawing;


namespace AutoGRAPHClass
{

    public class AutoGRAPHClient : WebClient
    {
        public WebClient Client { get; set; }
        public void Login(string Login, string Password)
        {
            Encoding = Encoding.UTF8;
            Headers["Content-Type"] = "application/json";
            Headers["AG-TOKEN"] = DownloadString("http://web.intelnavi.ru/ServiceJSON/Login?UserName=" + Login + "&Password=" + Password).Trim('\"', '\"'); // метод GETsession;
            Console.WriteLine(Headers["AG-TOKEN"]);
        }
        public RSchema[] EnumSchemas()
        {
            RSchema[] schemas = JsonConvert.DeserializeObject<RSchema[]>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumSchemas"));
            return schemas;
        }
        public REnumDevices EnumDevices(string schemaID)
        {
            REnumDevices enumDevices = JsonConvert.DeserializeObject<REnumDevices>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumDevices?schemaID=" + schemaID));
            return enumDevices;
        }
        public REnumDrivers EnumDrivers(string schemaID)
        {
            REnumDrivers enumDriver = JsonConvert.DeserializeObject<REnumDrivers>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumDrivers?schemaID=" + schemaID));
            return enumDriver;
        }
        public REnumGeofences EnumGeoFences(string schemaID)
        {
            REnumGeofences enumGeofences = JsonConvert.DeserializeObject<REnumGeofences>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumGeoFences?schemaID=" + schemaID));
            return enumGeofences;
        }
        public Dictionary<Guid, RParameters> EnumParameters(string schemaID, Guid[] IDs)
        {
            Dictionary<Guid, RParameters> enumParameters = JsonConvert.DeserializeObject<Dictionary<Guid, RParameters>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumParameters?schemaID="
                + schemaID + "&IDs=" + string.Join(",", IDs)));
            return enumParameters;
        }
        public RDeviceStatus[] EnumStatuses(string schemaID)
        {
            RDeviceStatus[] enumStatuses = JsonConvert.DeserializeObject<RDeviceStatus[]>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumStatuses?schemaID=" + schemaID));
            return enumStatuses;
        }
        public Dictionary<Guid, RGeoFence> GetGeofences(string schemaID, Guid[] IDs)
        {
            Dictionary<Guid, RGeoFence> getGeofance = JsonConvert.DeserializeObject<Dictionary<Guid, RGeoFence>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/EnumParameters?schemaID="
                 + schemaID + "&IDs=" + string.Join(",", IDs)));
            return getGeofance;
        }
        public RDeviceInfo GetDevicesInfo(string schemaID)
        {
            RDeviceInfo deviceInfo = JsonConvert.DeserializeObject<RDeviceInfo>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetDevicesInfo?schemaID=" + schemaID));
            return deviceInfo;
        }
        public Dictionary<Guid, ROnlineInfo> GetOnlineInfo(string schemaID, Guid[] IDs)
        {
            Dictionary<Guid, ROnlineInfo> onlineInfo = JsonConvert.DeserializeObject<Dictionary<Guid, ROnlineInfo>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetOnlineInfo?schemaID="
                + schemaID + "&IDs=" + string.Join(",", IDs)));
            return onlineInfo;
        }
        public Dictionary<Guid, ROnlineInfo> GetOnlineInfo(string schemaID, Guid[] IDs, string[] finalParams)
        {
            //"&finalParams=Speed,Googlemapsfinal"
            Dictionary<Guid, ROnlineInfo> onlineInfo = JsonConvert.DeserializeObject<Dictionary<Guid, ROnlineInfo>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetOnlineInfo?schemaID="
                + schemaID + "&IDs=" + string.Join(",", IDs) + "&finalParams=" + string.Join(",", finalParams)));
            return onlineInfo;
        }
        public Dictionary<Guid, ROnlineInfo> GetOnlineInfoAll(string schemaID)
        {
            Dictionary<Guid, ROnlineInfo> onlineInfoAll = JsonConvert.DeserializeObject<Dictionary<Guid, ROnlineInfo>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetOnlineInfoAll?schemaID="
                + schemaID + "&finalParams=*"));
            return onlineInfoAll;
        }
        public Dictionary<Guid, ROnlineInfo> GetOnlineInfoAll(string schemaID, string[] finalParams)
        {
            Dictionary<Guid, ROnlineInfo> onlineInfoAll = JsonConvert.DeserializeObject<Dictionary<Guid, ROnlineInfo>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetOnlineInfoAll?schemaID="
                + schemaID + "&finalParams=" + string.Join(",", finalParams)));
            return onlineInfoAll;
        }
        public Dictionary<Guid, RDataRanges> GetFilesInfo(string schemaID, Guid[] IDs)
        {
            Dictionary<Guid, RDataRanges> filesInfo = JsonConvert.DeserializeObject<Dictionary<Guid, RDataRanges>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetDataRanges?schemaID="
                + schemaID + "&IDs=" + string.Join(",", IDs)));
            return filesInfo;
        }
        public Dictionary<Guid, RTrips> GetTrips(string schemaID)//, Guid[] IDs, DateTime SD, DateTime ED, int tripSplitterIndex, string[] tripParams, string[] tripTotalParams)
        {
            Dictionary<Guid, RTrips> getTrips = JsonConvert.DeserializeObject<Dictionary<Guid, RTrips>>(DownloadString("http://web.intelnavi.ru/ServiceJSON/GetTrips?schemaID=" + schemaID + "&IDs=14ff63c9-bf24-4067-b128-97eefbeb0524&SD=20180808-0000&ED=20180809-0000&tripSplitterIndex=0"));
                //"http://web.intelnavi.ru/ServiceJSON/GetDataRanges?schemaID="
                //+ schemaID + "&IDs=" + string.Join(",", IDs)));
            return getTrips;
        }

    }
    public class RSchema
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public RSchema()
        { }
        public RSchema(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    /*  {"ID":"d53afdbe-22c8-4a06-b34c-162f9a994df3"
     *  ,"Groups":[{"ID":"bf5372f9-c5ca-4885-a5d9-462141ec565e","ParentID":null,"Name":"Сибпромжелдортранс"}],
     *  
     *  "Items":[{"Serial":410239
     *            ,"Allowed":true
     *            ,"Properties":[{"Inherited":true
     *                            ,"Type":0
     *                            ,"Name":"Alias"
     *                            ,"Value":null}
     *                            ,
     *                           {"Inherited":false
     *                            ,"Type":0
     *                            ,"Name":"VehicleRegNumber"
     *                            ,"Value":"8465"}]
                  ,"FixedLocation":null
                  ,"Image":"(folder)"
                  ,"TripSplitters":[{"ID":0,"Name":"Смена"}
     *                              ,{"ID":1,"Name":"Сутки"}]
     *            ,"IsAreaEnabled":false
     *            ,"ID":"513f9cc4-69e8-4bf7-bdee-403fbcdb3285"
     *            ,"ParentID":"bf5372f9-c5ca-4885-a5d9-462141ec565e"
     *            ,"Name":"ТЭМ-2"}]}


    */
    public class REnumDevices
    {
        public RGroupItem[] Groups { get; set; } // все группы приборов в схеме
        public RDeviceItem[] Items { get; set; } // все ТС в схеме
    }

    public class RGroupItem
    {
        public Guid ID { get; set; }             // уникальный ID объекта в схеме
        public Guid? ParentID { get; set; }      // ID-родительской группы
        public string Name { get; set; }         // название группы
    }

    public class RDeviceItem : RGroupItem
    {
        public int Serial { get; set; }          // номер прибора
        public bool Allowed { get; set; }        // доступен или нет ( = включён ли этот прибор в ключ сервера)
        public RProperty[] Properties { get; set; } // список свойств прибора (включая унаследованные)
        public RPoint FixedLocation { get; set; }   // если !=null - данный прибор является стационарным и это его координаты (широта/долгота)
        public string Image { get; set; }          // имя файла с изображением прибора (.png)
        public RTripSplitter[] TripSplitters { get; set; } // делители на рейсы для этого прибора
        public bool IsAreaEnabled { get; set; }  // включена или нет обработка полей на данном приборе
    }


    public class RProperty
    {
        public bool Inherited { get; set; }     // унаследованное (true) или собственное (false) свойство
        public RPropType Type { get; set; }     // тип свойства
        public string Name { get; set; }        // название свойства
        public object Value { get; set; }       // значение (типа свойства зависит от поля Type)
    }
    public enum RPropType : int                 // типы свойств
    {
        String = 0,
        Number = 1,
        Date = 2,
        TareTable = 3,
        Time = 4,
        Memo = 5,
        Color = 6,
        Bool = 7,
        Radio = 8,
        Image = 9,
        File = 10,
        ProgressBar = 11,
        Combobox = 12,
        FileLink = 13,
        Device = 14,
        GeoFence = 15
    }

    public class RTripSplitter
    {
        public int ID { get; set; }              // уникальный ID делителя в схеме
        public string Name { get; set; }        // название делителя
    }

    public class RPoint
    {
        public double Lat { get; set; }              // уникальный ID делителя в схеме
        public double Lng { get; set; }        // название делителя
    }

    public class REnumDrivers
    {
        public RGroupItem[] Groups { get; set; } // все группы водителей в схеме
        public RDriverItem[] Items { get; set; } // все водители в схеме
    }

    public class RDriverItem : RGroupItem
    {
        public string DriverID { get; set; }        // номер карты водителя
        public RProperty[] Properties { get; set; } // список свойств водителя (включая унаследованные)
    }

    public class REnumGeofences
    {
        public RGroupItem[] Groups { get; set; } // все группы геозон в схеме
        public RGeofenceItem[] Items { get; set; } // все геозоны в схеме
    }

    public class RGeofenceItem : RGroupItem
    {
        public RProperty[] Properties { get; set; } // список свойств геозоны (включая унаследованные)
    }


    public class RParameters
    {
        public Guid ID { get; set; }                     // ID ТС
        public RParameter[] FinalParams { get; set; }    // финальные (итоговые) параметры
        public RParameter[] OnlineParams { get; set; }   // онлайн (табличные) параметры
    }

    public class RParameter
    {
        public string Name { get; set; }                 // имя параметра (внутренее название, латиница - например Daylight, Speed, ...)
        public string Caption { get; set; }              // название параметра (например "Дн. освещ", "Скорость", ...)
        public string GroupName { get; set; }            // группа параметров (параметры могут объединяться в группы с одним названием)
        public ReturnType ReturnType { get; set; }       // тип параметра (см. ниже)
        public string Unit { get; set; }                 // ед.измерения параметра (км/ч, км, кг, ...)
        public string Format { get; set; }               // форматирование параметра (dd.MM.yyyy, ...)
        public RParameterStatus[] Statuses { get; set; } // список статусов параметра.
                                                         // например для параметра Motion (Движение) возможны три статуса - Стоянка, Движение, Полёт)
    }

    public class RParameterStatus
    {
        public int Value { get; set; }                   // численное значение статуса
        public string Caption { get; set; }              // название статуса
        public Guid ReferenceID { get; set; }            // если статус геозона или водитель - здесь хранится GUID этого объекта
        public Guid[] ReferenceIDs { get; set; }         // если геозоны с наложениями - здесь полный список геозон (максимум 4), в которых находится прибор
    }

    // тип данных = тип статуса
    public enum ReturnType : int
    {
        Boolean = 0,
        Byte = 1,
        Int32 = 2,
        Int64 = 3,
        Double = 4,
        DateTime = 5,
        TimeSpan = 6,
        Guid = 7,
        Guid4 = 8,
        String = 9,
        Image = 10,
        Coordinates = 11
    }

    public partial class RDeviceStatus
    {
        public int ID { get; set; }                  // ID статуса, начинаются как правило с 0
        public string Name { get; set; }             // user friendly название статуса
        public string ImageName { get; set; }        // название иконки для этого статуса
        public bool Enabled { get; set; }            // включен статус в схеме или нет
    }

    public class RGeoFence
    {
        public Guid ID { get; set; }           // ID геозоны
        public string Name { get; set; }       // название геозоны
        public string ImageName { get; set; }  // имя файла изображения

        public bool IsPolygon { get; set; }    // true, если полигон; false, если точка
        public double R { get; set; }          // радиус точки в метрах. Для полигонов всегда 0
        public double[] Lat { get; set; }      // массив широт точек
        public double[] Lng { get; set; }      // массив долгот точек
    }

    public class RDeviceInfo
    {
        public RDeviceStage[] Stages { get; set; }  // массив конфигурации отрезков
    }

    public class RDeviceStage
    {
        public string Name { get; set; }       // название отрезка для UI
        public string Parameter { get; set; }  // название параметра, который используется для получения данного отрезка
        public bool IsGroup { get; set; }      // true, если это Parameter - название группы или маска параметров. false, если это параметр.
        public string Caption { get; set; }    // название отрезка для UI
        public string Image { get; set; }      // имя файла картинки для этого отрезка
    }

    public class ROnlineInfo
    {
        public Guid ID { get; set; }                // ID ТС
        public string Name { get; set; }            // название ТС
        public RPoint LastPosition { get; set; }    // точка-местоположение или null, если неизвестно
        public DateTime DT { get; set; }            // дата-время последнего местоположения (=координат), в UTC
        public ROnlineState State { get; set; }     // состояние
        public double Speed { get; set; }           // скорость (в км/ч)
        public double Course { get; set; }          // направление движения в градусах (=азимут) или -1 если координаты-местоположение недостоверно        
        public string Address { get; set; }         // адрес в текстовом виде или null, если адрес неизвестен
        public Dictionary<string, object> Final { get; set; } // финальные параметры
    }

    public enum ROnlineState
    {
        Park = 0,     // остановка
        Move = 1,     // движение
        Flight = 2    // полёт
    }

    /* {"d4dcf8fe-35bf-4289-8c58-56b28b8018e9":{"ID":"d4dcf8fe-35bf-4289-8c58-56b28b8018e9"
                                            ,"Name":"JCB"
                                            ,"Serial":408267
                                            ,"LastModified":"2019-01-13T04:07:23.6263712Z"
                                            ,"Files":[{"File":"0408267-180416.sbin"
                                                       ,"Length":521168
                                                       ,"LastModified":"2018-04-22T14:03:12.7669629Z"}
                                                       ,{"File":"0408267-180423.sbin"
                                                       ,"Length":579936
                                                       ,"LastModified":"2018-04-28T05:38:00.6725748Z"}
                                                       ,{"File":"0408267-180430.sbin"
                                                       ,"Length":949072
                                                       ,"LastModified":"2018-05-04T14:34:59.8018392Z"}
                                                       ,{"File":"0408267-180507.sbin"
                                                       ,"Length":400144
                                                       ,"LastModified":"2018-05-12T08:27:44.8523148Z"}
                                                       ,{"File":"0408267-180521.sbin"
                                                       ,"Length":501760
                                                       ,"LastModified":"2018-05-27T16:58:38.4840486Z"}
                                                       ,{"File":"0408267-190107.sbin"
                                                       ,"Length":73552,
                                                       ,"LastModified":"2019-01-13T04:07:23.6263712Z"}]}}*/
    public class RDataRanges
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Serial { get; set; }
        public DateTime LastModified { get; set; }
        public RFile[] Files { get; set; }
    }

    public class RFile
    {
        public string File { get; set; }
        public int Length { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class RTrips
    {
        public Guid ID { get; set; }               // ID ТС
        public string Name { get; set; }           // название ТС
        public RTrip[] Trips { get; set; }         // рейсы
        public Dictionary<string, object> Total { get; set; } // финальные значения по всем рейсам
    }

    public class RTrip
    {
        public int Index { get; set; }             // порядковый номер рейса (с 0)
        public DateTime SD { get; set; }           // дата/время начала рейса (в UTC)
        public DateTime ED { get; set; }           // дата/время конца рейса (в UTC)
        public RPoint PointStart { get; set; }     // координата начала рейса
        public RPoint PointEnd { get; set; }       // координата конца рейса
        public RTripStage[] Stages { get; set; }   // отрезки для данного рейса
        public Dictionary<string, object> Total { get; set; } // параметры для РЕЙСА (визуально это колонки в таблице рейсов после колонок "Начало" и "Конец")
        public RTripArea[] Areas { get; set; }     // полигоны обработанных полей (только для методов GetTripsArea, GetTripsAreaTotal, GetTripAreaItems)
    }

    public class RTripStage // отрезки рейса (например "Остановки", "Заправки/сливы")
    {
        public string Name { get; set; }                      // название отрезка
        public string Alias { get; set; }                     // алиас (некоторые параметры именуются через Alias)
        public string[] Params { get; set; }                  // список имён колонок в отрезке - например "Motion", "Daylight", "Speed", "Speed Avg" 
        public RTripStageItem[] Items { get; set; }           // строки отрезка (только если GetTrips, в GetTripsTotal == null)
        public RParameterStatus[] Statuses { get; set; }      // список доступных статусов для данного параметра/отрезка
        public Dictionary<string, object> Total { get; set; }  // финальные данные по отрезку (как правило - суммарные данные по всем Items)
    }

    public class RTripStageItem
    {
        public int Index { get; set; }                      // Индекс отрезка (нумерация с 0)
        public DateTime SD { get; set; }                    // дата/время начала отрезка (в UTC)
        public DateTime ED { get; set; }                    // дата/время конца отрезка (в UTC)
        public int Status { get; set; }                     // ID статуса (0 = выключенный, >0 = порядковый номер статуса)
        public Guid StatusID { get; set; }                  // ID геозоны (если отрезок геозон)
        public Guid[] StatusIDs { get; set; }               // ID геозон (если есть перекрытия - тут может быть больше одного элемента)
        public string Caption { get; set; }                 // имя статуса (Move, Park, ...), название геозоны (если отрезок геозон), ФИО водителя, ...
        public object[] Values { get; set; }                // значения для колонок в табличке отрезка (количество и порядок соответствуют RTripStage.Params)
    }

    public class RTripArea
    {
        public Color Color { get; set; }            // цвет полигона
        public double[][][] Polygons { get; set; }  // массив полигонов, состоящий из массива точек, каждая из которых возвращается как массив из двухх элементов - широта и долгота
    }

  



    class Help
    {
    }
}
