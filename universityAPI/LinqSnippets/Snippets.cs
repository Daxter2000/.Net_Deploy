using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;



namespace LinqSnippets
{

    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golfs",
                "VW Golfs",
                "VW California",
                "Audi A3",
                "VW Golf 23",
                "VW California 23",
                "Audi A332",
            };

            //1. SELECT * FROM CARS
            var carList = from car in cars select car;
            foreach ( var car in carList )
            {
                Console.WriteLine(car.ToString());
            }

            // 2. SELECT WHERE car contains VW
             var audiList = from car in cars where car.Contains("VW") select car;
            foreach ( var audi in audiList )
            {
                Console.WriteLine(audi);
            }

        }

        //NUMBER EXAMPLES
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9};

            //Get each number multiplied by 3. take all numbers except 8 and order asc

            var processedNumerList = numbers
                .Select(x => x * 3).
                Where(x => x != 9).
                OrderBy(x => x);
        }

        static public void SerachExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bc",
                "r",
                "s",
                "p",
                "we",
                "wf"
            };
            // 1. First of all elements
            var first = textList.First();
            //2. First element equal to "C"

            var cText = textList.First(textList => textList.Equals("c"));

            //3. First eement that contains "j"
            var jText = textList.First(textList => textList.Contains("c"));

            //4. First element that contains Z or default. If cant find any coincidence, returns an empty list
            var firstOrDefaultText = textList.FirstOrDefault(x => x.Contains("z"));

            //5. Last or Default 
            var LastOrDefaultText = textList.LastOrDefault(x => x.Contains("z"));

            //6. Unique values
            var singleElement = textList.Single();
            var uniqueElement = textList.SingleOrDefault();

            int[] evenNumers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2,6,12 };

            // Remove repeated elements in one list included in another list
            var myEvenNumbers = evenNumers.Except(otherEvenNumbers);  

        }

        static public void MultipleSelects()
        {
            //SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
            };

            var myOpinionSelection = myOpinions.SelectMany(x => x.Split(','));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "a",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id =1,
                            Name = "b",
                            Email = "b@gmail.com",
                            Salary = 112331
                        },
                         new Employee
                        {
                            Id =2,
                            Name = "c",
                            Email = "c@gmail.com",
                            Salary = 123
                        },
                          new Employee
                        {
                            Id =3,
                            Name = "d",
                            Email = "d@gmail.com",
                            Salary = 1322
                        },

                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id =4,
                            Name = "e",
                            Email = "e@gmail.com",
                            Salary = 123
                        },
                         new Employee
                        {
                            Id =5,
                            Name = "f",
                            Email = "f@gmail.com",
                            Salary = 12
                        },
                          new Employee
                        {
                            Id =6,
                            Name = "g",
                            Email = "g@gmail.com",
                            Salary = 11
                        },

                    }
                }
            };

            //Get all employees from all enterprises

            var EmployeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            //Known if any list is empty
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //All enterprises at leat must have an employee with more or equal than 100 of salary

            bool hasEmployeeWithSalaryMoreThan100 = enterprises.Any(enterprise =>
            enterprise.Employees.Any(employee => employee.Salary >= 100));
        }


        static public void linqCollections()
        {
            var firstList = new List<string> () { "a", "b", "c" };
            var secondList = new List<string>() { "a", "b", "c" };

            //INER JOIN

            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement });

            //OUTER JOIN LEFT

            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };


            var leftOuterJoin1 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };

            

            //OUTER JOIN RIGHT

            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                on secondElement equals element
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where secondElement != temporalElement
                                select new { Element = secondElement };

            //UNION 
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinQ()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            var skipTwoFirstValues = myList.Skip(2);

            var skipLatTwo = myList.SkipLast(2);

            var skipWhile = myList.SkipWhile(num => num < 4);

            //TAKE

            var takeFirstTwoValues = myList.Take(2);

            var takeLastTwoValues = myList.TakeLast(2);

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4);


            //VARIABLES

            //ZIP 

            //REPEAT

            //ALL

            //AGGRETATE

            //DISTINC 

            //GROUP BY


        }

    }
}