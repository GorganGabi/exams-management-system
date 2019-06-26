using EMS.Business;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace exams_management_system
{
    public class SMTPClient
    {
        public static void StudentSendMail(StudentDetailsModel studentDetailsModel)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Random student", "ergfdfgsgasdfasd@gmail.com"));
            message.To.Add(new MailboxAddress("Gorgan Gabriel", "gabigorgan@gmail.com"));
            message.Subject = "Constestatie";

            var emailText = @"Buna ziua,
                Ma numesc " + studentDetailsModel.Name +
                " si sunt in grupa "    + studentDetailsModel.Group +
                @".  As dori sa fac contestatie.
               
                -Multumesc!";
            message.Body = new TextPart("plain")
            {
                Text = emailText
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 465, true);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("ergfdfgsgasdfasd@gmail.com", "mypassword.notsarcastic");

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public static void ProfessorSendMail(GradeDetailsModel gradeDetailsModel, StudentDetailsModel student)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Florin Olariu", "ergfdfgsgasdfasd@gmail.com"));
            message.To.Add(new MailboxAddress(student.Name, student.Email));
            message.Subject = "[" + gradeDetailsModel.ExamName + "]" + " Nota Examen";

            var attachment = new MimePart("image", "gif")
            {
                Content = new MimeContent(File.OpenRead($"..\\EMS.Presentation\\ClientApp\\src\\assets\\{gradeDetailsModel.ExamName}\\barem.png")),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName($"..\\EMS.Presentation\\ClientApp\\src\\assets\\{gradeDetailsModel.ExamName}\\barem.png")
            };

            var body = new TextPart("plain")
            {
                Text = "Buna ziua, " + student.Name +  
                        @"
                        Va anunt ca lucrarea dumneavoastra la materia " + gradeDetailsModel.ExamName +
                        " a fost corectata si nota este " + gradeDetailsModel.Value +
                        @".
                        Intrati in aplicatie si alegeti daca sunteti de acord cu aceasta nota sau nu. 
                        Mai jos am atasat baremul." +
                        "-O zi buna!"
            };


            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 465, true);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("ergfdfgsgasdfasd@gmail.com", "mypassword.notsarcastic");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}