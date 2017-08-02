using System;
using System.Collections.Generic;

namespace collections_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");  
            int[] myarr1 = new int[10] {0,1,2,3,4,5,6,7,8,9};
            string[] myarr2 = new string[4] {"Tim", "Martin", "Nikki", "Sarah"};
            bool[] myarr3 = new bool[10];
            for(int i=0; i<10; i+=2){
                myarr3[i] = true;
            }

            int[,] multip = new int[10,10];
            for(int x=0; x<10; x++){
            for(int y=0; y<10; y++){
                multip[x, y] = (x+1) * (y+1);
            }
        }
        //multiplication table
        
        }
    }
       

}




    
