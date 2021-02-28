using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleInterview.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<WordClass> WordsList = new List<WordClass>();
            string file_path = "C:/Users/hibes/source/repos/SampleInterview.ConsoleApp/SampleInterview.ConsoleApp/SampleInterview.txt";

            using (FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sw = new StreamReader(fs))
                {
                    string text = sw.ReadToEnd();
                    string[] WordsArr = text.Split(' ', '.', ',', ';', ':', '?', '\n', '\r','\t','-');
                    int minWordLength = 2;
                    //Inserting words that come from file into List and count the occurence of words
                    foreach (string item in WordsArr)
                    {
                        WordClass word = WordsList.Find(x => x.Word == item.Trim().ToLower());
                       
                        if (item.Length > minWordLength)// I count only the words that is grater than 2 in length
                        {
                            if (word == null)
                            {
                                WordsList.Add(new WordClass { Word = item, Count = 1 });
                            }
                            else
                            {
                                word.Count++;

                            }
                        }

                    }
                   
                    List<WordClass> OrderedList = WordsList.OrderByDescending(x => x.Count).ToList();// Sorting the list  from largest to small by count of words
                    int max = OrderedList.Max(x => x.Count);//Getting the largest count of words in the list
                    int count = 0;
                    Console.WriteLine("\nTotal Occurences of Words");
                    Console.WriteLine("*********************************************************\n");
                    
                    foreach (WordClass item in OrderedList)
                    {
                        //Find the third most occurence word in the list
                        if (max>item.Count && count<2)
                        {
                            max = item.Count;
                            count++;
                        }
                        Console.WriteLine("Total occurrences of {0}: {1}", item.Word, item.Count);
                    }
                    //creating a list for third most occurence word in the list
                    List<WordClass> ThirdMaxWords = new List<WordClass>();
                    foreach (WordClass item in OrderedList)
                    {
                        if (item.Count == max)
                        {
                            ThirdMaxWords.Add(new WordClass { Word = item.Word, Count = max });
                        }
                    }
                    Console.WriteLine("\nThird Most Occurence Words ");
                    Console.WriteLine("***************************************************************\n");
                    

                    foreach (WordClass item in ThirdMaxWords)
                    {
                        Console.WriteLine("Total occurrences of {0}: {1}", item.Word, item.Count);

                    }

                    //Finding palindrome words and adding them into  List
                    List<WordClass> PalindWords = new List<WordClass>();
                    foreach (WordClass item in WordsList)
                    {
                        if (item.Word.SequenceEqual(item.Word.Reverse()) )
                        {
                            PalindWords.Add(new WordClass { Word = item.Word, Count = item.Count });
                        }
                    }
                    Console.WriteLine("\nPalindrome Words");
                    Console.WriteLine("***************************************************************\n");
                    

                    foreach (WordClass item in PalindWords)
                    {

                        if (item.Count>3)
                        {
                            Console.WriteLine("Total occurrences of {0}: {1}", item.Word, item.Count); 
                        }
                    }
                    Console.WriteLine("Ordered List:{0}\nPalindrome Words:{1}", WordsList.Count, PalindWords.Count);
                    Console.WriteLine("\nThe ratio of Palindrome strings:1/{0}", Divide(WordsList.Count,PalindWords.Count));

                }
                
            }
           
            
            Console.ReadKey();
        }

        static int Divide(int a,int b)
        {
            int result = 0;
            int c = a + b;
            while (c > b)
            {
                c = c - b;
                result++;
               
            }
            return result;
        }
    }
}

  

