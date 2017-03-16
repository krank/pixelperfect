using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace PixelPerfect
{
    public interface IPixelPerfectRegistrar
    {

        void RegisterObject(PixelPerfectObject obj);

        void DeregisterObject(PixelPerfectObject obj);

        bool ObjectIsRegistered(PixelPerfectObject obj);

    }
}