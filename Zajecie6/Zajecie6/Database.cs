using Zajecie6.Models;

namespace Zajecie6;

public static class Database  // statyczna klasa może zawierać tylko statyczne obiekty - to jest jedyna różnica
{
    public static List<Test> Tests = new List<Test>()
    {
        new Test(){Id = 1, Name = "Test1"},
        new Test(){Id = 2, Name = "Test2"},
        new Test(){Id = 3, Name = "Test3"}
    };
}