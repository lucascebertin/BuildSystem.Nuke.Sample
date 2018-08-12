namespace BuildSystem.Nuke.Sample.App.Model
{
    public interface ICalc
    {
        int Sum(int a, int b);
    }

    public class Calc : ICalc
    {
        public int Sum(int a, int b) => a + b;
    }
}
