using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class roll_result : MonoBehaviour
{
    public int hits;
    public int net_hits;
    public int glitch_lvl;
    public int thresshold;
    public bool opposed_roll = false;
    public int opposed_dice = 0;
    public diceroller diceroll;


    public void set_net_hits(int succes, int glitch)
    {
        hits = succes;
        glitch_lvl = glitch;
    }

    public int get_net_hits()
    {
        net_hits = hits - thresshold;
        return net_hits;
    }

    public void set_op_dicepool(int dice)
    {
        opposed_dice = dice;
    }
    
    public int get_glitch_lvl()
    {
        return glitch_lvl;
    }

    public bool check_succes()
    {
        if (opposed_roll == true)
        {
            roll_op_dice();
        }

        if (hits >= thresshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void roll_op_dice()
    {
        diceroll = new diceroller();
        for(int i=0; i<opposed_dice; i++)
        {
            diceroll.AddDice(6);
        }
        diceroll.Roll();
        thresshold = diceroll.succescount();
        
        
    }



}
