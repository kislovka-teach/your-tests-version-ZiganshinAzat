namespace TaskManagerAPI.Abstractions;

public interface IPasswordHasherService
{
    string Hash(string input);

    bool Verify(string input, string hashString);
}