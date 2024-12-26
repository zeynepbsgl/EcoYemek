using MealBox.Models.Classes;

public class UserService : IUserService
{
    private readonly Context _context;
    private readonly ILogger<UserService> _logger;
    public UserService(Context context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task AddUserAsync(User user)
    {
       _logger.LogInformation("Adding user to database");

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User added to database");
    }
}
