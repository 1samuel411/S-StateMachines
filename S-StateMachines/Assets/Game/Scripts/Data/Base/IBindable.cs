using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public delegate void BindableUpdated<T>(T val);

    public interface IBindable<T>
    {

        event BindableUpdated<T> OnUpdate;

        void SetValue(T t);
        T GetValue();

    }
}
