using System;
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
    /// 
    /// </summary>
    public class World
	{
		public readonly int worldHeight = 5000;

        public readonly int worldWidth = 5000;

        public float screenHeight;

        public float screenWidth;

        public Dictionary<long, Player> playerList;

		public Dictionary<long, Food> foodList;

        public long clientID;

        /// <summary>
        /// Constructor for a World object that instantiates the food and player dictionaries. 
        /// </summary>
        public World()
		{
			playerList = new Dictionary<long, Player>();
			foodList = new Dictionary<long, Food>();
		}
	}