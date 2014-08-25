namespace Anagram
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            const string WORDSFILE = @"c:\test\_wordlist";
            const string PHRASE = "poultry outwits ants";
            const string MD5HASCODE = "4624d200580677270a54ccff86b9610e";

            var logList = new List<string>(File.ReadAllLines(WORDSFILE));

            var uniqueLogList = logList.Distinct();
            var choisesLogList = from log in uniqueLogList
                                 where !log.Except(PHRASE).Any()
                                 select log;

            var md5HashBuilder = new StringBuilder();
            var solver = new AnagramSolver(
                choisesLogList, 
                result =>
                {
                    using (var md5Hash = MD5.Create())
                    {
                        var hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(result));

                        foreach (var h in hash)
                        {
                            md5HashBuilder.Append(h.ToString("x2"));
                        }

                        Console.Out.Write(".");

                        if (md5HashBuilder.ToString() == MD5HASCODE)
                        {
                            Console.Out.WriteLine();
                            Console.Out.WriteLine("[{0}] {1}", DateTime.Now, result);
                            Console.ReadLine();
                        }

                        md5HashBuilder.Clear();
                    }
                });

            Console.Out.WriteLine(DateTime.Now);

            solver.Execute(PHRASE);

            Console.Out.WriteLine();
            Console.Out.WriteLine(DateTime.Now);
            Console.ReadLine();
        }
    }
}
