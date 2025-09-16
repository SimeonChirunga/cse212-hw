public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: we create a new array of doubles with the specified length
        // Step 2: Use a loop to fill the array with multiples
        // Step 3: For each index i (from 0 to length-1), we calculate the multiple as: number * (i + 1)
        //         because the first multiple should be number * 1, second should be number * 2, etc.
        // Step 4: Return the filled array

        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;

        
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Calculate the effective rotation amount
        //         Since rotating right by 'amount' is equivalent to taking the last 'amount' elements
        //         and moving them to the front

        // Step 2: Get the elements that need to be moved to the front
        //         These are the last 'amount' elements of the list

        // Step 3: Remove those elements from their current position
        // Step 4: Insert them at the beginning of the list

        // Handle edge case: if amount equals the list length, no rotation needed
        if (amount == data.Count || amount == 0)
            return;
        
        // Get the elements to move
        List<int> elementsToMove = data.GetRange(data.Count - amount, amount);

        // Remove those elements from the end
        data.RemoveRange(data.Count - amount, amount);

        // Insert them at the beginning
        data.InsertRange(0, elementsToMove);
    }
}
