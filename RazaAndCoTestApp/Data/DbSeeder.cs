using RazaAndCoTestApp.Model;

namespace RazaAndCoTestApp.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Users.Any()) return;

            context.Users.AddRange(
                new User
                {
                    Username = "admin",
                    PasswordHash = "$2a$11$Zl0sTap9GRqHFWcd3Iz3Mu.pGOgCcHVKFKbHcDEn2Trbw../20JCK",
                    Role = "Admin",
                    Email = "admin@test.com"
                },
                new User
                {
                    Username = "user",
                    PasswordHash = "$2a$11$Zl0sTap9GRqHFWcd3Iz3Mu.pGOgCcHVKFKbHcDEn2Trbw../20JCK",
                    Role = "User",
                    Email = "user@test.com"
                }
            );

            context.SaveChanges();
        }
    }

}
