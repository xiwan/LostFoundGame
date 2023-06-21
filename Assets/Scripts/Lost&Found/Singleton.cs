using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound
{
    public class Singleton<T> where T : class
    {
        public static void Initialize()
        {
            Instance = (T)Activator.CreateInstance(typeof(T), true);
        }
        public static T Instance;
    }
}
