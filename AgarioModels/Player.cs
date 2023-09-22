﻿using System;
using System.Numerics;

namespace AgarioModels;

/// <summary>
/// Author:    Alex Thurgood and Toby Armstrong
/// Date:      April 11, 2023
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500, Alex Thurgood, and Toby Armstrong - This work may not 
///            be copied for use in Academic Coursework.
///
/// Toby Armstrong and Alex Thurgood, certify that we wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// This class is used to create a Player object that is a child of the GameObject class. 
/// This uses the GameObject's base constructor, but also has an added name field, and is used in the deserialization process. 
/// </summary>
public class Player : GameObject
{
    public string Name { get; private set; }

    /// <summary>
    /// Constructor for a player object that calls the GameObjects constructor and sets the inputted name.
    /// </summary>
    /// <param name="Name"> The name to be displayed above the player object </param>
    /// <param name="X"> The X location of the player object </param>
    /// <param name="Y"> The Y location of the player object </param>
    /// <param name="ARGBColor"> The ARGB representation of the object's color </param>
    /// <param name="ID"> The ID unique to the player object </param>
    /// <param name="Mass"> The mass/area of the player object </param>
    public Player(string Name,float X, float Y, int ARGBColor, long ID, float Mass) : base(X, Y, ARGBColor, ID, Mass)
    {
        this.Name = Name;
    }
}


