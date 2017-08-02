using System;

namespace fundamentals_1
{
    class Program
    {
        static void Main(string[] args)
        {   
            // first_loop(1,255);
            // second_loop(1, 100);
            third_loop(1, 100);
            Console.WriteLine("Hello World!");
        }

        static void first_loop(int start, int end){
            for (int i=start; i<=end; i++){
                Console.WriteLine(i);
            }

        }

        static void second_loop(int start, int end){
            for (int i=start; i<=end; i++){
                if (i % 15 == 0){
                    continue;
                } else if  (i % 3 == 0 || i % 5 == 0) {
                Console.WriteLine(i);
                }
            }

        }

     static void third_loop(int start, int end){
            for (int i=start; i<=end; i++){
                if (i % 15 == 0){
                    Console.WriteLine("FizzBuzz");
                } else if  (i % 3 == 0){
                    Console.WriteLine("Fizz");
                } else if (i % 5 == 0){
                    Console.WriteLine("Buzz");
                }
            }

        }
    }
}
