//-----------------------------------------------------------------------------
// <copyright file="SingletonTemplate.cs" company="Microsoft">
//  (c) Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.WindowsServerSolutions.Common
{
    public class Singleton<ObjectType> where ObjectType : class, new()
    {
        Singleton() { }

        class SingletonCreator
        {
            //The static constructor for a class executes before any instance of the class is created.
            //The static constructor for a class executes before any of the static members for the class are referenced.
            //The static constructor for a class executes after the static field initializes (if any) for the class.
            //The static constructor for a class executes at most once during a single program instantiation.
            //A static constructor does not take access modifiers or have parameters.
            //A static constructor is called automatically to initialize the class before the first instance is created or any static members are referenced.
            //A static constructor cannot be called directly.
            //The user has no control on when the static constructor is executed in the program.
            //A typical use of static constructors is when the class is using a log file and the constructor is used to write entries to this file.

            //Explicit static constructor to tell C# compiler not to mark type as beforefieldinit 
            static SingletonCreator() { }
            // Private object instantiated with private constructor 
            internal static readonly ObjectType instance = new ObjectType();
        }

        public static ObjectType UniqueInstance
        {
            get { return SingletonCreator.instance; }
        }
    } 
}
