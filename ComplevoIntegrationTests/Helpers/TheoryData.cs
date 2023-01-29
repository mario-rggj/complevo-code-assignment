namespace ComplevoIntegrationTests.Helpers;

public partial class BasicTests
{
  public class TheoryData<T1, T2> : TheoryData
  {
    public void Add(T1 p1, T2 p2)
    {
      AddRow(p1, p2);
    }
  }
}