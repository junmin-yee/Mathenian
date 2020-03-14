using System;
using System.Threading.Tasks;

namespace Mathenian.Models
{
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception exception) when (onException != null)
            {
                onException(exception);
            }
        }
    }
}
