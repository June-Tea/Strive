//This static class keeps track of the players scores so that they can be passed to the scoreboard
public static class ScoreHolder
{
    //how far the player was when they died or 7777777 if the player won
    public static float playerDistance = 0;
    //checks how quickle the player made their progress
    public static float playerTime = 0f;

    //allows the player's distance to changed to a set amount
    public static void setDistance(float distance){
        playerDistance = distance;
    }
    //allows the player's time to changed to a set amount
    public static void setTime(float time){
        playerTime = time;
    }

}
