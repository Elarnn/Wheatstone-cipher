using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wheatstone_cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input text:");
            string message = Console.ReadLine().ToUpper().Replace(" ", "");
            message = Regex.Replace(message, @"[\p{P}\s]", ""); // Remove all punctuation symbols

            Console.WriteLine("Write first keyword:");
            string keyword_1 = Console.ReadLine().ToUpper();
            Console.WriteLine("Write second keyword:");
            string keyword_2 = Console.ReadLine().ToUpper();

            if (keyword_1.Any(char.IsDigit) || keyword_2.Any(char.IsDigit)) throw new InvalidOperationException("Keywords shouldn't contain digits");

            char[,] matrix_1 = GenerateMatrix(keyword_1);
            char[,] matrix_2 = GenerateMatrix(keyword_2);
            DisplayMatrix(matrix_1);
            DisplayMatrix(matrix_2);

            string result = Encrypt_Wheatstone(matrix_1, matrix_2, message);
            Console.WriteLine("Result text: " + result);
            Console.ReadKey();
        }
            static string Encrypt_Wheatstone(char[,] matrix_1, char[,] matrix_2, string message)
            {
                char[,] msg = SplitStringInto2DArray(message, 2); // Split the message into pairs of characters

                char[,] encrypt_msg = new char[msg.GetLength(0), 2];
                int index = 0;
                int row1 = -1, col1 = -1, row2 = -1, col2 = -1;

                for (int i = 0; i < matrix_1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix_1.GetLength(1); j++)
                    {
                        if (index > msg.GetLength(0) - 1) break;
                        if (matrix_1[i, j] == msg[index, 0])
                        {
                            row1 = i;
                            col1 = j;
                        }
                        if (matrix_2[i, j] == msg[index, 1])
                        {
                            row2 = i;
                            col2 = j;
                        }
                        if (row1 != -1 && row2 != -1)
                        {
                            if (col1 == col2)
                            {
                                encrypt_msg[index, 0] = matrix_1[row2, col2];
                                encrypt_msg[index, 1] = matrix_2[row1, col1];
                            }
                            else
                            {
                                encrypt_msg[index, 0] = matrix_1[row1, col2];
                                encrypt_msg[index, 1] = matrix_2[row2, col1];
                            }
                            row1 = -1; col1 = -1; row2 = -1; col2 = -1;
                            i = 0; j = 0;
                            index++;
                        }

                    }
                }
                return string.Concat(encrypt_msg.Cast<char>());
            }
            static char[,] GenerateMatrix(string keyword)
            {
                char[,] matrix = new char[5, 5];
                string alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYZ"; // English alphabet without the letter Q
            string key = "";

            // Filling the keyword
                int keywordIndex = 0;
                foreach (char letter in keyword)
                {
                    if (!key.Contains(letter.ToString()) && letter != 'Q')
                    {
                        key += letter;
                        matrix[keywordIndex / 5, keywordIndex % 5] = letter;
                        keywordIndex++;
                    }
                }

            // Filling the remaining cells of the matrix
                foreach (char letter in alphabet)
                {
                    if (!key.Contains(letter.ToString()) && letter != 'Q')
                    {
                        matrix[keywordIndex / 5, keywordIndex % 5] = letter;
                        keywordIndex++;
                    }
                }

                return matrix;
            }
        // Splits a string into a 2D character array with a specified number of columns
        static char[,] SplitStringInto2DArray(string input, int numCols)
        {
            // Determine the dimensions of the array
            int numRows = (int)Math.Ceiling((double)input.Length / numCols);

            // Create a 2D array
            char[,] result = new char[numRows, numCols];

            // Fill the 2D array
            int index = 0;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (index < input.Length)
                    {
                        result[i, j] = input[index];
                        index++;
                    }
                    else
                    {
                        // If the string length is not divisible by numCols, add a space at the end
                        result[i, j] = ' ';
                    }
                }
            }
            return result;
        }

        static void DisplayMatrix(char[,] matrix)
            {
                int numRows = matrix.GetLength(0);
                int numCols = matrix.GetLength(1);

                for (int row = 0; row < numRows; row++)
                {
                    for (int col = 0; col < numCols; col++)
                    {
                        Console.Write(matrix[row, col] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
