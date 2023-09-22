using System.Text.Json;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using AgarioModels;
using System.Diagnostics;
using Logger;
using Microsoft.Extensions.Logging;

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
/// This class handles connections and disconnections from the server, it handles grabbing your clients cursor position on the play
/// surface and translating those into the corresponding world coordinates. It listens for messages(protocols) being sent from the
/// server to the client and depending on which message is sent. For example if the incoming message from the server is
/// "CMD_Update_Players", then a list of all players with updated location,mass,etc will be sent to the client.
/// </summary>
public partial class MainPage : ContentPage
{
    private Networking clientNetwork;
    private World worldModel;
    private int x;
    private int y;
    private List<Food> foodlist = null;
    private List<Player> listOfPlayers = null;
    private string playerName;
    private Stopwatch watch;
    private FileLogger logger;

    /// <summary>
    /// Constructor for initializing a main page for the client GUI.
    /// </summary>
    public MainPage()
    {
        logger = new FileLogger("AgarioLogger");
        InitializeComponent();
        clientNetwork = new Networking(logger, onConnect, onDisconnect, onMessage, '\n');
        worldModel = new World();
        PlaySurface.Drawable = new Drawable(ref worldModel);
    }

    /// <summary>
    /// Event handler for when the "Connect to Server" button is pressed. Connects the client based on the inputted server name and
    /// awaits messages.
    /// </summary>
    /// <param name="sender"> The server that this message is going to </param>
    /// <param name="e"> Event handler for clicking the Connect to Server button </param>
    private void ConnectButtonClicked(object sender, EventArgs e)
    {
        if (ServerEntry.Text != "" && NameEntry.Text != "")
        {
            try
            {
                clientNetwork.Connect(ServerEntry.Text, 11000);
                clientNetwork.ClientAwaitMessagesAsync();

                logger.LogInformation("Client has connected!");
                WelcomeScreen.IsVisible = false;
                GameScreen.IsVisible = true;
                worldModel.screenWidth = (float)PlaySurface.WidthRequest;
                worldModel.screenHeight = (float)PlaySurface.HeightRequest;

            }
            catch (Exception)
            {
                logger.LogDebug("Client connection failed!");
                Dispatcher.Dispatch(() => ErrorDisplay.Text = "ERROR: Failed to connect, please check server name/ip and try again.");
            }

        }
        else
        {
            Dispatcher.Dispatch(() => ErrorDisplay.Text = "ERROR: Please enter a name and valid server before attempting to connect.");
        }

    }

    /// <summary>
    /// Updates the client GUI correctly and updates all related data (i.e. clients player and food lists).
    /// </summary>
    /// <param name="channel"> The Networking object being connected </param>
    private void onConnect(Networking channel)
    {
        this.playerName = NameEntry.Text;
        clientNetwork.Send(String.Format(Protocols.CMD_Start_Game, this.playerName));
        logger.LogDebug("Client has sent start game command.");
        watch = Stopwatch.StartNew();
    }

    /// <summary>
    /// Updates the client GUI correctly and clears all related data (i.e. clients player and food lists).
    /// </summary>
    /// <param name="channel"> The Networking object being disconnected </param>
    private void onDisconnect(Networking channel)
    {
        logger.LogInformation("Client has disconnected!");
        WelcomeScreen.IsVisible = true;
        GameScreen.IsVisible = false;
    }

    /// <summary>
    /// Grabs ands sets the X and Y to the current location of the cursor.
    /// </summary>
    /// <param name="sender"> The cursor pointer</param>
    /// <param name="e">the event to get the location when the cursor is moved</param>
    private void PointerPosition(Object sender, PointerEventArgs e)
    {
        if (worldModel.playerList.ContainsKey(worldModel.clientID))
        {
            Point? p = e.GetPosition(PlaySurface);
            this.x = ((int)p.Value.X - (int)worldModel.screenWidth / 2) + (int)worldModel.playerList[worldModel.clientID].X;
            this.y = ((int)p.Value.Y - (int)worldModel.screenHeight / 2) + (int)worldModel.playerList[worldModel.clientID].Y;
        }
    }

    /// <summary>
    /// Sends the split command to the server with the worlds x and y coordinates
    /// </summary>
    /// <param name="sender"> the tap gesture (mousepad) </param>
    /// <param name="e"> The event that gets triggered on the tap gesture</param>
    private void OnTap(Object sender, TappedEventArgs e)
    {
        clientNetwork.Send(String.Format(Protocols.CMD_Split, this.x, this.y));
        logger.LogDebug($"Client has split to ({this.x}, {this.y})");
    }


