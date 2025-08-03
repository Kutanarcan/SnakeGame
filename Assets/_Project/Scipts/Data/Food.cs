namespace Snake.Runtime.CSharp
{
    public struct Food
    {
        public int index; // Index of the food in the grid
        
        // Dispose
        public void Dispose()
        {
            index = -1; // Mark as disposed by setting index to an invalid value
        }
    }
}