```
Author:     Tobias R. Armstrong and Alex Thurgood
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  tobybobarm, ThurgoodAlex
Repo:       https://github.com/uofu-cs3500-spring23/assignment8agario-ms_ostrich 
Date:       11-Apr-2023,8:40 PM
Project:    ClientGui
Copyright:  CS 3500, Tobias R. Armstrong, and Alex Thurgood - This work may not be copied for use in Academic Coursework.
```

#Comments to Evaluators#
 The split command is bound to mouse/trackpad clicks rather than keyboard presses because we feel this makes more sense as far as user interaction is concerned.

 The stats on the side of the game screen move around because of the changing text lengths. We could not find an easy way to solve this problem in MAUI. With more time we would hope to fix this but it is not a game breaking bug so we did not spend too much time attempting to fix it.

 When running program on Windows the PlaySurfaces background color is not properly set and it blends in with the GUI background. Because this is not a problem on Mac and we did most of our developement on mac, we did not spend time fixing this issue. Please consider this program's functionality on mac for grading purposes.

 When trying to connect with empty name and server entries or a faulty server ip, sometimes the errors do not display on the error label. When setting a break point to see why ths was happening, the errors were displayed every time. After removing the breakpoint, errors continued to be displayed properly. This did not happen everytime so we would assume it is a MAUI bug.

#Project Purpose#
 Networking class in order to allow communication between the client and server. The GUI is updated properly upon recieveing messages from the server (in the defined format by the protocols).

#Consulted Peers#
 Rylie Gagne
 Zoe Exelbert
 James Semurad
 Discord
 Piazza


#References#
 1. Unity.com: Used to learn how to parse string into long:
     https://answers.unity.com/questions/1205763/c-converting-string-to-long.html
 2. Microsoft.com: Used to better understand lock statements:
     https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
 3. StackOverflow.com: Used to learn how to create and use a StopWatch to track client's time spent playing;
     https://stackoverflow.com/questions/14019510/calculate-the-execution-time-of-a-method
 4. Class Discord: Our most used reference for this project was the class discord which we used to look over the class's discussion of           common errors and to ask clarifying questions.