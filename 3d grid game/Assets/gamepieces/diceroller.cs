using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Dice
{
    public int sides;
    public int rollValue;

    public Dice(int sides)
    {
        this.sides = sides;
    }

    public void Roll()
    {
        rollValue = UnityEngine.Random.Range(1, sides + 1);
    }
}

public class diceroller
{
    public List<Dice> dice;

    public diceroller()
    {
        dice = new List<Dice>();
    }

    public void AddDice(int sides)
    {
        dice.Add(new Dice(sides));
    }

    public void Roll()
    {
        for (int i = 0; i < dice.Count; i++)
        {
         dice[i].Roll();   
        }
    }

    public int TotalValue()
    {
        int v=0;
        
        for (int i = 0; i < dice.Count; i++)
        {
            v += dice[i].rollValue;
        }
        
        return v;
    }

    public int succescount()
    {
        int s = 0;
        
        for (int i = 0; i < dice.Count; i++)
        {
            if (dice[i].rollValue>=5)
            {
                s++;
            }
        }

        return s;
    }
    
    public int number_of_6()
    {
        int six = 0;
        for (int i = 0; i < dice.Count; i++)
        {
            if (dice[i].rollValue==6)
            {
                six++;
            }
        }

        return six;
    }
    
    public int number_of_1()
    {
        int one = 0;
        for (int i = 0; i < dice.Count; i++)
        {
            if (dice[i].rollValue==1)
            {
                one++;
            }
        }

        return one;
    }

}
