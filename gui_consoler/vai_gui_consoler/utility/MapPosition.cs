using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Utility
{
    public class MapPositionDictionary
    {
        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
            public string image_path;
            public int id;
            public Vector3(int _id, float _x, float _y, float _z, string _image_path)
            {
                id = _id;
                x = _x;
                y = _y;
                z = _z;
                image_path = _image_path;
            }
        }
            public struct Pair<TKey, Vector3>
            {

                public TKey Key;
                public Vector3 Value;

                public Pair(KeyValuePair<TKey, Vector3> pair)
                {
                    Key = pair.Key;
                    Value = pair.Value;
                }//method

            }//struct

            public static void WriteXml<TKey, TValue>(XmlWriter writer, IDictionary<TKey, Vector3> dict)
            {

                var list = new List<Pair<TKey, Vector3>>(dict.Count);

                foreach (var pair in dict)
                {
                    list.Add(new Pair<TKey, Vector3>(pair));
                }//foreach

                var serializer = new XmlSerializer(list.GetType());
                serializer.Serialize(writer, list);

            }//method

            public static void ReadXml<TKey, TValue>(XmlReader reader, IDictionary<TKey, TValue> dict)
            {

                reader.Read();

                var serializer = new XmlSerializer(typeof(List<Pair<TKey, TValue>>));
                var list = (List<Pair<TKey, TValue>>)serializer.Deserialize(reader);

                foreach (var pair in list)
                {
                    dict.Add(pair.Key, pair.Value);
                }//foreach

                reader.Read();

            }//method

    }//class

    public static class DictionaryXML
    {
        public const float LOOP_TAG = 0.001f;
        public const string LOOP_DEFAULT = "Default";
        public static void Create()
        {
            Dictionary<string, MapPositionDictionary.Vector3> a = new Dictionary<string, MapPositionDictionary.Vector3>();
            a.Add(LOOP_DEFAULT, new MapPositionDictionary.Vector3(0, 2389.6f, 0.8f, 1033.41f, "/img/0ba5c77ae9a02808e407932f403cf708.jpg"));
            a.Add("A - Home", new MapPositionDictionary.Vector3(1, 2389.6f, 0.8f, 1033.41f, "/img/0ba5c77ae9a02808e407932f403cf708.jpg"));
            a.Add("B - Bar", new MapPositionDictionary.Vector3(2, 3386.76f, 0.8f, 1033.19f, "/img/422a926b43bdbe483fd6dbddee5828de.jpg"));
            a.Add("C - Club", new MapPositionDictionary.Vector3(3, 2386.0f, 0.8f, 1703.94f, "/img/ae10521efeeb68a0faba701084197f9b.jpg"));
            a.Add("D - School", new MapPositionDictionary.Vector3(4, 3386.0f, 0.8f, 1702.81f, "/img/fa8eeae6d500090b77865a1f148d8dd8.jpg"));
            a.Add("H - Hospital", new MapPositionDictionary.Vector3(5, 2386.0f, 0.8f, 1033.19f, "/img/6b5bbdfa7bdef7cf1a5b5bd9f5637fd6.jpg"));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            XmlWriter writer = XmlWriter.Create("map_points.xml", settings);
            MapPositionDictionary.WriteXml<string, MapPositionDictionary.Vector3>(writer, a);
            writer.Close();
        }

        public static bool Read(Dictionary<string, MapPositionDictionary.Vector3> a)
        {
            try
            {
            XmlReader reader = XmlReader.Create("map_points.xml");
            MapPositionDictionary.ReadXml<string, MapPositionDictionary.Vector3>(reader, a);
            reader.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    }//namespace