    /// <summary>
    /// Checks and deals with incoming messages and protocols accordingly. Properly updates client GUI and data if needed.
    /// </summary>
    /// <param name="channel"> The Networking object sending the message </param>
    /// <param name="message"> The incoming message (most likely in the form of a defined protocol) </param>
    private async void onMessage(Networking channel, string message)
    {
        try
        {
            if (message.StartsWith(Protocols.CMD_Player_Object))
            {
                worldModel.clientID = long.Parse(message[Protocols.CMD_Player_Object.Length..]);
                logger.LogInformation($"Client ID has been set to {worldModel.clientID}");
            }

            else if (message.StartsWith(Protocols.CMD_Food))
            {
                Console.WriteLine(message);
                foodlist = JsonSerializer.Deserialize<List<Food>>(message[Protocols.CMD_Food.Length..]);
                logger.LogTrace("Update Food protocol deserialized");
                if (foodlist == null)
                    return;
                else
                {
                    lock (worldModel.foodList)
                    {
                        foreach (Food foodItem in foodlist)
                        {
                            worldModel.foodList.TryAdd(foodItem.ID, foodItem);
                        }
                    }
                }
            }

            else if (message.StartsWith(Protocols.CMD_Dead_Players))
            {
                string messageWithoutCommand = message[(Protocols.CMD_Dead_Players.Length + 1)..(message.Length - 1)];
                string[] idList = messageWithoutCommand.Split(',');

                logger.LogDebug($"{idList.Length} players have died");

                if (idList[0] != "")
                {
                    List<long> idArray = new List<long>(idList.Select(long.Parse).ToList());
                    if (idArray.Contains(worldModel.clientID))
                    {
                        watch.Stop();
                        if (await DisplayAlert("Game Over!", $"GAME STATS:\n Time Alive: {watch.Elapsed.Seconds} seconds\n Final Mass: {MassLabel.Text.Substring(6)}\n Final Radius: {RadiusLabel.Text.Substring(8)}", "Play Again", "Disconnect"))
                        {
                            clientNetwork.Send(String.Format(Protocols.CMD_Start_Game, this.playerName));
                            watch = Stopwatch.StartNew();
                        }
                        else
                        {
                            clientNetwork.Disconnect();
                        }
                    }
                }

                for (int i = 0; i < idList.Length; i++)
                {
                    if (long.TryParse(idList[i], out long id))
                    {
                        lock (worldModel.playerList)
                        {
                            worldModel.playerList.Remove(id);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            else if (message.StartsWith(Protocols.CMD_Eaten_Food))
            {
                string messageWithoutCommand = message[(Protocols.CMD_Eaten_Food.Length + 1)..(message.Length - 1)];
                string[] idList = messageWithoutCommand.Split(',');
                if (idList.Length == 0)
                    return;
                for (int i = 0; i < idList.Length; i++)
                    if (long.TryParse(idList[i], out long id))
                    {
                        lock (worldModel.foodList)
                        {
                            worldModel.foodList.Remove(id);
                        }
                    }
                    else
                    {
                        return;
                    }
            }

            else if (message.StartsWith(Protocols.CMD_HeartBeat))
            {
                clientNetwork.Send(String.Format(Protocols.CMD_Move, this.x, this.y));
                PlaySurface.Dispatcher.Dispatch(PlaySurface.Invalidate);
                if (worldModel.playerList.ContainsKey(worldModel.clientID))
                {
                    Dispatcher.Dispatch(() => MassLabel.Text = $"Mass: {worldModel.playerList[worldModel.clientID].Mass}");
                    Dispatcher.Dispatch(() => RadiusLabel.Text = $"Radius: {worldModel.playerList[worldModel.clientID].radius}");
                    Dispatcher.Dispatch(() => LocationLabel.Text = $"Current Location: ({(int)worldModel.playerList[worldModel.clientID].X}, {(int)worldModel.playerList[worldModel.clientID].Y})");
                    Dispatcher.Dispatch(() => PlayerNumberLabel.Text = $"Number of Players: {worldModel.playerList.Count}");
                    Dispatcher.Dispatch(() => FoodNumberLabel.Text = $"Food in World: {worldModel.foodList.Count}");
                }
            }

            else if (message.StartsWith(Protocols.CMD_Update_Players))
            {
                this.listOfPlayers = JsonSerializer.Deserialize<List<Player>>(message[Protocols.CMD_Update_Players.Length..]);
                logger.LogTrace("Update Players protocol deserialized");
                if (listOfPlayers == null)
                    return;
                else
                {
                    lock (worldModel.playerList)
                    {


                        foreach (Player player in listOfPlayers)
                        {
                            worldModel.playerList[player.ID] = player;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            clientNetwork.Disconnect();
        }
    }

}

