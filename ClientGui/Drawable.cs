using System;
using System.Numerics;
using System.Text.Json;
using AgarioModels;

namespace ClientGui;
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
/// 
/// This class represents the logic for drawing all of the game objects, It uses the XAML graphics view height and width to create our
/// viewing bounds so game objects will only be drawn if they are inside our viewing bounds.
/// 
/// </summary>
public class Drawable : IDrawable
{
    private World world;

    private readonly float scalingConstant = 25;

    /// <summary>
    /// Constructor for the drawable object that sets the world field to inputted reference
    /// </summary>
    /// <param name="world"> a reference to the World object that will determine how things are drawn </param>
    public Drawable(ref World world)
    {
        this.world = world;
    }

    /// <summary>
    /// Fills the canvas, Loops over the active playerList and foodList and draws them on our playSurface.
    /// </summary>
    /// <param name="canvas">The canvas (playSurface) to draw on</param>
    /// <param name="dirtyRect">Not used</param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Player clientPlayer = world.playerList[world.clientID];
        float viewPortWidth = clientPlayer.radius * scalingConstant;

        float leftBound = clientPlayer.X - viewPortWidth/2;
        float rightBound = clientPlayer.X + viewPortWidth/2;
        float bottomBound = clientPlayer.Y + viewPortWidth/2;
        float topBound= clientPlayer.Y - viewPortWidth/2;

        lock (world.playerList)
        {
            foreach (Player player in world.playerList.Values)
            {
                if (player.X > leftBound
                    && player.X < rightBound
                    && player.Y > topBound
                    && player.Y < bottomBound)
                {
                    float xOffset = player.objectLocation.X - leftBound;
                    float yOffset = player.objectLocation.Y - topBound;
                    float xRatio = xOffset / viewPortWidth;
                    float yRatio = yOffset / viewPortWidth;

                    canvas.FillColor = Color.FromInt(player.ARGBColor);
                    canvas.FillCircle(xRatio * world.screenWidth, yRatio * world.screenHeight, player.radius * world.screenWidth / viewPortWidth);
                    canvas.FontColor = Colors.Black;
                    canvas.DrawString(player.Name, xRatio * world.screenWidth - 50, yRatio * world.screenHeight - 50, 100, 20, HorizontalAlignment.Center, VerticalAlignment.Center);
                }
            }
        }
        lock (world.foodList)
        {
            foreach (Food food in world.foodList.Values)
            {
                if (food.X > leftBound
                    && food.X < rightBound
                    && food.Y > topBound
                    && food.Y < bottomBound)
                {
                    float xOffset = food.objectLocation.X - leftBound;
                    float yOffset = food.objectLocation.Y - topBound;
                    float xRatio = xOffset / viewPortWidth;
                    float yRatio = yOffset / viewPortWidth;

                    canvas.FillColor = Color.FromInt(food.ARGBColor);
                    canvas.FillCircle(xRatio * world.screenWidth, yRatio * world.screenHeight, food.radius * world.screenWidth / viewPortWidth);
                }
            }
        }
    }
}

