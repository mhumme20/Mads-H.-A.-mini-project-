using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseUnit : MonoBehaviour
{
   public diceroller diceroll;
   public GameObject currentTile;

   public Faction Faction;
   //Attributes
   public int BOD, AGI, REA, STR, WIL, LOG, INT, CHA, EDG, ESS, MAG;
   //(int)Math.Ceiling(originalInt / 2.0)
   //limits
   public int Physical_Limit()
   {
      double result = (((STR * 2) + BOD + REA)) / 3.0;
      return (int)Math.Ceiling(result);
   }
   public int Mental_Limit()
   {
      double result = (((LOG * 2) + INT + WIL)) / 3.0;
      return (int)Math.Ceiling(result);
   }
   public int Social_Limit()
   {
      double result = (((CHA * 2) + WIL + ESS)) / 3.0;
      return (int)Math.Ceiling(result);
   }
   //derived statistics
   public int walking_speed_rate, running_speed_rate, sprint_rate;
   public int walking_speed()
   {
      return AGI * (walking_speed_rate*item_walk_modifier);
   }
   public int running_speed()
   {
      return AGI * (running_speed_rate+item_run_modifier);
   }

   public int carry_weight()
   {
      return STR * BOD * 10;
   }

   public int max_bonus_armor()
   {
      return STR + 1;
   }

   public int max_take_aim()
   {
      return WIL / 2;
   }

   public int physical_condition()
   {
      return 8 + (BOD / 2);
   }

   public int stun_condition()
   {
      return 8 + (WIL / 2);
   }

   public int overflow()
   {
      return BOD;
   }

   public int dogde()
   {
      return INT + REA + Item_dodge_bonus+cover;
   }

   public int composure()
   {
      return WIL + CHA;
   }

   public int soak()
   {
      return BOD + armor + item_soak_bonus;
   }
   
   //Skills
   public int unarmed_combat_skill,
      Automatics_skill,
      Pistol_skill,
      Longarms_skill,
      Heavy_weapons_skill,
      Throwing_weapons_skill,
      Running_skill;
   
   //initiative
   public int Initiative_dice, Initiative_modifier, Initiative_value;
   public void roll_initiative()
   {
      diceroll = new diceroller();
      for(int i=0; i<Initiative_dice; i++)
      {
         diceroll.AddDice(6);
      }
      diceroll.Roll();
      Initiative_value = REA + INT +Initiative_modifier+ diceroll.TotalValue();
   }
   
   //gear
   public int armor, accuracy, damage, damage_type, recoil_compensation, armor_piercing, total_ammo, ammo_in_mag, item_attack_bonus, Item_dodge_bonus, item_soak_bonus, item_walk_modifier, item_run_modifier, item_sprint_modifier;

   
   //condition
   public int stun_damage, physical_damage, physical_overflow, cover, aim, weight_of_carried_items;

   public int wound_modifier()
   {
      return stun_damage / 3 + physical_damage / 3;
   }
   
   
}
