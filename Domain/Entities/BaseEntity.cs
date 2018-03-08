using Domain.Base;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class BaseEntity : MessagePattern
    {
        public bool Deleted { get; private set; }

        [Key]
        public string Id { get; private set; }

        public DateTime? RegistratioDeleted { get; private set; }
        public DateTime Registration { get; private set; }

        public static string ToUpper(string name)
        {
            return name.ToUpper();
        }

        public void SetDeleted(bool deleted)
        {
            Deleted = deleted;
            RegistratioDeleted = DateTime.Now;
        }

        public void SetId(string id)
        {
            if (id == null)
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, "Id"));
            Id = id;
        }

        public void SetRegistration() => Registration = DateTime.Now;
    }
}