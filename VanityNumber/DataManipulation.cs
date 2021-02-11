/*using System;
using System.Collections;
using System.Text.RegularExpressions;

public class DataManipulation
{


    public String[] Words(String[] combos)
    {

        //reads in dictionary txt file and stores each line in index inside
        //string array
        string[] dictonary = System.IO.File.ReadAllLines(@"C:\Users\Scar\VanityNumber\dictonary.txt");

        

                    //moves through array to check each index of letter combos
                    for(int n = 0; n < splitWords.Length; n++)
                    {
                        //moves through array of dictonary for each word stored
                        for(int d = 0; d < dictonary.Length; d++ )
                        {
                            //checks to see if any of the letter combos match
                            //the words from the dictonary array
                            if(splitWords[n].Contains(dictonary[d]))
                            {
                                //if a word is found then the length of the dic word
                                //is compared with the words in the top 5 array to 
                                //find which word is longer
                               for(int k = 0; k < topFive.Length; k++)
                               {
                                   //checks if word already in top 5 if not then checks length
                                   //if longer length replaces 
                                   if(topFive[k].Length < dictonary[d].Length)
                                   {
                                       Console.Write("TOP: " + k);
                                       Console.WriteLine(" DIC: " + d);
                                       Console.Write("TOP: " + topFive[k].Length);
                                       Console.WriteLine(" DIC: " + dictonary[d].Length);
                                       Console.Write("TOP: " + topFive[k]);
                                       Console.WriteLine(" DIC: " + dictonary[d]);
                                       //Console.WriteLine();
                                       if(!topFive[k].Contains(dictonary[d]))
                                       { 
                                          Console.WriteLine("Reached insert");
                                          topFive[k] = dictonary[d].ToString();
                                          Array.Sort(topFive);
                                          //Console.WriteLine("in insert: " + topFive[k]);
                                         //break;
                                       }
                                       else
                                       {
                                           Console.WriteLine("Length not Long enough");
                                       }
                                       //topFive[k] = dictonary[d].ToString(); 
                                       Console.WriteLine("Here: " + topFive[k]);
                                       //exit;
                                       //Console.WriteLine();
                                       //Console.WriteLine("Top 5: " + k + " " + topFive[k] + " Dictonary: " + dictonary[d]);
                                        //Console.WriteLine(topFive[k].Length); 
                                       
                                   }
                                   else
                                   {
                                       Console.WriteLine("Skipped");
                                   }
                                   //Console.WriteLine("Top 5: " + k + " " + topFive[k] + " Dictonary: " + dictonary[d]);
                                   //Console.WriteLine(topFive[k].Length); 
                                    
                                     
                                      // Console.WriteLine();
                                                                             
                                          
                                  // Console.Write(topFive[k]);
                               }
                               //Console.WriteLine("Here3: " + topFive[k]);
                                       Console.WriteLine();

                               
                               //Console.Write(splitWords.Length);
                               //Console.Write(dictonary[d] + "    ");
                               //Console.WriteLine(splitWords[n]);
                                
                            }
                            
                           // Console.WriteLine();
                        }

                    }
                   // Console.Write(output);
                  // Console.Write(topFive);

    }
}*/