using System;
using UnityEngine;

namespace MosPolyJam
{
    public class SerializeInterface : PropertyAttribute
    {
        public readonly Type SerializeType;

        public SerializeInterface (Type serializeType)
        {
            SerializeType = serializeType;
        }
    }
}