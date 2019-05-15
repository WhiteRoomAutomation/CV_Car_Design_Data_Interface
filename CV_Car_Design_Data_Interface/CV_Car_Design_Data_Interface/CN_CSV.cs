using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace CV_Car_Design_Data_Interface
{
    public class CN_CSV
    {
        public string Initials { get; set; }
        public int? Number { get; set; }
        public string Owner { get; set; }
        public string AAR_Type { get; set; }
        public string AAR_Type_Description{ get; set; }
        public string CN_Type{ get; set; }
        public string CN_Type_Description{ get; set; }
        public int? Tare_Weight{ get; set; }
        public int? Load_Limit{ get; set; }
        public float? Outside_Length{ get; set; }
        public float? Outside_Width{ get; set; }
        public float? Outside_Height{ get; set; }
        public float? Inside_Length{ get; set; }
        public float? Inside_Width{ get; set; }
        public float? Inside_Height{ get; set; }
        public int? Capacity{ get; set; }
        public string Capacity_Unit{ get; set; }
        public string Floor_Type{ get; set; }
        public string Floor_Type_Description{ get; set; }
        public string Door_Type{ get; set; }
        public string Door_Type_Description{ get; set; }
        public float? Door_Width{ get; set; }
        public float? Door_Height{ get; set; }
        public string Clearance_Restriction{ get; set; }
    }
}
