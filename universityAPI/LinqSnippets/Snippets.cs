using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;



namespace LinqSnippets
{

    public class Snippets
    {
        private static object number;

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
            foreach (var car in carList)
            {
                Console.WriteLine(car.ToString());
            }

            // 2. SELECT WHERE car contains VW
            var audiList = from car in cars where car.Contains("VW") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }

        //NUMBER EXAMPLES
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

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
            int[] otherEvenNumbers = { 0, 2, 6, 12 };

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
            var firstList = new List<string>() { "a", "b", "c" };
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


        }
        //Pagination with Skip and Take

        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }


        //VARIABLES
        //The variables inside the Query, has a limited scope inside itself. So, we can declare a Let and use it inside the block

        static public void LinQVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: ", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0}, Square: {1} ", number, Math.Pow(number, 2));
            }
        }



        //ZIP 

        static public void ZipLinQ()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);

            // {1="one", 2="two"}
        }

        //REPEAT AND RANGE

        static public void repeatRangeLinq()
        {
            //generate simple sequences
            //Generate collection 0-1000 sequence
            var first1000 = Enumerable.Range(1, 1000); //---> Range

            var aboveAverage = from number in first1000
                               let average = first1000.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            //Repeat a value N times
            var fiveXs = Enumerable.Repeat("x", 5); // { x, x, x, x, x }

        }


        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = true

                },
                 new Student
                {
                    Id = 2,
                    Name = "Pedro",
                    Grade = 80,
                    Certified = false

                },
                  new Student
                {
                    Id = 1,
                    Name = "Lucas",
                    Grade = 95,
                    Certified = true

                },
                   new Student
                {
                    Id = 1,
                    Name = "Marcos",
                    Grade = 45,
                    Certified = false

                },
                    new Student
                {
                    Id = 1,
                    Name = "Juan",
                    Grade = 92,
                    Certified = true

                },
                     new Student
                {
                    Id = 1,
                    Name = "Jesus",
                    Grade = 100,
                    Certified = true

                },
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where !student.Certified
                                       select student;

            var approvedStudents = from student in classRoom
                                   where student.Grade > 50 && student.Certified
                                   select student.Name;


        }

        //ALL

        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10);

            bool allAreBiggerOrEquialThan2 = numbers.All(number => number < 10);

            bool allNumbersAreGreaterThan0 = numbers.All(numbers => numbers > 0);

        }

        //AGGRETATE

        static public void aggregateQueries()
        {
            //Querys with sequences

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all numbers

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            string[] words = { "hello", "my", "name", "is", "Martin" };

            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);

        }


        //DISTINC 

        static public void distincValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1 };
            IEnumerable<int> notRepeatedValues = numbers.Distinct();

        }
        //GROUP BY

        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            //get even numbers and generate two groups

            var grouped = numbers.GroupBy(x => x % 2 == 0);

            foreach (var group in grouped)
            {
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }

            //ANOTHER EXAMPLE
            var classRoom = new[]
           {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = true

                },
                 new Student
                {
                    Id = 2,
                    Name = "Pedro",
                    Grade = 80,
                    Certified = false

                },
                  new Student
                {
                    Id = 1,
                    Name = "Lucas",
                    Grade = 95,
                    Certified = true

                },
                   new Student
                {
                    Id = 1,
                    Name = "Marcos",
                    Grade = 45,
                    Certified = false

                },
                    new Student
                {
                    Id = 1,
                    Name = "Juan",
                    Grade = 92,
                    Certified = true

                },
                     new Student
                {
                    Id = 1,
                    Name = "Jesus",
                    Grade = 100,
                    Certified = true

                },
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            foreach (var group in certifiedQuery)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item.Name);
                }
            }
            //we obtain two groups 
            //1. No certified
            //2. Certified students


        }



        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "New Post",
                    Content = "Content post",
                    Created = DateTime.Now,
                    Comments = new List<Comment> ()
                    {
                        new Comment()
                        {
                            Id = 1,
                             Created = DateTime.Now,
                             Title = "My fisrt comment",
                             Content = "Comment 1"
                        },
                        new Comment()
                        {
                            Id = 2,
                             Created = DateTime.Now,
                             Title = "My second comment",
                             Content = "Comment 2"
                        },
                        new Comment()
                        {
                            Id = 3,
                             Created = DateTime.Now,
                             Title = "My third comment",
                             Content = "Comment 3"
                        }
                    }
                },
                new Post()
                {
                    Id = 1,
                    Title = "Second Post",
                    Content = "Content post",
                    Created = DateTime.Now,
                    Comments = new List<Comment> ()
                    {
                        new Comment()
                        {
                            Id = 4,
                             Created = DateTime.Now,
                             Title = "My 4 comment",
                             Content = "Comment 4"
                        },
                        new Comment()
                        {
                            Id = 5,
                             Created = DateTime.Now,
                             Title = "My 5 comment",
                             Content = "Comment 5"
                        },
                        new Comment()
                        {
                            Id = 6,
                             Created = DateTime.Now,
                             Title = "My 6 comment",
                             Content = "Comment 6"
                        }
                    }
                }
            };

            var commentsWihContent = posts.SelectMany(post => post.Comments,
                (post, comment) => 
                    new { PostId = post.Id, CommentContent = comment.Content });
        }
    }
}