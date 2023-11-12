namespace BankConsole;
using Newtonsoft.Json; // [JsonProperty] para serializar campos privados

//public class User: Person // Implementa clase absPersontracta Person
// public class User: IPerson // Implementa Interfaz
public class User
{
	[JsonProperty]
	protected int ID { get; set; }
	[JsonProperty]
	protected string Name { get; set; }
	[JsonProperty]
	protected string Email { get; set; }
	[JsonProperty]
	protected DateTime RegisterDate { get; set; }
	[JsonProperty]
	protected decimal Balance { get; set; }

	public User()
	{
	}

	public User(int ID, string Name, string Email, decimal Balance)
	{
		this.ID = ID;
		this.Name = Name;
		this.Email = Email;
		this.AddBalance_b(Balance); // No llamar a métodos sobreescribibles en constructor
		this.RegisterDate = DateTime.Now;
	}

	public DateTime GetRegisterDate()
	{
		return this.RegisterDate;
	}

	private void AddBalance_b(decimal amount)
	{
		decimal quantity = 0;
		if (amount <0){
			quantity = 0;
		}
		else {
			quantity = amount;
		}
		this.Balance += quantity;
	}

	public virtual void AddBalance(decimal amount)
	{
		this.AddBalance_b(amount);
	}

	public virtual string ShowData()
	{
		return $"ID: {this.ID} Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate}";
	}
	
	public string ShowData(String initalMessage)
	{
		return $"{initalMessage} -> Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate}";
	}
	public int GetUserID() {
		return this.ID;
	}
}
