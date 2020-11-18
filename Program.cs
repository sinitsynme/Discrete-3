using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        
        //создал словарь, подробнее в методе CreateCards
        private static Dictionary<int, string> _cards = new Dictionary<int, string>(); 
        
        private static int count = 0; //считает подходящие сочетания
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //даёт вывести символы крестушек/пикушек...
            CreateCards(ref _cards);
            int[] fiveCards = new int[5]; //для ключей карт, вошедших в сочетание

            Rec(0,0,ref fiveCards);
            Console.WriteLine();
            Console.WriteLine("Число сочетаний равно " + count);
            Console.ReadLine();
        }


        static void Rec(int idx, int last, ref int[] fiveCards)
        {
            if (idx == 5)
            {
                if (Test(fiveCards))
                {
                    count++;
                    //Out(fiveCards, _cards); !!!! раскомментировать для вывода всех нужных сочетаний на консоль
                    //однако, если у вас очень много сочетаний, вывод будет очень долгим!!!!
                }
                return;
            }

            for (int i = last + 1; i <= 36;i++)
            {
                fiveCards[idx] = i;
                Rec(idx+1, i, ref fiveCards);
                
            }
            
        } //рекурсивный перебор - не трогаем!!!
        

        static void CreateCards(ref Dictionary<int, string> cards)
        {
            /*
                Ребятки, этот метод не трогаем, он нам генерирует библиотеку всех существующих
                36 карт. 
                Библиотека - структура данных, которая хранит в себе данные в виде
                таких пар, как "ключ-значение". По ключу из словаря мы можем доставать значения, обращаемся как к массиву
                К примеру, вызвав команду cards[1] мы получим 6♥, cards[2] нам выдаст 6♦ и так далее.
                
                ---СПИСОК КЛЮЧЕЙ И КАРТ---
                
                key - value
                1 - 6♥
                2 - 6♦
                3 - 6♣
                4 - 6♠
                5 - 7♥
                6 - 7♦    (cards[6] выведет 7♦)...
                7 - 7♣
                8 - 7♠
                9 - 8♥
                10 - 8♦
                11 - 8♣
                12 - 8♠
                13 - 9♥
                14 - 9♦
                15 - 9♣
                16 - 9♠
                17 - 10♥
                18 - 10♦
                19 - 10♣
                20 - 10♠    (cards[20] выведет 10♠)...
                21 - J♥
                22 - J♦
                23 - J♣
                24 - J♠
                25 - Q♥ 
                26 - Q♦ 
                27 - Q♣ 
                28 - Q♠ 
                29 - K♥
                30 - K♦
                31 - K♣
                32 - K♠
                33 - A♥ 
                34 - A♦ 
                35 - A♣ 
                36 - A♠ 
                
                !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                определить масть по ключу:
                метод Suit(int key)
                
                определить достоинство карты по ключу:
                метод Value(int key)
                
                
            */
            int count = 1;
            for (int i = 1; i <= 10; i++)
            {
                string s;
                if (i > 0 && i < 6)
                {
                    s = (i + 5).ToString();
                }

                else if (i == 6)
                {
                    s = "J";
                }

                else if (i == 7)
                {
                    s = "Q";
                }

                else if (i == 8)
                {
                    s = "K";
                }
                else
                {
                    s = "A";
                }
                
                cards.Add(count++, s+"♥");
                cards.Add(count++, s+"♦");
                cards.Add(count++, s+"♣");
                cards.Add(count++, s+"♠");
                
            }
        }

        static void Out(int[] s, Dictionary<int, string> cards) //выводит карты
        {
            foreach (int key in s)
            {
                Console.Write(cards[key] + " ");
            }
            Console.WriteLine();
        }

        static string Suit(int key)
        {
            var convert = new Dictionary<int, string>()
            {
                [0] = "♠", [1] = "♥", [2] = "♦", [3] = "♣"
            };
            return convert[key % 4];
        }

        static string Value(int key)
        {
            if (key >= 1 && key <= 4)
            {
                return "6";
            }
            else if (key >= 5 && key <= 8)
            {
                return "7";
            }
            else if (key >= 9 && key <= 12)
            {
                return "8";
            }

            else if (key >= 13 && key <= 16)
            {
                return "9";
            }
            else if (key >= 17 && key <= 20)
            {
                return "10";
            }
            else if (key >= 21 && key <= 24)
            {
                return "J";
            }
            else if (key >= 25 && key <= 28)
            {
                return "Q";
            }
            else if (key >= 29 && key <= 32)
            {
                return "K";
            }
            else
            {
                return "A";
            }
        }
        
        //Нужно переписать под себя только метод Test()!
        //-------------------------------------------------------------------------------
        /*ПРИМЕР АЛГОРИТМА
         12 вар -  Нужно, чтобы ТОЧНО были 2 дамы, 2 туза, 1 карта пиковой масти
         то есть надо проверить массив ключей на то, что у нас есть 2 непиковых дамы, 2 непиковых туза 
         и 1 пикушка
        */
        static bool Test(int[] fiveCards)
        {
            int queens = 0;
            int spades = 0; //создаю три интовых переменных, чтобы можно было отслеживать
            int aces = 0; // число дам, тузов, пикушек  
            foreach (int key in fiveCards)
            {
                if (Value(key) == "Q")
                {
                    queens++;
                }

                if (Value(key) == "A")
                {
                    aces++;
                }

                if (Suit(key) == "♠")
                {
                    spades++;
                }
            }

            return (queens == 2 && spades == 1 && aces == 2);
        }

    }
}
