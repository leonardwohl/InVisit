using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld.Space;

namespace Invisit
{
    public class SerializableData
    {
        [Serializable]
        public struct LatLonS
        {
            public double lat;
            public double lon;

        }

        public static LatLonS SerializeLatLon(LatLong arg)
        {
            LatLonS ret = new LatLonS();
            ret.lat = arg.GetLatitude();
            ret.lon = arg.GetLongitude();
            return ret;
        }

        public static LatLonS SerializeLatLon(LatLongAltitude arg)
        {
            LatLonS ret = new LatLonS();
            ret.lat = arg.GetLatitude();
            ret.lon = arg.GetLongitude();
            return ret;
        }

        public static LatLong DeserializeLatLon(LatLonS arg)
        {
            return new LatLong(arg.lat,arg.lon);
        }

    }
}