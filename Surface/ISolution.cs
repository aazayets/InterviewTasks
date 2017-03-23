namespace Surface
{
    public interface ISolution
    {
        void Initialize(SurfaceType[][] surface);
        int GetArea(int x, int y);
    }

    public class TestSolution : ISolution
    {
        public void Initialize(SurfaceType[][] surface)
        {
        }

        public int GetArea(int x, int y)
        {
            return 0;
        }
    }
}