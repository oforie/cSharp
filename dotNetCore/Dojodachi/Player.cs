using System;
namespace Dojodachi 
{
    public class Player
    {
            public int fullness { get; set;}

            public int happiness { get; set;}
        
            public int energy { get; set;}
            
            public int meals { get; set;}
            public string status { get; set;}

            public string message { get; set;}

            public Player()
            {
                fullness = 20;
                happiness = 20;
                energy = 50;
                meals = 3;
                status = "";
                message = "";
            }

            // public Player feed()
            // {
            //     Random rand = new Random();
            //     this.meals -= 1;
            //     if (rand.Next(0,4) != 0){
            //         int amt = rand.Next(5,10);
            //         this.fullness += amt;
            //         this.status = $"Your Player was just fed and is {amt} fuller";
            //     }
                
            //     this.status = $"Your Player was just fed but was pissed off by the meal!";

            // }
        
    }
}