using Microsoft.EntityFrameworkCore;
using MicroSvc01Core.Context;

namespace MicroSvc01Core.Code
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DogContext(serviceProvider.GetRequiredService<DbContextOptions<DogContext>>()))
            {
                if(context == null || context.Dog == null)
                {
                    throw new ArgumentNullException("Null DogContext");
                }

                if(context.Dog.Any())
                {
                    return;
                }

                context.Dog.AddRange(
                    new Models.Dog
                    {
                        Breed = "Boder Collie",
                        Name = "Jake",
                        Sex = "M",
                        Age = 2
                    },
                    new Models.Dog
                    {
                        Breed = "MinPin",
                        Name = "Samson",
                        Sex = "M",
                        Age = 16
                    },
                    new Models.Dog
                    {
                        Breed = "American Bulldog",
                        Name = "Amy",
                        Sex = "F",
                        Age = 7
                    },
                    new Models.Dog
                    {
                        Breed = "Bloodhound",
                        Name = "Sissy",
                        Sex = "F",
                        Age = 12
                    },
                    new Models.Dog
                    {
                        Breed = "Chihuahua",
                        Name = "Chico",
                        Sex = "M",
                        Age = 4
                    },
                    new Models.Dog
                    {
                        Breed = "Doberman Pinscher",
                        Name = "Bandit",
                        Sex = "M",
                        Age = 9
                    },
                    new Models.Dog
                    {
                        Breed = "Doberman Pinscher",
                        Name = "Bambi",
                        Sex = "F",
                        Age = 8
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
