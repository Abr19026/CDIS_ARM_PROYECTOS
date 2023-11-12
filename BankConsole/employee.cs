namespace BankConsole;

public class Employee : User, IPerson
{
	public string Department { get; set; }

	public Employee()
	{
	}

	public Employee(int ID, string Name, string Email, decimal Balance, string Department) : base(ID, Name, Email, Balance)
	{
		this.Department = Department;
	}

	public override void AddBalance(decimal amount)
	{
			base.AddBalance(amount);
			if (amount < 0)
				return;
			
			if (this.Department.Equals("IT"))
				Balance += (amount * 0.05m);
	}

	public override string ShowData()
	{
			return base.ShowData() + $", Departamento: {this.Department}";
	}

	public string GetName()
	{
		return Name;
	}

	public string GetCountry()
	{
		return "México";
	}
}
