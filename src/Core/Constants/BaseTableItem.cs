namespace Constants;

public static class BaseTableItem : object
{
	static BaseTableItem()
	{
	}

	public static class Role : object
	{
		static Role()
		{
		}

		public static System.Guid SimpleUser =
			new System.Guid(g: "29D863FE-8504-4A9E-8CD2-8EFC2BFAAEE2");

		public static System.Guid SpecialUser =
			new System.Guid(g: "9881A445-9960-4420-8D1D-DF599960986E");
		
		public static System.Guid Supervisor =
			new System.Guid(g: "731DE79C-C7AC-46C5-86E0-3EBFD83750C8");

		public static System.Guid Administrator =
			new System.Guid(g: "C2539A70-AD1C-4C89-8BEA-EAEB81615FF9");

		public static System.Guid ApplicationOwner =
			new System.Guid(g: "210262A3-B878-4FEA-AE21-1CD3BE57D355");

		public static System.Guid Programmer =
			new System.Guid(g: "B7BB7615-1D74-452C-A3A0-E85076B04C20");
	}
}
