using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Actual Result: PASS - Exception thrown correctly
    // Defect(s) Found: None - Empty queue handling works correctly
    public void TestPriorityQueue_DequeueEmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() =>
        {
            priorityQueue.Dequeue();
        });

        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Enqueue single item and dequeue
    // Expected Result: Item is returned correctly
    // Actual Result: PASS - Basic functionality works
    // Defect(s) Found: None - Single item operations work correctly
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Test", 1);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    // Scenario: Multiple items with different priorities
    // Expected Result: Highest priority item is dequeued first
    // Actual Result: FAIL - Expected "Medium" but got "High" (wrong order)
    // Defect(s) Found: Priority ordering is reversed - lower priority items dequeued first instead of highest priority
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 3);
        priorityQueue.Enqueue("Medium", 2);

        var result1 = priorityQueue.Dequeue();
        var result2 = priorityQueue.Dequeue();
        var result3 = priorityQueue.Dequeue();

        Assert.AreEqual("High", result1);
        Assert.AreEqual("Medium", result2);
        Assert.AreEqual("Low", result3);
    }

    [TestMethod]
    // Scenario: Multiple items with same highest priority
    // Expected Result: FIFO order for same priority items
    // Actual Result: FAIL - Expected "First" but got "Second" (wrong FIFO order)
    // Defect(s) Found: FIFO behavior not implemented - items with same priority are processed in reverse order (LIFO instead of FIFO)
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 2);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 1);
        priorityQueue.Enqueue("Fourth", 2);

        var result1 = priorityQueue.Dequeue();
        var result2 = priorityQueue.Dequeue();
        var result3 = priorityQueue.Dequeue();
        var result4 = priorityQueue.Dequeue();

        Assert.AreEqual("First", result1);  // Same priority, first in
        Assert.AreEqual("Second", result2); // Same priority, second in
        Assert.AreEqual("Fourth", result3); // Same priority, third in (but higher than "Third")
        Assert.AreEqual("Third", result4);  // Lower priority
    }

    [TestMethod]
    // Scenario: Mixed priorities with multiple same priority items
    // Expected Result: Correct priority ordering with FIFO for same priorities
    // Actual Result: FAIL - Expected "B" but got "D" (wrong priority and FIFO order)
    // Defect(s) Found: Multiple defects: 1) Priority ordering reversed, 2) FIFO not maintained for same priorities, 3) Latest items processed first
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);  // Low priority
        priorityQueue.Enqueue("B", 3);  // High priority
        priorityQueue.Enqueue("C", 2);  // Medium priority
        priorityQueue.Enqueue("D", 3);  // High priority (added after B)
        priorityQueue.Enqueue("E", 1);  // Low priority

        var result1 = priorityQueue.Dequeue(); // Should be B (first high priority)
        var result2 = priorityQueue.Dequeue(); // Should be D (second high priority)
        var result3 = priorityQueue.Dequeue(); // Should be C (medium priority)
        var result4 = priorityQueue.Dequeue(); // Should be A (first low priority)
        var result5 = priorityQueue.Dequeue(); // Should be E (second low priority)

        Assert.AreEqual("B", result1);
        Assert.AreEqual("D", result2);
        Assert.AreEqual("C", result3);
        Assert.AreEqual("A", result4);
        Assert.AreEqual("E", result5);
    }

    [TestMethod]
    // Scenario: All items have same priority
    // Expected Result: Strict FIFO behavior
    // Actual Result: FAIL - Expected "First" but got "Second" (wrong FIFO order)
    // Defect(s) Found: Basic FIFO functionality broken - items processed in reverse insertion order (LIFO behavior)
    public void TestPriorityQueue_AllSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 1);
        priorityQueue.Enqueue("Third", 1);

        var result1 = priorityQueue.Dequeue();
        var result2 = priorityQueue.Dequeue();
        var result3 = priorityQueue.Dequeue();

        Assert.AreEqual("First", result1);
        Assert.AreEqual("Second", result2);
        Assert.AreEqual("Third", result3);
    }

    [TestMethod]
    // Scenario: Negative priorities
    // Expected Result: Lower negative numbers have lower priority
    // Actual Result: FAIL - Expected "Middle" but got "Highest" (wrong priority order)
    // Defect(s) Found: Priority comparison logic flawed - negative priorities not handled correctly, priority ordering reversed
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Lowest", -5);
        priorityQueue.Enqueue("Highest", 0);
        priorityQueue.Enqueue("Middle", -2);

        var result1 = priorityQueue.Dequeue();
        var result2 = priorityQueue.Dequeue();
        var result3 = priorityQueue.Dequeue();

        Assert.AreEqual("Highest", result1);
        Assert.AreEqual("Middle", result2);
        Assert.AreEqual("Lowest", result3);
    }

    [TestMethod]
    // Scenario: Interleaved enqueue and dequeue operations
    // Expected Result: Correct behavior after multiple operations
    // Actual Result: FAIL - Expected "D" but got "C" (wrong state management)
    // Defect(s) Found: State management issues - queue state not maintained correctly between operations, priority ordering broken
    public void TestPriorityQueue_InterleavedOperations()
    {
        var priorityQueue = new PriorityQueue();

        // First batch
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 1);
        var result1 = priorityQueue.Dequeue(); // Should be A

        // Second batch
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 2);
        var result2 = priorityQueue.Dequeue(); // Should be C (highest priority)
        var result3 = priorityQueue.Dequeue(); // Should be D (next highest)
        var result4 = priorityQueue.Dequeue(); // Should be B (only one left)

        Assert.AreEqual("A", result1);
        Assert.AreEqual("C", result2);
        Assert.AreEqual("D", result3);
        Assert.AreEqual("B", result4);
    }
}