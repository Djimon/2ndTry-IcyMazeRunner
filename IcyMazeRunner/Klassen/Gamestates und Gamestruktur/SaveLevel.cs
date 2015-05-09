using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    static class SaveLevel
    {
        const int e = 11;
        const int n = 221;
        const int d = 35;
        /*Zahl verschlüsseln und in einer txt Datei speichern */
        /*Verschlüsselung VZ = KZ^e mod n*/
        /*Entschlüsselung KZ = VZ^d mod n*/
        public void encrypt(int number)
        {
            long encrypt = number ^ e % n;
            string output = encrypt + "";
            System.IO.File.WriteAllText(@"SaveData.txt", output);
        }
        public int decrypt()
        {
            string input;
            System.IO.StreamReader file = new System.IO.StreamReader(@"SaveData.txt");
            input = file.ReadLine();
            int tmp = Convert.ToInt32(input);
            return tmp ^ d % n;
        }
            
    }
}
    