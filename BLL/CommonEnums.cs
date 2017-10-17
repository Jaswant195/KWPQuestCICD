using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{

	/// <summary>Is the action an Insert or an Update?</summary>
	public enum UpsertAction
	{
		Insert,
		Update
	}

	/// <summary>
	/// Is the action an Insert or an Update or a Delete?
	/// </summary>
	public enum SaveChangesAction
	{
		Insert,
		Update,
		Delete
	}

	// QUERY: WOuld it be better to change the permission level to be an enum?  That way we would restrict it's value.
	//public enum PermissionLevel
	//{
	//	User = 0,
	//	Administrator = 1,
	//	ReadOnly = 2,
	//	SuperUser = 4;
	//}

	public static class PermissionLevel
	{
		public const int User = 0;
		public const int Administrator = 1;
		public const int ReadOnly = 2;
		public const int SuperUser = 4;
	}
}
