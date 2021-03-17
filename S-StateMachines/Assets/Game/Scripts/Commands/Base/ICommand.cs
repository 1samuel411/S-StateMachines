using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public interface ICommand
    {

        bool Execute(); // Returns whether the command could be executed.

    }
}