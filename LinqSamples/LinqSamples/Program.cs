using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>() { 4, 6, 7, 8, 34, 33, 11 };
            var isTrue = numbers.TrueForAll(o => o > 0);
            p(isTrue);

            var unique = "Apples, Oranges, Apples, Melons"
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            p(unique);

            var EmpList = new List<Employee>
            {
                new Employee{Id =1, Name = "Jack", Age = 29},
                new Employee{Id =2, Name = "Jim", Age = 30},
                new Employee{Id =3, Name = "Jill", Age = 27},
                new Employee{Id =4, Name = "James", Age = 33},
                new Employee{Id =5, Name = "John", Age = 25},
            };

            var lst = EmpList
                        .Where(e => e.Age > 28)
                        .Select(e => e.Name);

            EmpList.Add(new Employee { Id = 1, Name = "Jeb", Age = 29 });

            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        static void p(dynamic w)
        {
            Console.WriteLine(w);
        }
    }

    class Employee {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

}
