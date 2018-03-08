using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Base
{
    [NotMapped]
    public abstract class MessagePattern
    {
        protected MessagePattern()
        {
            Errors = new List<string>();
        }

        [NotMapped]
        public List<string> Errors { get; set; }

        public bool IsFailure { get; set; }

        public void AddMessage(string mensage)
        {
            IsFailure = true;
            Errors.Add(mensage);
        }

        public void AddMessages(List<string> mensages)
        {
            IsFailure = true;
            Errors.AddRange(mensages);
        }
    }
}