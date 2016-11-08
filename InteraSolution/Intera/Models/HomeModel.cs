using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using Intera.Entity;

namespace Intera.Models
{
    public class HomeModel : ModelBase
    {
        public void EnviarEmail(string emailDestinatario, Pessoa p)
        {
            //valores
            string nomeRemetente = "INTERA";
            string emailRemetente = "interaproject2016@gmail.com";
            string assuntoMensagem = "Recuperação de Senha";
            string conteudoMensagem = "Caro usuário " + p.Nome + ", " +
                "Conforme solicitado, sua senha é " + p.Senha +
                " Caso queira trocar a senha basta fazer o login e clique  no seu nome, após isso exibirá as opções do usuário, vá em seu perfil 'profile' e nela o senhor poderá trocar sua senha.";

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Define a prioridade do e-mail.
            objEmail.Priority = System.Net.Mail.MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = false;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailRemetente, "intera2016");

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Send(objEmail);
        }
    }
}