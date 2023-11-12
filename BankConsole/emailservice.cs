namespace BankConsole;
using MailKit.Net.Smtp;
using MimeKit;

public static class EmailService
{

	public static void SendMail()
	{
		var Message = new MimeMessage();
		Message.From.Add(new MailboxAddress("Aplicación",".......@gmail.com"));
		Message.To.Add(new MailboxAddress("Admin","abraham.ramirezmor@uanl.edu.mx"));
		Message.Subject = "BankConsole: Nuevos Usuarios";

		Message.Body = new TextPart("plain") {
			Text = GetEmailText()
		};

		using (var client = new SmtpClient()) {
			client.Connect("smtp.gmail.com",587,false); // No encriptado
			client.Authenticate("@gmail.com", "");
			client.Send(Message);
			client.Disconnect(true);
		}
	}

	private static string GetEmailText()
	{
		List<User> newUsers = Storage.GetNewUsers();
		if (newUsers.Count == 0)	{
			return "No hay usuarios nuevos";
		}
		string emailText = "Usuarios agregados hoy:\n";

		foreach(User user in newUsers) {
			emailText+= user.ShowData() + "\n";
		}
		return emailText;
	}
}
