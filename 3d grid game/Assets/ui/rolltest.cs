using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class rolltest : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI succestext;
   [SerializeField] private GameObject second_chance_button;
   [SerializeField] private GameObject reduce_glitch_button;
   [SerializeField] private roll_result Roll_Result;
   [SerializeField] private GameObject push_the_limit_button1, push_the_limit_button2;
   [SerializeField] private GameObject plb_background;
   
   
   public int edge_stat = 5;
   public int edge_pool = 5;
   
   
   public diceroller diceroll;

   public int last_succes_count=0;
   public int original_dicepool;
   public int rolled_dicepool;
   public int dicepool;
   public bool edge_used = false;
   public int glitch_threshold;
   //0 is no glitch, 1 is a normal glitch, 2 is a critical glitch
   public int glitch_lvl = 0;
   public bool pre_edge = false;
   public int six_count = 0;
   public int Limit = 7;

   public int exsplotion = 0;
   
   private void Start()
   {
      last_succes_count = 0;
      succestext.text = dicepool.ToString()+" Dice";
      second_chance_button.SetActive(false);
      reduce_glitch_button.SetActive(false);
      glitch_threshold = (dicepool / 2)+1;
      edge_used = false;
      pre_edge = false;
      rolled_dicepool = original_dicepool;
      dicepool = original_dicepool;
      
      
   }

   public void pre_edge_active()
   {
      pre_edge = true;
      rolled_dicepool = original_dicepool + edge_stat;
      succestext.text = rolled_dicepool.ToString()+"! Dice";
   }
   public void pre_edge_notactive()
   {
      pre_edge = false;
      rolled_dicepool = original_dicepool;
      succestext.text = rolled_dicepool.ToString()+" Dice";
   }
   
   public void Roll()
   {
      exsplotion = 0;
      diceroll = new diceroller();
      last_succes_count = 0;
      second_chance_button.SetActive(false);
      reduce_glitch_button.SetActive(false);
      glitch_lvl = 0;
      edge_used = false;
      dicepool = rolled_dicepool;
      glitch_threshold = (dicepool / 2)+1;
      for(int i=0; i<dicepool; i++)
      {
         diceroll.AddDice(6);
      }
      diceroll.Roll();
      last_succes_count = diceroll.succescount();
      six_count = diceroll.number_of_6();
      
      if (pre_edge == true)
      {
         edge_pool -= 1;
         edge_used = true;
         check_for_sixes();
      }
      else
      {
         UpdateText();
      }
   }

   public void second_chance()
   {
      if (edge_used == false)
      {
         diceroll = new diceroller();
         int newpool = dicepool -= last_succes_count;
        for(int i=0; i<newpool; i++)
         {
            diceroll.AddDice(6);
         }

         edge_used = true;
         diceroll.Roll();
         last_succes_count += diceroll.succescount();
         second_chance_button.SetActive(false);
         edge_pool -= 1;
         UpdateText();
      }
      
      
      
   }

   public void Reduce_glitch()
   {
      glitch_lvl -= 1;

      if (glitch_lvl == 1)
      {
         succestext.text = "Glitch! Number of Success's "+diceroll.succescount().ToString();
         
      }
      else if (glitch_lvl == 0)
      {
         succestext.text = "Number of Success's "+diceroll.succescount().ToString();
         
      }
      reduce_glitch_button.SetActive(false);
      edge_pool -= 1;
      edge_used = true;
   }

   public void commint_roll()
   {
      Roll_Result.set_net_hits(last_succes_count, glitch_lvl);
   }
   
   
   private void UpdateText()
   {
      
      if (diceroll.number_of_1() >= glitch_threshold && last_succes_count == 0)
      {
         succestext.text = "Critical Glitch!!!";
         glitch_lvl = 2;
         if (edge_used == false)
         {
            reduce_glitch_button.SetActive(true);
         }
      }
      
      else if (diceroll.number_of_1()>= glitch_threshold)
      {
         if(last_succes_count>=Limit && pre_edge==false)
         {
            succestext.text = "Glitch! Number of Success's "+last_succes_count.ToString()+" Limit reach only "+Limit.ToString()+" success´s count";
         }
         else
         {
            succestext.text = "Glitch! Number of Success's "+last_succes_count.ToString();
         }
         
         glitch_lvl = 1;
         if (edge_used == false)
         {
            reduce_glitch_button.SetActive(true);
         }
                      
      }
      else
      {
         if(last_succes_count>=Limit && pre_edge==false)
         {
            succestext.text = "Number of Success's "+last_succes_count.ToString()+" Limit reach only "+Limit.ToString()+" success´s count";
         }
         else
         {
            succestext.text = "Number of Success's "+last_succes_count.ToString();
         }
         
         if (edge_used == false)
         {
            second_chance_button.SetActive(true);
         }

         check_edge();
      }

      
      
     
     //for push the limit, create a function that add the edge value to the dicepool, then create a corutine that will activate if the number of sixes exceed zero and
     //then rolls a dicepool equal to the number of sixes, then restarts until the number of sixes is zero
   }
   private void check_for_sixes()
   {
      if (six_count>=1)
      {
         StartCoroutine(push_the_limit());
      }
      else if (six_count == 0)
      {
         UpdateText();
      }

   }
   
   IEnumerator push_the_limit()
   {
      diceroll = new diceroller();
      for(int i=0; i<six_count; i++)
      {
         diceroll.AddDice(6);
      }

      six_count = 0;
      diceroll.Roll();
      last_succes_count += diceroll.succescount();
      six_count = diceroll.number_of_6();
      exsplotion++;
      UpdateText();
      yield return new WaitForSeconds(1.0f);
      check_for_sixes();
   }


   public void check_edge()
   {
      if (edge_pool <= 0)
      {
         second_chance_button.SetActive(false);
         reduce_glitch_button.SetActive(false);
         push_the_limit_button1.SetActive(false);
         push_the_limit_button2.SetActive(false);
         plb_background.SetActive(false);
      }

      if (edge_pool>=1)
      {
         plb_background.SetActive(true);
      }
   }
   
}
