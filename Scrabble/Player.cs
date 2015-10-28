using System;

public class Player
{
    // Player class member variables
    private string name;
    private LetterRack letterRack;
    private int playerID;
    private int skipCount;
    private int score;
    private bool hasTurnPriority;

    public Player(int userID, string username)
    {
        playerID = userID;
        name = username;
    }
}