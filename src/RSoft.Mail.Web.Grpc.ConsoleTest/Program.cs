using Microsoft.Extensions.Logging;
using RSoft.Mail.Business.Contracts;
using RSoft.Mail.Business.Models;
using RSoft.Mail.Contract;
using RSoft.Mail.Web.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace RSoft.Mail.Web.Grpc.ConsoleTest
{

    public class Program
    {
        public static void Main(string[] args)
        {


            (new Creator<DateTime>()).ShowGenericType();

            Console.ReadKey();

            //RunTest();
        }

        private static void RunTest()
        {

            //Console.WriteLine("Press any key to start test");
            //Console.ReadKey();

            IGrpcMailServiceProvider provider = new GrpcMailServiceProvider();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiNzQ1OTkxY2MtYzIxZi00NTEyLWJhOGYtOTUzMzQzNWI2NGFiIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6IlJTb2Z0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoibWFzdGVyQHNlcnZlci5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiVXNlciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9ncm91cHNpZCI6IkF1dGhlbnRpY2F0aW9uIFNlcnZpY2UiLCJuYmYiOjE2MjIzODkwODQsImV4cCI6MTYyMjQwMzYwNCwiaXNzIjoiUlNvZnQuQXV0aC5EZXYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxMDAifQ.ydvEh2xVhNRRyiXtv4dvtkD6sgZBo1W_OySz2LwMOv8";

            IEmailAddress from = new EmailAddress("rodrigo.rodrigues.tech@gmail.com", "RSoft Fetch");
            IEmailAddress replyTo = from;
            IEnumerable<IEmailAddress> to = new List<IEmailAddress>() { new EmailAddress("rodrigo.rodrigues9@fatec.sp.gov.br", "Rodrigo Fatec") };
            IEnumerable<IEmailAddress> cc = new List<IEmailAddress>();
            IEnumerable<IEmailAddress> bcc = new List<IEmailAddress>();
            IEnumerable<IFileAttachment> files = new List<FileAttachment>();
            bool enableHtml = true;

            string content = "Este é um <strong>teste</strong> de envio de e-mail via serviço gRpc";

            IMessageHandle message = new Message("Send-Mail by Console Test", content, from, replyTo, to, cc, bcc, files, enableHtml);
            //var reply = provider.SendMail(message, "https://localhost:5001", token, "rodriguesrm@gmail.com").Result;
            var reply = provider.SendMail(message, "https://odin.asgard:5031", token, "rodriguesrm@gmail.com").Result;

            Console.WriteLine($"Status: {(reply.Success ? "Success" : "FAIL")}");
            Console.WriteLine($"Message: {(reply.Success ? $"id=>{reply.MailId}" : reply.Errors)}");

            Console.ReadKey();

        }
    }

    public class Creator<T>
    {

        public void ShowGenericType()
        {

            T gen = default(T);

            Type constructed = gen.GetType();
            Console.WriteLine(constructed);

        }

    }

}
