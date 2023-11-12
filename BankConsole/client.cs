namespace BankConsole;

public class Client : User
{
	public char TaxRegime { get; set; }
	public Client(int ID, string Name, string Email, decimal Balance, char TaxRegime) : base(ID, Name, Email, Balance)
	{
		this.TaxRegime = TaxRegime;
	}

	public Client()
	{
	}

	public override void AddBalance(decimal amount)
	{
			base.AddBalance(amount);
			if (amount < 0)
				return;
			
			if (TaxRegime.Equals('M'))
				Balance += (amount * 0.02m);
	}

	public override string ShowData()
	{
			return base.ShowData() + $", Régimen Fiscal: {this.TaxRegime}";
	}
}
