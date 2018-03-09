using System;
using TestingEFCoreInMemory.Models;

namespace TestingEFCoreInMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new TimeRegistrationContext())
            {
                var user = new UserEntity();
                user.FirstName = "Bob";
                user.LastName = "Barker";

                db.Users.Add(user);
                db.SaveChanges();

                foreach( var savedUser in db.Users)
                {
                    Console.WriteLine($"user saved with data:  id={savedUser.Id}");
                }

            }
            Console.ReadLine();
        }
    }
}
