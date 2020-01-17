using System;

namespace CheckLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            int start = 0;
            int val;
            Console.WriteLine(start);
            Console.WriteLine("Press the spacebar to collect");
            Console.WriteLine("Press X to quit");
            while (true)
            {
                ConsoleKeyInfo user = Console.ReadKey();
                Console.Clear();

                if (user.Key == ConsoleKey.Spacebar)
                {
                    val = start += 1;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if (user.Key == ConsoleKey.W)
                {
                    val = start += 2;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if(user.Key == ConsoleKey.E)
                {
                    val = start += 5;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if(user.Key == ConsoleKey.R)
                {
                    val = start += 10;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if (user.Key == ConsoleKey.A)
                {
                    val = start += 12;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if (user.Key == ConsoleKey.S)
                {
                    val = start += 15;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if (user.Key == ConsoleKey.D)
                {
                    val = start += 20;
                    Console.Clear();
                    Console.WriteLine(val);
                }
                else if (user.Key == ConsoleKey.X)
                {
                    val = start;
                    Console.Clear();
                    Console.WriteLine("Thanks for playing! Your final score is: " + val);
                    break;
                }
                else
                {
                    Console.WriteLine("You broke the game! Happy!?");
                    break;
                }
            }

         } 

            
        }
    }

