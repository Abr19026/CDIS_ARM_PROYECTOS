namespace BankConsole;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Storage
{
	// Dentro del directorio donde esté el ejecutable
	static string filepath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "users.json");

	private
	static
	List<User>
	GetUsers()
	{
		var ListUsers = new List<User>();
		// Lee archivo
		string usuarios_ant = "";
		if(File.Exists(filepath))
			usuarios_ant = File.ReadAllText(filepath);

		var usuarios = JsonConvert.DeserializeObject<List<Object>>(usuarios_ant);

		if(usuarios == null)
			usuarios = new List<Object>();

		var listObjects = JsonConvert.DeserializeObject<List<object>>(usuarios_ant);

		if(listObjects == null) {
			return ListUsers;
		}

		// los convierte en objetos
		foreach(var obj in listObjects) {
			User newUser;
			JObject user = (JObject)obj;

			if(user.ContainsKey("TaxRegion")) 
				newUser = user.ToObject<Client>();
			else
				newUser = user.ToObject<Employee>();
			
			ListUsers.Add(newUser);
		}
		return ListUsers;
	}

	private
	static
	void
	SaveUsers(List<User> UserList)
	{
		JsonSerializerSettings settings = new JsonSerializerSettings{ Formatting = Formatting.Indented };
		var usuarios_serializado = JsonConvert.SerializeObject(UserList, settings);
		File.WriteAllText(filepath, usuarios_serializado);
	}

	// publicos
	public
	static
	User[]
	GetUsersByID(int ID)
	{
		return GetUsers()
			.Where(
				user =>
				user.GetUserID() == ID
			).ToArray();
	}

	public
	static
	List<User>
	GetNewUsers()
	{
		return GetUsers()
			.Where(
				user => 
					user.GetRegisterDate()
					.Date.Equals(DateTime.Today)
			).ToList();
	}

	public
	static
	QueryResultUser
	AddUser(User user)
	{
		// Obtiene usuarios anteriores
		var UserList = GetUsers();
		if(UserList
				.Where(
					x=>
						x.GetUserID() == user.GetUserID()
				).ToArray()
				.Length != 0
			) 
		{
			return QueryResultUser.ALREADY_EXISTS;
		}
		// Agrega nuevo usuario
		UserList.Add(user);
		SaveUsers(UserList);
		return QueryResultUser.SUCCESS;
	}

	public
	static 
	QueryResultUser 
	DeleteUser(int ID)
	{
		// obtiene datos del archivo
		var ListUsers = GetUsers();
		// Obtiene usuario a eliminar
		var userToDelete = ListUsers.Where(x=>x.GetUserID() == ID).ToArray();

		if (userToDelete.Length == 0) {
			return QueryResultUser.DOES_NOT_EXIST;
		}

		ListUsers.RemoveAll(x => x.GetUserID() == ID);
		// Serializar
		SaveUsers(ListUsers);
		return QueryResultUser.SUCCESS;
	}
}

public enum QueryResultUser
{
	SUCCESS,
	DOES_NOT_EXIST,
	ALREADY_EXISTS,
}
