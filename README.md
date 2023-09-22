```
Author:     Tobias R. Armstrong and Alex Thurgood
Start Date: 9-April-2023
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  tobybobarm, ThurgoodAlex
Repo:       https://github.com/uofu-cs3500-spring23/assignment8agario-ms_ostrich
Commit Date:April 17, 2023, 8:40 PM
Solution:   Agario
Copyright:  CS 3500, Tobias R. Armstrong and Alex Thurgood - This work may not be copied for use in Academic Coursework.
```

#Overview of Functionality#
 Welcome to Agar.Two, a new way to enjoy the Agario experience! This game is a more fast paced and interactive version of the Agario you know and love. Player movement speed is greatly increased from the original game in order to encourage a more fast paced and exciting experience. Players are also gradually slowed down if their mouse is not moved to force players to be making movement decisions at all times. This effectively keeps the user more alert and invested in the game, hopefully bringing in more revenue for VC Jim! Finally, the split command propels the player much further than in the original game to account for the increased speed of the game. Have fun and be safe!

 This solution provides the required data and methods for a fully functioning Agario client user interface. The two main projects required for the defined functionality are the AgarioModels project, which contains all of the data regarding in game models such as food items, players, the worlds current state, and the protocols defining how the client and server communicate, and the ClientGui project, which contains the UI code including all of the necessary methods and data regarding client connections, client disconnections, client mouse movements, client button presses, and the displaying/drawing of start, game, and end screens.

 Start Screen:
    The start screen currently has an entry for the player's name, an entry for the server they would like to connect to, a "Connect to Server" Button, and a label displaying any errors that may occur. Upon entering a name and valid server, the user may press the connect button to try and connect to a running server. If the connection is successful, the start screen will be hidden and the game screen will appear. If the connection is unsuccessful, a prompt in the error label will appear instructing the user to check both entries and try again.

 Game Screen:
    The amount of the world that the game screen shows is based off of the player's radius. This is so we can only draw game objects that are in the game bounds, so the server doesn't draw unnecessary objects. This effectively zooms the screen out as the player grows in mass/radius. As the player moves, the screen stays centered around them and game objects will start to appear if they are within the specified bounds determined by the player's radius/mass. On the right hand side of the game screen, We display stats such as the current mass, radius, player location, the number of food objects, and the number of players. If the player's mass is above 1000, they are able to click the mouse to split themselves in half and propel half of themselves in the direction of the mouse pointer. We decided to bind this action to mouse clicks instead of spacebar presses because the user will already be using the mouse/trackpad to move so it would be easier to send a split command using the mouse rather than another button on the computer.

 End Screen:
    If a player dies (is eaten by another player with a bigger mass than them) a game over screen is displayed on top of the game screen. This screen notifies the user of the total time they were alive, their final mass, and their final radius. It also gives them 2 buttons: one to respawn and continue playing on the same server and one to disconnect and return to the start screen. 

#Partnership Information#
 Almost all work was done together either in person or over zoom. Some minor additions/changes were made individually, such as adding a dispatcher or adding documentation, but no substantial work that would require a new branch was done individually.

#Branching#
 Almost all work was done together, so no branches were created for individual progress. The only branch outside of the master branch that was created was made in order to fix some discrepancies between both local repos and the remote repo.

#Testing#
 We tested this solution by making sure the game functioned properly through GUI inspection. This included editing code, rerunning the program, and connecting to the server in order to observe how the program acted. Using the debugger as well as observation, we were able to work out all known bugs and arrive at our final solution.

 We believe that drawing the scene is more of a bottle neck than the network data in the project because of the sheer amount of items that must be looped through and drawn. In order to draw the scene correctly, each game object must be checked to see if it is within the bounds of the screen. As the game progresses, the bounds get bigger and more items must actually be drawn, putting more strain on the run time. Although tons of networking data is getting sent every second, they are only strings and can be deserialized quite efficiently if needed. Overall we believe that the computational requirements of the drawing functionality are more taxing than the networking data, and are thus more of a bottleneck.

#Time Tracking (Personal Software Practice)#
 ESTIMATED TIME: 18 HRS
 TRACKED TIME:
    April 9:  2 HRS
    April 10: 1 HRS
    April 11: 2 HRS
    April 12: 2 HRS
    April 13: 5 HRS
    April 14: 2 HRS
    April 15: 3 HRS
    April 16: 1 HRS
    April 17: 5 HRS

        Effective time spent: 13 HRS
        Debugging time spent: 6  HRS
        Learning time spent:  4  HRS

        Time spend as a team: 21 HRS
        Alex individual time: 1  HRS
        Toby Individual time: 1  HRS

        TOTAL TIME SPENT: 23 HRS

 Reflection:
 Our time estimates seem to be getting better and better as we complete more assignments. We accounted for some roadblocks with our estimate for this project because our estimates tended to be less than our actual time spent in the past. This project took about 4 hours longer than our estimate because we spent a lot more time focusing on good software practices and producing a product that we were both proud of. Overall we are both excited about the final product and may even consider added more functionality after the completion of this assignment.