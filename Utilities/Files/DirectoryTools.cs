﻿using System.Collections.Generic;
using System.IO;

namespace Utilities.Files
{
    public class DirectoryTools
    {
        public static void DeleteDirectory(string path, bool recurse)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recurse);
            }
        }

        public static List<string> GetSubFiles(string root, string extension)
        {
            List<string> ret = new List<string>();
            
            if (!Directory.Exists(root)) return ret;

            
            try
            {
                ret.AddRange(Directory.GetFiles(root, "*." + extension, SearchOption.TopDirectoryOnly
                    /* Other option seems to fall over on security exceptions */));
            }
            catch
            {
                //Possible security problem
            }


            foreach (string subDir in Directory.GetDirectories(root, "*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    ret.AddRange(GetSubFiles(subDir, extension));
                }
                catch
                {
                    //Possible security problem
                }
            }

            return ret;
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}