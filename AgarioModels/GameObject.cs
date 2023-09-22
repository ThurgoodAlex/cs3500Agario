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
/// This creates a GameObject that holds and sets the field used by both the Food class and Player class. 
/// Some of these fields include the ID, the location, the color, the mass, and calculates the radius.
/// </summary>
public class GameObject
{
    public long ID { get; private set;}

    public Vector2 objectLocation { get; private set; }

    public float X { get; }

    public float Y { get; }

    public int ARGBColor { get; private set; }

    public float Mass { get; private set; }

    public float radius { get; private set; }

    /// <summary>
    /// Constructor for a GameObject object that sets the object's X and Y locations, color, ID, mass, and radius.
    /// </summary>
    /// <param name="X"> The X location of the game object </param>
    /// <param name="Y"> The Y location of the game object </param>
    /// <param name="ARGBColor"> The ARGBColor representation of the game object's color </param>
    /// <param name="ID"> The ID of the Game Object </param>
    /// <param name="Mass"> The mass of the game object </param>
    public GameObject(float X, float Y, int ARGBColor, long ID, float Mass)
    {
        objectLocation = new Vector2(X, Y);
        this.X = objectLocation.X;
        this.Y = objectLocation.Y;
        this.ARGBColor = ARGBColor;
        this.ID = ID;
        this.Mass = Mass;
        this.radius = (float)Math.Sqrt(Mass / Math.PI);
    }
}

