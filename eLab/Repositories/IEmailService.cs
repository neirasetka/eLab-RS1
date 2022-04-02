using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Repositories
{
    public interface IEmailService
    {
        public Task SendEmail(string email, string subject, string htmlMessage);
    }
}
