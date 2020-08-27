using Microsoft.AspNetCore.Components;

namespace BlazorApp.Component
{
    public class CounterComponent:ComponentBase
    {
        public int currentCount = 0;

        public void IncrementCount()
        {
            currentCount++;
        }
    }
}