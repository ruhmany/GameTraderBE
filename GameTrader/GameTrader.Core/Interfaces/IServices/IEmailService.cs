using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface IEmailService
    {
        Task EmailSender(string to, string subject, string content);
    }
}
