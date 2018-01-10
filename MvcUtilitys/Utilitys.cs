using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MvcUtilitys
{
    //utility class TODO move to new dll when more than 1 function
    public static class Utilitys
    {
        /// <summary>
        /// Create Random File Name With Random Length Of 10-30 Random
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string GetRandomFileName(string fileType)
        {
            Random rnd = new Random();
            string name = string.Empty;
            int length = rnd.Next(10, 30);
            for (int i = 0; i < length; i++)
            {
                name += (char)rnd.Next(65, 91);
            }
            return name + "." + fileType;
        }
    }
}