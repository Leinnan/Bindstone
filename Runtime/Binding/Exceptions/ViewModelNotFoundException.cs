using System;

namespace Bindstone.Binding.Exceptions
{
    /// <summary>
    /// Exception thrown when the requested view-model could not be found.
    /// </summary>
    public class ViewModelNotFoundException : Exception
    {
        public ViewModelNotFoundException(string message)
            : base(message)
        {
        }
    }
}
