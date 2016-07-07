using System.Collections.Generic;
using System.Linq;

namespace Applicita.Entities.Common
{
    /// <summary>
    /// Wrapper for responses from Business Layer
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Determines if request succeeded
        /// </summary>
        public virtual bool Succeeded => _succeeded;

        /// <summary>
        /// List of errors if any
        /// </summary>
        public IEnumerable<string> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                if (_errors != null && _errors.Any())
                {
                    _succeeded = false;
                }
            }
        }

        public ServiceResult()
        {
        }

        public ServiceResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ServiceResult(string error)
            : this(new[] { error })
        {
        }

        private bool _succeeded = true;
        private IEnumerable<string> _errors;
    }
}
