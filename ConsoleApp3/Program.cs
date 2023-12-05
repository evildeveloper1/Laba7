using System;
using System.Collections.Generic;

class TaskScheduler<TTask, TPriority>
{
    private PriorityQueue<TTask, TPriority> taskQueue = new PriorityQueue<TTask, TPriority>();
    private Dictionary<TTask, Action<TTask>> initializationPool = new Dictionary<TTask, Action<TTask>>();
    private Dictionary<TTask, Action<TTask>> resetPool = new Dictionary<TTask, Action<TTask>>();

    public void ScheduleTask(TTask task, TPriority priority)
    {
        taskQueue.Enqueue(task, priority);
    }

    public void ExecuteNext(Action<TTask> taskExecution)
    {
        if (taskQueue.Count > 0)
        {
            TTask nextTask = taskQueue.Dequeue();
            taskExecution(nextTask);
        }
        else
        {
            Console.WriteLine("No tasks to execute.");
        }
    }

    public void AddToInitializationPool(TTask task, Action<TTask> initializationAction)
    {
        initializationPool.Add(task, initializationAction);
    }

    public void AddToResetPool(TTask task, Action<TTask> resetAction)
    {
        resetPool.Add(task, resetAction);
    }

    public void ReturnToPool(TTask task)
    {
        if (resetPool.ContainsKey(task))
        {
            resetPool[task](task);
            taskQueue.Enqueue(task, default(TPriority)); // Reset priority to default
        }
        else
        {
            Console.WriteLine($"No reset action defined for task '{task}'.");
        }
    }

    public void ExecuteNextTask()
    {
        ExecuteNext(task =>
        {
            if (initializationPool.ContainsKey(task))
            {
                initializationPool[task](task);
            }

            Console.WriteLine($"Executing task: {task}");

            ReturnToPool(task);
        });
    }
}

class PriorityQueue<TItem, TPriority> where TPriority : IComparable<TPriority>
{
    private List<Tuple<TItem, TPriority>> elements = new List<Tuple<TItem, TPriority>>();

    public int Count => elements.Count;

    public void Enqueue(TItem item, TPriority priority)
    {
        elements.Add(Tuple.Create(item, priority));
        elements.Sort((x, y) => x.Item2.CompareTo(y.Item2));
    }

    public TItem Dequeue()
    {
        if (Count == 0)
        {
            return default(TItem);
        }

        TItem item = elements[0].Item1;
        elements.RemoveAt(0);
        return item;
    }
}

class Program
{
    static void Main()
    {
        TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>();

        scheduler.ScheduleTask("Task 1", 2);
        scheduler.ScheduleTask("Task 2", 1);

        scheduler.AddToInitializationPool("Task 1", task => Console.WriteLine($"Initializing {task}"));
        scheduler.AddToInitializationPool("Task 2", task => Console.WriteLine($"Initializing {task}"));

        scheduler.AddToResetPool("Task 1", task => Console.WriteLine($"Resetting {task}"));
        scheduler.AddToResetPool("Task 2", task => Console.WriteLine($"Resetting {task}"));

        scheduler.ExecuteNextTask();
        scheduler.ExecuteNextTask();

        Console.ReadLine();
    }
}