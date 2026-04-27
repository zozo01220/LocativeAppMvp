public class EmailService : IEmailService
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine("=== EMAIL ===");
        Console.WriteLine($"TO: {to}");
        Console.WriteLine($"SUBJECT: {subject}");
        Console.WriteLine(body);
        Console.WriteLine("=============");

        return Task.CompletedTask;
    }
}