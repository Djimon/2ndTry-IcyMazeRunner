using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace IcyMazeRunner.Klassen
{
    class SaveLevel
    {

        const int e = 11;
        const int n = 221;
        const int d = 35;
        /*Zahl verschlüsseln und in einer txt Datei speichern */
        /*Verschlüsselung VZ = KZ^e mod n*/
        /*Entschlüsselung KZ = VZ^d mod n*/
        public SaveLevel(int number)
        {
            encrypt(number);
        }
        public void encrypt(int number)
        {
            ulong encrypt = (ulong)(Math.Pow(number, e)) % n;
            Console.WriteLine(encrypt);
            string output = encrypt + "";
            System.IO.File.WriteAllText(@"SaveData.txt", output);
        }
        public int decrypt()
        {
            string input;
            System.IO.StreamReader file = new System.IO.StreamReader(@"SaveData.txt");
            input = file.ReadLine();
            BigInteger tmp = new BigInteger(Convert.ToUInt64(input));
            ulong tmp2 = Convert.ToUInt64(input);
            return (int)(BigInteger.Pow(tmp, d) % new BigInteger(n));
        }

    }
}
    