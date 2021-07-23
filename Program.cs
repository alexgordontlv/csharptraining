using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace sample2
{
    class Buddy
    {
        public int MyProperty { get; set; }
    }

    class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Buddy newName { get; set; }
        public string[] AllNames { get; set; }
    }
    class Person
    {
        public int Age { get; set; }
        public Name Name { get; set; }
        public string Address { get; set; }
    }
    class Program
    {
        public static string PrintSpace(int level)
        {
            var stringBuilder = new StringBuilder("");
            for (int i = 0; i < level; i++)
            {
                stringBuilder.Append("      ");
            }
            return stringBuilder.ToString();
        }

        public static void PrintObjectsByOrder<T>(T obj, int level = 0) where T : class
        {
            if (obj == null) return;

            var classProps = obj.GetType().GetProperties();
            System.Console.WriteLine($"{PrintSpace(level - 1)}Object Name is:{obj.GetType().Name}\n{PrintSpace(level)}--------------");
            foreach (var item in classProps)
            {
                var key = item.Name;
                var value = item.GetValue(obj);
                var valueType = value.GetType();
                if (valueType.IsClass && valueType != typeof(string) && !valueType.IsArray)
                {
                    System.Console.WriteLine($"{PrintSpace(level)}{key} = ");
                    PrintObjectsByOrder(value, level + 1);
                }
                else if (valueType.IsArray)
                {
                    System.Console.WriteLine($"{PrintSpace(level)}{key} = ");
                    foreach (var arrayItem in value as IEnumerable)
                    {
                        System.Console.WriteLine($"{PrintSpace(level + 1)}{arrayItem}");
                    }

                }
                else System.Console.WriteLine($"{PrintSpace(level)}{key} = {value}");
            }

        }
        static void Main(string[] args)
        {
            var person = new Person();
            var name = new Name();
            name.FirstName = "John";
            name.LastName = "Doe";
            person.Age = 55;
            name.newName = new Buddy { MyProperty = 5 };
            name.AllNames = new string[] { "lola", "katya", "masha" };
            person.Address = "215 Rochester street, NY";
            person.Name = name;
            PrintObjectsByOrder(person);

        }


    }

}
