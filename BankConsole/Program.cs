// See https://aka.ms/new-console-template for more information
using BankConsole;
using System.Text.RegularExpressions;

// Cliente email, argumentos
#region parte4

if (args.Length == 0) {
	Console.WriteLine("Enviando correo ...");
	// EmailService.SendMail();
}
else
	ShowMenu();

void ShowMenu()
{
	Console.Clear();
	Console.WriteLine("Selecciona una opción");
	Console.WriteLine("1- Crear un usuario nuevo");
	Console.WriteLine("2- Eliminar usuario existente");
	Console.WriteLine("3- Salir");

	int option = LeerValor("Debes ingresar un número (1, 2 o 3).\n", int.Parse, x => x >=1 || x <= 3);

	switch(option) {
		case 1:
			CreateUser();
			break;
		case 2:
			DeleteUser();
			break;
		case 3:
			Environment.Exit(0);
			break;
	}
}

void CreateUser()
{
	Regex reg_mail = new Regex(@"^[\w\.-]+@[\w\.-]+$");
	// positivo, no repetible
	int ID = LeerValor("ID: ",int.Parse, x => x>0 && Storage.GetUsersByID(x).Length == 0);

	Console.Write("Nombre: ");
	string name = Console.ReadLine();

	// Formato correcto
	string email = LeerValor("Email: ", x=>x, x=> reg_mail.IsMatch(x));

	// Decimal positivo
	decimal balance = LeerValor("Saldo: ", decimal.Parse, x=>x>0);

	// Solo permitir 'c' y 'e'
	char userType = LeerValor(
				"Escribe 'c' si el usuario es Cliente , 'e' si es Empleado: ",
				char.Parse, x=>new char[]{'c','e'}.Contains(x));

	User newUser;

	if (userType.Equals('c')) {
		char taxRegime = LeerValor("Regimen Fiscal: ", char.Parse, x=>true);
		newUser = new Client(ID, name, email, balance, taxRegime);
	} else {
		Console.Write("Departamento: ");
		string department = Console.ReadLine();
		newUser = new Employee(ID, name, email, balance, department);
	}

	Storage.AddUser(newUser);
	Console.WriteLine("Usuario Creado");
	Thread.Sleep(2000);
	ShowMenu();
}

void DeleteUser()
{
	Console.Clear();
	int ID;
	bool valido = false;
	// Positivo, debe existir
	do {
		ID = LeerValor("Ingresa el ID del usuario a eliminar: ",int.Parse, x => x>0 );
		valido = Storage.DeleteUser(ID) == QueryResultUser.SUCCESS;
		if(!valido) {
			Console.WriteLine("Usuario no existe");
		}
	} while(!valido);

	Console.WriteLine("Usuario eliminado");
	Thread.Sleep(2000);
	ShowMenu();
}
#endregion

T LeerValor<T>(string Mensaje, Func<string, T> convertidor,Func<T, bool> validador)
{
    bool valido = false;
    T retorno = default(T);
		do {
			try {
				Console.Write(Mensaje);
				retorno = convertidor(Console.ReadLine());
				valido = validador(retorno);
			} 
			catch (FormatException){}
			catch (OverflowException){}

			if (!valido) {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Entrada Invalida, intente de nuevo");
				Console.ForegroundColor = ConsoleColor.White;
			}
		} while(!valido);
    return retorno;
}
